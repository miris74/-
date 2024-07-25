uDarknightsing System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
namespace myproject
{
    class Program
    {
        static void Main(string[] args)
        {
            string enemyname = "シロナ";
            string penname = "チャンピオン";
            int playernum = 0;
            Pokemon MIkaruge = new Mikaruge();

            Console.WriteLine("あなたの名前は？");
            string playername = Console.ReadLine();
            Console.WriteLine("あなたの名前は");
            Console.WriteLine("{0}です", playername);
            Console.WriteLine("{0}の　{1}が\n　勝負を　しかけてきた！", penname, enemyname);
            Console.WriteLine("{0}の　{1}は\n　を{2}をくりだした！", penname, enemyname, Mikaruge.Name);

            int enemynum = enemyselect(playernum);
        }

        public static int choose()
        {
            int result = 0;
            Console.WriteLine("{0}は　どうする？\n (１－４の中で選んでください)");
            int selectNum = int.Parse(Console.ReadLine());

            if(0< selectNum && selectNum < 5)
            {
                return selectNum;
            }
            else
            {
                Console.WriteLine("無効な入力です。正しい値を入力してください");
                choose();
            }

            return result;
        }

        //敵の技番号１－４をランダムに出力するプログラム
        static int enemyselect(int playernum)
        {
            Random random = new Random();
            int enemynum = random.Next(1, 5);
            return enemynum;
        }
    }


    public class Trainer
    {
        public string Battel { get; set; }
        public string PlayerItem { get; set; }
        public string Change { get; set; }
        public string Run { get; set; }


    }

    public class Pokemon
    {

        public string Name { get; set; }
        public int Level { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }
        public int Hitpoint { get; set; }
        public int Atack { get; set; }
        public int Difence { get; set; }
        public int SpecialAtack { get; set; }
        public int SpecialDifence { get; set; }
        public int Speed { get; set; }
        public string Waza1 { get; set; }
        public string Waza2 { get; set; }
        public string Waza3 { get; set; }
        public string Waza4 { get; set; }
        public string Tokusei { get; set; }
        public string PkemonItem { get; set; }

        public static void battle(int playernum, int enemynum, Pokemon player, Pokemon enemy)
        {
            if (player.Speed < enemy.Speed)
            {
                player.Hitpoint = player.Hitpoint - 10;
                Console.WriteLine("{0}の\n たいあたり！", enemy.Name);
                Console.WriteLine("{0}に\n 10のダメージ！", player.Name);
            }
            else
            {
                enemy.Hitpoint = enemy.Hitpoint - 10;
                Console.WriteLine("{0}の\n たいあたり！", player.Name);
                Console.WriteLine("{0}に\n 10のダメージ！", enemy.Name);
            }
        }

        public static void itemUse()
        {

        }


    }



    //ポケモンステータス振り分け

    public class Mikaruge : Pokemon
    {
        //ポケモン名　ミカルゲ
        public string Name = "ミカルゲ";

        //タイプ
        public string Type1 = "ゴースト";
        public string Type2 = "あく";

        //ステータス実数値
        public int Hitpoint = 125;       //hp        125 hp
        public int Atack = 112;      //こうげき  112 atk
        public int Difence = 128;      //ぼうぎょ  128 dfe
        public int SpecialAtack = 112;    //とくこう  112 spatk
        public int SpecialDifence = 128;    //とくぼう  128 spdfe
        public int Speed = 55;        //すばやさ  55 spe

        //わざこうせい
        public string Waza1 = "シャドーボール";
        public string Waza2 = "サイコキネシス";
        public string Waza3 = "ぎんいろのかぜ";
        public string Waza4 = "さしおさえ";

        //とくせい
        public string Tokusei = "プレッシャー";
    }


    public class Roserade : Pokemon

    {
        //ポケモン名　ロズレイド
        public string Name = "ロズレイド";

        //タイプ
        public string Type1 = "くさ";
        public string Type2 = "どく";

        //ステータス実数値
        public int Hitpoint = 135;       //hp        135 hp
        public int Atack = 90;       //こうげき  90 atk
        public int Difence = 85;       //ぼうぎょ  85 dfe
        public int SpecialAtack = 145;    //とくこう  145 spatk
        public int SpecialDifence = 125;    //とくぼう  125 spdef
        public int Speed = 110;      //すばやさ  110 sp

        //わざこうせい
        public string Waza1 = "エナジーボール";
        public string Waza2 = "ヘドロばくだん";
        public string Waza3 = "じんんつうりき";
        public string Waza4 = "シャドーボール";

        //とくせい
        public string Tokusei = "しぜんかいふく";

    }


    public class Gastrodon : Pokemon
    {
        //ポケモン名　トリトドン
        public string Name = "トリトドン";     //gastodon = トリトドンの英名

        //タイプ
        public string Type1 = "みず";
        public string Type2 = "じめん";

        //ステータス実数値
        public int Hitpoint = 186;
        public int Atack = 103;
        public int Difence = 88;
        public int SpecialAtack = 112;
        public int SpecialDifence = 102;
        public int Speed = 59;

        //わざこうせい
        public string Waza1 = "だくりゅう";
        public string Waza2 = "じしん";
        public string Waza3 = "ヘドロばくだん";
        public string Waza4 = "ストーンエッジ";

        //とくせい
        public string Tokusei = "ねんちゃく";
    }


    public class Lucario : Pokemon
    {
        //ポケモン名
        public string Name = "ルカリオ";

        //タイプ
        public string Type1 = "かくとう";
        public string Type2 = "はがね";

        //ステータス実数値
        public int Hitpoint = 145;
        public int Atack = 130;
        public int Difence = 90;
        public int SpecialAtack = 135;
        public int SpecialDifence = 90;
        public int Speed = 110;

        //わざこうせい
        public string Waza1 = "はどうだん";
        public string Waza2 = "じしん";
        public string Waza3 = "サイコキネシス";
        public string Waza4 = "りゅうのはどう";

        //とくせい
        public string Tokusei = "ふくつのこころ";
    }


    public class Milotic : Pokemon
    {
        //ポケモン名
        public string Name = "ミロカロス";       //milotic=ミロカロスの英名

        //タイプ
        public string Type1 = "みず";

        //ステータス実数値
        public int Hitpoint = 170;
        public int Atack = 80;
        public int Difence = 99;
        public int SpecialAtack = 120;
        public int SpecialDifence = 145;
        public int Speed = 101;

        //わざこうせい
        public string Waza1 = "アクアリング";
        public string Waza2 = "ミラーコート";
        public string Waza3 = "れいとうビーム";
        public string Waza4 = "なみのり";

        //とくせい
        public string Tokusei = "ふしぎなうろこ";
    }


    public class Garchomp : Pokemon
    {
        //ポケモン名
        public string Name = "ガブリアス";      //garchomp=ガブリアスの英名

        //タイプ
        public string Type1 = "ドラゴン";
        public string Type2 = "じめん";

        //ステータス実数値
        public int Hitpoint = 183;
        public int Atack = 150;
        public int Difence = 115;
        public int SpecialAtack = 100;
        public int SpecialDifence = 105;
        public int Speed = 122;

        //わざこうせい
        public string Waza1 = "ドラゴンダイブ";
        public string Waza2 = "じしん";
        public string Waza3 = "かわらわり";
        public string Waza4 = "ギガインパクト";

        //とくせい
        public string Tokusei = "すながくれ";
    }



    //味方ポケモン

    public class charJolteon : Pokemon
    {

        //ポケモン名
        public string Name = "サンダース";      //jolteon=サンダースの英名

        //ステータス実数値
        public int Hitpoint = 140;
        public int Atack = 85;
        public int Difence = 80;
        public int SpecialAtack = 130;
        public int SpecialDifence = 115;
        public int Speed = 150;

        //わざこうせい
        public string Waza1 = "かみなり";
        public string Waza2 = "でんじは";
        public string Waza3 = "１０まんボルト";
        public string Waza4 = "シャドーボール";

        //とくせい
        public string Tokusei = "ちくでん";
    }


    public class charLaplace : Pokemon
    {

        //ポケモン名
        public string Name = "ラプラス";      //Laplace=ラプラスの英名

        //ステータス実数値
        public int Hitpoint = 205;
        public int Atack = 105;
        public int Difence = 100;
        public int SpecialAtack = 105;
        public int SpecialDifence = 115;
        public int Speed = 80;

        //わざこうせい
        public string Waza1 = "ぜったいれいど";
        public string Waza2 = "れいとうビーム";
        public string Waza3 = "じしん";
        public string Waza4 = "じわれ";

        //とくせい
        public string Tokusei = "ちょすい";
    }


    public class chaHydreigon : Pokemon
    {
        //ポケモン名
        public string Name = "サザンドラ";      //Hydreigon=サザンドラの英名

        //ステータス実数値
        public int Hitpoint = 167;
        public int Atack = 125;
        public int Difence = 110;
        public int SpecialAtack = 145;
        public int SpecialDifence = 110;
        public int Speed = 118;

        //わざこうせい
        public string Waza1 = "あくのはどう";
        public string Waza2 = "かえんほうしゃ";
        public string Waza3 = "ラスターカノン";
        public string Waza4 = "りゅうせいぐん";

        //とくせい
        public string Tokusei = "ふゆう";
    }

    public class charAzumasill : Pokemon
    {

        //ポケモン名
        public string Name = "マリルリ";      //Azumaeill=マリルリの英名

        //ステータス実数値
        public int Hitpoint = 175;
        public int Atack = 70;
        public int Difence = 100;
        public int SpecialAtack = 80;
        public int SpecialDifence = 100;
        public int Speed = 70;

        //わざこうせい
        public string Waza1 = "はらだいこ";
        public string Waza2 = "アクアテール";
        public string Waza3 = "ハイドロポンプ";
        public string Waza4 = "アクアリング";

        //とくせい
        public string Tokusei = "あついしぼう";
    }



    public class charScizor : Pokemon
    {

        //ポケモン名
        public string Name = "ハッサム";      //Scizor=ハッサムの英名

        //ステータス実数値
        public int Hitpoint = 145;
        public int Atack = 150;
        public int Difence = 120;
        public int SpecialAtack = 75;
        public int SpecialDifence = 100;
        public int Speed = 85;

        //わざこうせい
        public string Waza1 = "バレットパンチ";
        public string Waza2 = "つるぎのまい";
        public string Waza3 = "インファイト";
        public string Waza4 = "つばめがえし";

        //とくせい
        public string Tokusei = "テクニシャン";
    }


    public class charDoragonite : Pokemon
    {
        //ポケモン名
        public string Name = "カイリュー";      //Doragonite=カイリューの英名

        //ステータス実数値
        public int Hitpoint = 166;
        public int Atack = 154;
        public int Difence = 115;
        public int SpecialAtack = 120;
        public int SpecialDifence = 120;
        public int Speed = 100;

        //わざこうせい
        public string Waza1 = "しんそく";
        public string Waza2 = "じしん";
        public string Waza3 = "はねやすめ";
        public string Waza4 = "アイアンヘッド";

        //とくせい
        public string Tokusei = "マルチスケイル";

        //もちもの
        public string Item = "たべのこし";
    }
}
