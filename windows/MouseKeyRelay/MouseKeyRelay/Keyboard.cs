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
        private enum modifierKeys {
            MOD_SHIFT = 16,
            MOD_CTRL = 17,
            MOD_ALT = 18
        };
        private Dictionary<modifierKeys, int> modifierDic = new Dictionary<modifierKeys, int>()
        {
              {modifierKeys.MOD_SHIFT, 0x1000},
              {modifierKeys.MOD_CTRL, 0x2000},
              {modifierKeys.MOD_ALT, 0x4000}
        };

        private const string charmappinng = @"keymapping.csv";
        private Dictionary<int, int> keyMappings;
        private Dictionary<int, int> keyMappingsShift;
        private int modifier;

        public Keyboard() {
            this.keyMappings = new Dictionary<int, int>();
            this.keyMappingsShift = new Dictionary<int, int>();
            this.modifier = 0;

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
                        throw new Exception("キーマッピングファイルのフォーマットエラー");
                    }
                }
            }
        }
        public int getChar(int inKey, bool keyPush = true)
        {
            int outKey = 0;
             if (keyMappings.ContainsKey(inKey))
            {
                // キーマッピング定義が存在する
                outKey = keyMappings[inKey];
            }

            // 修飾キーであれば入力状態を更新する
            int mod = getModifierFromKeys(inKey);
            if (mod > 0) {
                if (keyPush && !isValidedModifierKeys(inKey))
                {
                    this.modifier += mod;
                }
                if (!keyPush && isValidedModifierKeys(inKey))
                {
                    this.modifier -= mod;
                }
            }
 
            return outKey;
        }
        public int getModifierFromKeys(int inKey)
        {
            if (Enum.IsDefined(typeof(modifierKeys), inKey))
            {
                return modifierDic[(modifierKeys)inKey];
            }
            else
            {
                return 0;
            }
        }
        public bool isValidedModifierKeys(int inKey) {
            if (((this.modifier & 0x1000) != 0) && inKey == (int)modifierKeys.MOD_SHIFT)
            {
                // Shift
                return true;
            }
            if (((this.modifier & 0x2000) != 0) && inKey == (int)modifierKeys.MOD_CTRL)
            {
                // Ctrl
                return true;
            }
            if (((this.modifier & 0x4000) != 0) && inKey == (int)modifierKeys.MOD_ALT)
            {
                // Alt
                return true;
            }
            return false;
        }

    }
}
