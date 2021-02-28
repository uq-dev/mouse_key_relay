﻿using System;
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
        private int modifier;

        public Keyboard() {
            this.keyMappings = new Dictionary<int, int>();
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

        public int getChar(Keys inKey, bool keyPush = true)
        {
            int outKey = 0; 
             if (keyMappings.ContainsKey((int)inKey))
            {
                // キーマッピング定義が存在する
                outKey = keyMappings[(int)inKey];
            }

            // 修飾キーであれば入力状態を更新する
            int mod = getModifierFromKeys(inKey);
            if (mod > 0)
            {
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

        public int getModifierFromKeys(Keys inKey)
        {
            switch (inKey)
            {
                case Keys.ShiftKey:
                case Keys.LShiftKey:
                case Keys.RShiftKey:
                    return (int)Keys.Shift;
                case Keys.ControlKey:
                case Keys.LControlKey:
                case Keys.RControlKey:
                    return (int)Keys.Control;
                case Keys.Menu:
                case Keys.LMenu:
                case Keys.RMenu:
                    return (int)Keys.Alt;
            }
            return 0;
        }

        public bool isValidedModifierKeys(Keys inKey)
        {
            switch (inKey)
            {
                case Keys.ShiftKey:
                case Keys.LShiftKey:
                case Keys.RShiftKey:
                    return ((this.modifier & (int)Keys.Shift) != 0);
                case Keys.ControlKey:
                case Keys.LControlKey:
                case Keys.RControlKey:
                    return ((this.modifier & (int)Keys.Control) != 0);
                case Keys.Menu:
                case Keys.LMenu:
                case Keys.RMenu:
                    return ((this.modifier & (int)Keys.Alt) != 0);
            }
            return false;
        }

    }
}
