using reverse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace reverse
{
    class Program
    {
        /// <summary>
        /// オセロの盤面を表す配列
        /// </summary>
        static PieceColor[,] stage = new PieceColor[8, 8];

        /// <summary>
        /// オセロのメインメソッド
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            for (int y = 0; y < stage.GetLength(0); y++)
            {
                for (int x = 0; x < stage.GetLength(1); x++)
                {
                    stage[y, x] = PieceColor.None;
                }
            }

            stage[3, 3] = PieceColor.Black;
            stage[3, 4] = PieceColor.White;
            stage[4, 3] = PieceColor.White;
            stage[4, 4] = PieceColor.Black;

            PieceColor color = PromptPieceColor();
            while (true)
            {
                showBoard(color);

                if (color == PieceColor.Black)
                {
                    Console.WriteLine("黒(○)のターンです");
                }
                else if (color == PieceColor.White)
                {
                    Console.WriteLine("白(●)のターンです");
                }

                ProcessPlayerTurn(color);

                // 攻守交替
                if (color == PieceColor.Black)
                {
                    color = PieceColor.White;
                }
                else if(color == PieceColor.White)
                {
                    color = PieceColor.Black;
                }
            }
        }

        /// <summary>
        /// 盤面をコンソールに表示します。
        /// </summary>
        /// <param name="array"></param>
        public static void showBoard(PieceColor color)
        {
            Console.SetCursorPosition(0, 0);
            Console.Clear();
            Console.WriteLine("   1   2   3   4   5   6   7   8");
            for (int y = 0; y < stage.GetLength(0); y++)
            {
                if (y == 0)
                {
                    Console.WriteLine(" ┏━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┓");
                }

                Console.Write(y + 1);
                Console.Write("┃");

                for (int x = 0; x < stage.GetLength(1); x++)
                {
                    PiecePosition pos = new PiecePosition() { X = x , Y = y  };

                    if (stage[y, x] == PieceColor.None)
                    {
                        if (!CanPutPiece(color,pos))
                        {
                            Console.Write("   "); 
                        }
                        else
                        {
                            Console.Write(" ・");
                        }
                    }
                    else if (stage[y, x] == PieceColor.White)
                    {
                        Console.Write(" ● ");
                    }
                    else if (stage[y, x] == PieceColor.Black)
                    {
                        Console.Write(" ○ ");
                    }

                    Console.Write("┃");
                }

                Console.Write("\n");
                if (y == 7)
                {
                    Console.WriteLine(" ┗━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┛");
                }
                else
                {
                    Console.WriteLine(" ┣━━━╋━━━╋━━━╋━━━╋━━━╋━━━╋━━━╋━━━┫");
                }


            }
        }

        /// <summary>
        ///プレイヤーのターンを処理します。
        ///この関数は一人分のターンを担当します。
        /// </summary>
        public static void ProcessPlayerTurn(PieceColor color)
        {
            PiecePosition pos;

            while (true)
            {
                // パスの判定
                if (!HasPlacePiece(color))
                {
                    Console.WriteLine("おける場所がないのでパスします");
                    Thread.Sleep(2000);
                    return;
                }



                // コマの座標
                pos = PromptPiecePosition(color);

                // 現在のマスの状態
                PieceColor current = stage[pos.Y, pos.X];

                if (current != PieceColor.None)
                {
                    Console.WriteLine("[ERROR] すでにコマが置かれています");
                    continue; // 最初に戻る
                }

                // コマの判定
                if (!CanPutPiece(color, pos))
                {
                    Console.WriteLine("[ERROR] そこにコマを置くことはできません");
                    continue;
                }

                break;

            }

            stage[pos.Y, pos.X] = color;

            // コマの反転
            ReversePiece(color, pos);

            //勝敗判定
            if (IsGameFinished())
            {
                showBoard(color);
                DisplayWinner();
                Console.ReadLine();
                Environment.Exit(0);
            }
        }



        /// <summary>
        /// ユーザーに次に置くコマの色を尋ねます。
        /// </summary>
        private static PieceColor PromptPieceColor()
        {
            int color;
            while (true)
            {
                Console.WriteLine("あなたの駒の色を教えてください。\n※白(●)は１、黒(○)は２です");
                Console.Write("駒の色: ");
                color = ReadInt();
                if (color == 1 || color == 2)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("[ERROR] 駒の色は1か2で入力してください");
                }
            };

            return (PieceColor)color;
        }

        /// <summary>
        /// コンソールから整数を読み取ります。
        /// </summary>
        /// <returns></returns>
        private static int ReadInt()
        {
            return int.Parse(Console.ReadLine() ?? "");
        }

        /// <summary>
        /// ユーザーに次に置くコマの座標を尋ねます。
        /// </summary>
        /// <returns>コマの座標を表すPiecePosition</returns>
        private static PiecePosition PromptPiecePosition(PieceColor color)
        {
            while (true)
            {
                int x;
                while (true)
                {
                    Console.WriteLine("駒を置く列を1以上8以下で入力してください");
                    Console.Write("列の数字: ");
                    x = ReadInt();
                    if (x >= 1 && x <= 9)
                    {
                        break;
                    }
                    else
                    {
                        // 盤面に存在しない座標の場合は入力し直し
                        Console.WriteLine("[ERROR] 列の数字は1以上8以下で入力してください");
                    }
                };

                int y;
                while (true)
                {
                    Console.WriteLine("駒を置く行を1以上8以下で入力してください");
                    Console.Write("行の数字: ");
                    y = ReadInt();
                    if (y >= 1 && y <= 9)
                    {
                        break;
                    }
                    else
                    {
                        // 盤面に存在しない座標の場合は入力し直し
                        Console.WriteLine("[ERROR] 行の数字は1以上8以下で入力してください");
                    }
                };

                string colorString = (color == PieceColor.White) ? "白(●)" : "黒(○)";

                Console.WriteLine("({0} {1})に{2}の駒を置きます", x, y, colorString);
                Console.WriteLine("よろしいですか?");
                Console.WriteLine("訂正する場合は0、進む場合は1を入力してください");

                if (ReadInt() == 1)
                {
                    return new PiecePosition() { X = x - 1, Y = y - 1 };
                }
            }
        }

        // 単位方向ベクトルリスト
        public static int[][] vectors = { new int[] { -1,0 } ,       //上 0
                                          new int[] { -1,-1 } ,       //左斜め上 1
                                          new int[] { 0, -1 },       //左 2
                                          new int[] { 1, -1 },       //左斜め下 3
                                          new int[] { 1, 0 },        //下 4
                                          new int[] { 1, 1 },        //右斜め下 5
                                          new int[] { 0,1 },          //右 6
                                          new int[] { -1, 1 },       //右斜め上 7
                                         };

        /// <summary>
        /// コマを置くことができるかどうかを判定します。
        /// </summary>
        /// <param name="color"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static bool CanPutPiece(PieceColor color, PiecePosition pos)
        {
            foreach (int[] v in vectors)
            {
                int vY = pos.Y + v[0];
                int vX = pos.X + v[1];

                //　壁判定の場合処理を停止します
                if (vY < 0 || vX < 0 || vY >= 8 || vX >= 8)
                {
                    // 次のベクトルに移り、判定をやり直す
                    continue;
                }

                if (stage[vY, vX] != color && stage[vY, vX] != PieceColor.None)
                {

                    for (int i = 2; i <= 7; i++)
                    {
                        int vY2 = pos.Y + i * v[0];
                        int vX2 = pos.X + i * v[1];

                        //　壁の場合、判定を停止します
                        if (vY2 < 0 || vX2 < 0 || vY2 >= 8 || vX2 >= 8)
                        {
                            continue;
                        }

                        if (stage[vY2, vX2] == color)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// コマを反転させます
        /// </summary>
        /// <param name="color"></param>
        /// <param name="pos"></param>
        public static void ReversePiece(PieceColor color, PiecePosition pos)
        {
            // 八方向すべての隣ベクトルを使って調べる
            foreach (int[] v in vectors)
            {
                // 現在位置＋隣ベクトルの座標の計算
                int vY = pos.Y + v[0];
                int vX = pos.X + v[1];

                //　壁判定の場合処理を停止します
                if (vY < 0 || vX < 0 || vY >= 8 || vX >= 8)
                {
                    continue;
                }

                //　隣ベクトル方向に進んだコマが自分の色ではないかつ、空白ではないの場合
                if (stage[vY, vX] != color && stage[vY, vX] != PieceColor.None)
                {

                    //　隣ベクトル方に壁、もしくは自身の色が見つかるまで調べる
                    for (int i = 2; i <= 7; i++)
                    {
                        int vY2 = pos.Y + i * v[0];
                        int vX2 = pos.X + i * v[1];

                        //　壁の場合、判定をスキップします
                        if (vY2 < 0 || vX2 < 0 || vY2 >= 8 || vX2 >= 8)
                        {
                            continue;
                        }

                        //　隣ベクトル方向に進んだ先にあるコマが自分と同じ色ならば
                        if (stage[vY2, vX2] == color)
                        {
                            //　自分と同じ色が見つかったコマまで自分の色にする
                            for (int j = 1; j <= i - 1; j++)
                            {
                                // posに格納されている座標から単位ベクトル方向にj個目進んだコマの座標
                                int vY3 = pos.Y + j * v[0];
                                int vX3 = pos.X + j * v[1];
                                stage[vY3, vX3] = color;
                            }
                            // posから最も近い自分のコマの色までの処理で終了する
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ゲームが終了したかどうかを判定します
        /// </summary>
        /// <returns></returns>
        public static bool IsGameFinished()
        {
            int cnt = 0;
            for (int Y1 = 0; Y1 <= 7; Y1++)
            {
                for (int X1 = 0; X1 <= 7; X1++)
                {
                    if (stage[Y1, X1] != PieceColor.None)
                    {
                        cnt++;
                    }
                }
            }

            if (cnt == 64)
            {
                // マスに空白のマスがない場合に「終了」を返す
                return true;
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                { 
                    PiecePosition pos = new PiecePosition() { X = i, Y = j };

                    if (CanPutPiece(PieceColor.White, pos))
                    {
                        return false;
                    }
                    if (CanPutPiece(PieceColor.Black, pos))
                    {
                        return false;
                    }


                }
            }
            return true;
        }

        /// <summary>
        /// 勝者の色を表示します。
        /// </summary>
        public static void DisplayWinner()
        {
            int wcnt = 0;
            int bcnt = 0;
            for (int Y = 0; Y <= 7; Y++)
            {
                for (int X = 0; X <= 7; X++)
                {
                    if (stage[Y, X] == PieceColor.White)
                    {
                        wcnt++;
                    }
                    else
                    {
                        bcnt++;
                    }
                }
            }
            if (bcnt < wcnt)
            {
                Console.WriteLine("黒の勝ち！");
            }
            else if (wcnt < bcnt)
            {
                Console.WriteLine("白の勝ち！");
            }
            else
            {
                Console.WriteLine("引き分け！");
            }
        }

        /// <summary>
        /// 盤面に指定した色のコマがおける場所があるかどうかを判定します。
        /// </summary>
        /// <returns></returns>
        public static bool HasPlacePiece(PieceColor color)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    PiecePosition pos = new PiecePosition() { X = i, Y = j };

                    if (CanPutPiece(color, pos))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}