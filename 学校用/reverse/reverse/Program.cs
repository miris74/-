using reverse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace reverse
{
    class Program
    {
        /// <summary>
        /// オセロの盤面を表す配列
        /// </summary>
        static PieceColor[,] stage = new PieceColor[8, 8];

        //オセロ作成のメインメソッド
        static void Main(string[] args)
        {
            bool fin = false;

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

            while (true)
            {
                showBoard();
                top();
            }
        }   

        /// <summary>
        /// 盤面をコンソールに表示します。
        /// </summary>
        /// <param name="array"></param>
        public static void showBoard()
        {

            for (int y = 0; y < stage.GetLength(0); y++)
            {
                Console.Write("|");
                for (int x = 0; x < stage.GetLength(1); x++)
                {
                    if (stage[y, x] == PieceColor.None)
                    {
                        Console.Write("  ");
                    }
                    else if (stage[y, x] == PieceColor.White)
                    {
                        Console.Write("〇");
                    }
                    else if (stage[y, x] == PieceColor.Black)
                    {
                        Console.Write("●");
                    }

                    Console.Write("|");
                }
                Console.WriteLine("\n-------------------------");
            }
        }

        //駒の配置に関する関数
        public static void top()
        {
            while (true)
            {
                // コマの色
                PieceColor color = PromptPieceColor();

                // コマの座標
                PiecePosition pos = PromptPiecePosition(color);

                // 現在のマスの状態
                PieceColor current = stage[pos.Y - 1, pos.X - 1];

                if (current != PieceColor.None)
                {
                    Console.WriteLine("[ERROR] すでにコマが置かれています");
                    continue; // 最初に戻る
                }

                // 
                stage[pos.Y - 1 , pos.X - 1 ] = color;

                break;
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
                Console.WriteLine("あなたの駒の色を教えてください。\n※白は１、黒は２です");
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

                string colorString = (color == PieceColor.White) ? "白" : "黒";

                Console.WriteLine("({0} {1})に{2}の駒を置きます", x, y, colorString);
                Console.WriteLine("よろしいですか?");
                Console.WriteLine("訂正する場合は0、進む場合は1を入力してください");

                if (ReadInt() == 1)
                {
                    return new PiecePosition() { X = x, Y = y };
                }
            }
        }

        // 単位方向ベクトルリスト

        public static int[,] vector = { { 0,-1 } ,   //上
                                 { -1,-1 } ,  //左斜め上
                                 { -1,0 },    //左
                                 { -1,1 },    //左斜め下
                                 { 0,1 },     //下
                                 { 1,1 },     //右斜め下
                                 { 1,0 },     //右
                                 { 1,-1 },    //右斜め上
                               };

        // 駒が挟まれているかどうかの判定関数
        static void Piecedetection()
        {

        }
    }
}