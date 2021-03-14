using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Input;
using System.Windows.Forms;


namespace MouseKeyRelay
{
    class Keyboard
    {
        private const string charmappinng = @"keymapping.csv";
        private Dictionary<int, int> keyMappings;

        public Keyboard() {
            this.keyMappings = new Dictionary<int, int>();

            StreamReader sr = new StreamReader(charmappinng);
            {
                // 末尾まで繰り返す
                while (!sr.EndOfStream)
                {
                    // CSVファイルの一行を読み込む
                    string line = sr.ReadLine();
                    // 読み込んだ一行をカンマ毎に分けて配列に格納する
                    string[] values = line.Split('\t');

                    int keyCode, outKeyCode;
                    if (int.TryParse(values[0], out keyCode) &&
                        int.TryParse(values[1], out outKeyCode))
                    {
                        if (keyCode > 0 && !keyMappings.ContainsKey(keyCode))
                        {
                            keyMappings.Add(keyCode, outKeyCode);
                        }
                    }
                    else
                    {
                        throw new Exception("キーマッピングファイルの読み込みエラー");
                    }
                }
            }
        }
        public int getChar(Keys inKey)
        {
            int outKey = 0; 
             if (keyMappings.ContainsKey((int)inKey))
            {
                // キーマッピング定義が存在する
                outKey = keyMappings[(int)inKey];
            }
            return outKey;
        }
     }
}
