using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.IO.Ports;
using System.Configuration;

namespace MouseKeyRelay
{
    public partial class CtrlPanel : Form
    {
        private SerialPort serialConnector;
        private Keyboard keyboard;
        private Mouse mouse;


        public CtrlPanel()
        {
            InitializeComponent();
            serialConnector = new SerialPort();

        }

        private void connectCOM()
        {
            if (serialConnector.IsOpen)
            {
                serialConnector.Close();
            }
            serialConnector.BaudRate = int.Parse(ConfigurationManager.AppSettings.Get("comBaudRate"));
            serialConnector.Parity = (Parity)int.Parse(ConfigurationManager.AppSettings.Get("comParity"));
            serialConnector.DataBits = int.Parse(ConfigurationManager.AppSettings.Get("comDataBits"));
            serialConnector.StopBits = (StopBits)int.Parse(ConfigurationManager.AppSettings.Get("comStopBits"));
            serialConnector.Handshake = (Handshake)int.Parse(ConfigurationManager.AppSettings.Get("comHandshake"));
            serialConnector.PortName = ConfigurationManager.AppSettings.Get("comPortName");

            serialConnector.Open();
        }

        private void inputStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (cboxKeyInput.Checked)
            {
                KeyDown += new KeyEventHandler(keyDown);
                KeyUp += new KeyEventHandler(keyUp);
                inputKey.Text = "please input";
            }
            else
            {
                KeyDown -= new KeyEventHandler(keyDown);
                KeyUp -= new KeyEventHandler(keyUp);
                inputKey.Text = "";
            }
        }

        void keyDown(object sender, KeyEventArgs e)
        {
            int key = (int)e.KeyCode;
            int mod = (int)e.Modifiers;
            /*
            // 入力済みの修飾キーであればスキップ
            if (keyboard.isValidedModifierKeys(key)) {
                return;
            }
            */
            // 修飾キーの入力状態を反映
            if ((mod & 0x1000) > 0 || (keyboard.getModifierFromKeys(key) & 0x1000) > 0)
            {
                shiftStatus.Checked = true;
            }
            if ((mod & 0x2000) > 0 || (keyboard.getModifierFromKeys(key) & 0x2000) > 0)
            {
                ctrlStatus.Checked = true;
            }
            if ((mod & 0x4000) > 0 || (keyboard.getModifierFromKeys(key) & 0x4000) > 0)
            {
                altStatus.Checked = true;
            }

            // 入力キーを転送する
            int outKey = keyboard.getChar(key);
            if (serialConnector.IsOpen && outKey > 0)
            {
                serialConnector.Write(outKey+ ";");
            }

            outputKey.Text = mod + " : " + key + " : " + e.KeyCode.ToString() + " out : " + outKey;
            cboxKeyInput.Checked = true;
            inputKey.Text = "";
        }

        void keyUp(object sender, KeyEventArgs e)
        {
            int key = (int)e.KeyCode;

            int outKey = 0;
            int mod = keyboard.getModifierFromKeys(key);
            if (mod > 0) {
                if ((keyboard.getModifierFromKeys(key) & 0x1000) > 0)
                {
                    shiftStatus.Checked = false;
                }
                if ((keyboard.getModifierFromKeys(key) & 0x2000) > 0)
                {
                    ctrlStatus.Checked = false;
                }
                if ((keyboard.getModifierFromKeys(key) & 0x4000) > 0)
                {
                    altStatus.Checked = false;
                }
                outKey = keyboard.getChar((int)e.KeyCode, false) + 3;
                outputKey.Text = mod + " : " + key;
            }
            // PrintScreenキーはkeyUpのタイミングでないとキャッチしないのでここで拾う
            if (e.KeyCode == Keys.PrintScreen) {
                outKey = keyboard.getChar((int)e.KeyCode, /*(int)e.Modifiers,*/ false);
                if (serialConnector.IsOpen)
                {
                    serialConnector.Write(outKey + ";");
                }
                inputKey.Text = "PrintScreen";
                outputKey.Text = mod + " : " + key + " : " + e.KeyCode.ToString() + " out : " + outKey;
            }
            if (serialConnector.IsOpen && outKey > 0)
            {
                serialConnector.Write(outKey + ";");
            }
        }

        private void CtrlPanel_Load(object sender, EventArgs e)
        {
            try
            {
                keyboard = new Keyboard();
                mouse = new Mouse(int.Parse(ConfigurationManager.AppSettings.Get("mouseSpeed")));
                // シリアル接続
                connectCOM();
            }
            catch (Exception ex)
            {
                debug.Text = ex.StackTrace;
            }
        }

        private void mousePanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (serialConnector.IsOpen) {
                int cmdCode = mouse.mouseBtnClick(e.Button);
                if (cmdCode > 0) {
                    serialConnector.Write(cmdCode + ";");
                }
            }
        }

        private void mousePanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (serialConnector.IsOpen)
            {
                int cmdCode = mouse.mouseBtnClick(e.Button);
                if (cmdCode > 0)
                {
                    serialConnector.Write(cmdCode + ";");
                    serialConnector.Write(cmdCode + ";");
                }
            }
        }

        private void mousePanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            Cursor.Current = Cursors.Hand;

            mouse.mouseDown(e.Location);
        }

        private void mousePanel_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;

            if (serialConnector.IsOpen)
            {
                int codeX = 0, codeY = 0;
                mouse.mouseMove(new Point(x, y), mousePanel.Size.Width, mousePanel.Size.Height, ref codeX, ref codeY);
                if (codeX > 0)
                { 
                    serialConnector.Write(codeX + ";");
                }
                if (codeY > 0)
                {
                    serialConnector.Write(codeY + ";");
                }
            }
        }

        private void mousePanel_MouseUp(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Default;

            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            mouse.mouseUp();
        }

        private void cboxMouseLeft_CheckedChanged(object sender, EventArgs e)
        {

            if (serialConnector.IsOpen)
            {
                if (cboxMouseLeft.Checked)
                {
                    serialConnector.Write(mouse.mouseBtnPush(MouseButtons.Left) + ";");
                }
                else
                {
                    serialConnector.Write(mouse.mouseBtnRelease(MouseButtons.Left) + ";");
                }
                
            }
        }

        private void cboxMouseRight_CheckedChanged(object sender, EventArgs e)
        {
            if (serialConnector.IsOpen)
            {
                if (cboxMouseRight.Checked)
                {
                    serialConnector.Write(mouse.mouseBtnPush(MouseButtons.Right) + ";");
                }
                else
                {
                    serialConnector.Write(mouse.mouseBtnRelease(MouseButtons.Right) + ";");
                }
            }
        }
    }
}
