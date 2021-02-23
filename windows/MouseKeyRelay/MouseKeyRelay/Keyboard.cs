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
              {modifierKeys.MOD_SHIFT, 0x10000},
              {modifierKeys.MOD_CTRL, 0x20000},
              {modifierKeys.MOD_ALT, 0x40000}
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

                    int keyCode, shift, outKeyCode;
                    if (int.TryParse(values[0], out keyCode) &&
                        int.TryParse(values[1], out shift) &&
                        int.TryParse(values[2], out outKeyCode) &&
                        keyCode > 0
                        )
                    {
                        if (shift == 1)
                        {
                            keyMappingsShift.Add(keyCode, outKeyCode);
                        }
                        else
                        {
                            keyMappings.Add(keyCode, outKeyCode);
                        }
                    }
                    else
                    {
                        // 
                    }
                }
            }
        }
        public int getChar(int inKey, int modifiers, bool keyPush = true)
        {

            int outKey = 0;
            // 入力済みの修飾キーを再度入力しようとしている場合は無効値を返す
            if (keyPush && isValidedModifierKeys(inKey))
            {
                return 0;
            }

            if (
                (((modifiers & modifierDic[modifierKeys.MOD_SHIFT]) != 0) || (getModifierFromKeys(inKey) == modifierDic[modifierKeys.MOD_SHIFT]))
                && keyMappingsShift.ContainsKey(inKey))
            {
                // (Shiftが押下状態 or 入力キーがShift) and Shift入力状態のキーマッピング定義が存在する
                outKey = keyMappingsShift[inKey];
            }
            if (outKey == 0 && keyMappings.ContainsKey(inKey))
            {
                // キーマッピング定義が存在する
                outKey = keyMappings[inKey];
            }

            if (keyPush)
            {
                 if (modifiers > 0 && ((this.modifier & modifiers) == 0)) 
                // if ((this.modifier & modifiers) == 0)
                {
                    // 未入力状態の修飾キーが別のキーとともに入力
                    this.modifier += modifiers;
                }
                else if ((this.modifier & getModifierFromKeys(inKey)) == 0)
                {
                    // 未入力状態の修飾キーが入力
                    this.modifier += getModifierFromKeys(inKey);
                }
            }
            else {
                 if (modifiers > 0 && ((this.modifier & modifiers) >= 0))
                 // if ((this.modifier & modifiers) >= 0)
                {
                    // 未入力状態の修飾キーが別のキーとともに入力
                    this.modifier -= modifiers;
                }
                else if ((this.modifier & getModifierFromKeys(inKey)) >= 0)
                {
                    this.modifier -= getModifierFromKeys(inKey);
                }
            }

            return outKey;
        }
        public bool isValidedModifierKeys(int inKey) {
            if (((this.modifier & 0x10000) != 0) && inKey == (int)modifierKeys.MOD_SHIFT)
            {
                return true;
            }
            if (((this.modifier & 0x20000) != 0) && inKey == (int)modifierKeys.MOD_CTRL)
            {
                return true;
            }
            if (((this.modifier & 0x40000) != 0) && inKey == (int)modifierKeys.MOD_ALT)
            {
                return true;
            }
            return false;
        }
        public int getModifierFromKeys(int inKey)
        {
            if (Enum.IsDefined(typeof(modifierKeys), inKey))
            {
                return modifierDic[(modifierKeys)inKey];
            }
            else {
                return 0;
            }
        }
    }
}
