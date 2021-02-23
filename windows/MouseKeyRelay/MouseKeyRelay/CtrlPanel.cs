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
            keyboard = new Keyboard();
            mouse = new Mouse(int.Parse(ConfigurationManager.AppSettings.Get("mouseSpeed")));
        }

        private void connectCOM()
        {
            try {
                debug.Text = "";
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

            } catch (Exception ex) {
                debug.Text = ex.StackTrace;
            }
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

            // 入力済みの修飾キーであればスキップ
            if (keyboard.isValidedModifierKeys(key)) {
                return;
            }
            // 修飾キーの入力状態を反映
            shiftStatus.Checked = (((int)e.Modifiers & 0x10000) != 0);
            ctrlStatus.Checked = (((int)e.Modifiers & 0x20000) != 0);
            altStatus.Checked = (((int)e.Modifiers & 0x40000) != 0);

            // 入力キーを転送する
            int outKey = keyboard.getChar(key, (int)e.Modifiers);
            if (serialConnector.IsOpen && outKey > 0)
            {
                serialConnector.Write(outKey+ ";");
            }

            outputKey.Text = (int)e.Modifiers + " : " + key + " : " + e.KeyCode.ToString() + "out : " + outKey;
            cboxKeyInput.Checked = true;
            inputKey.Text = "";
        }

        void keyUp(object sender, KeyEventArgs e)
        {
            int key = (int)e.KeyCode;
            if (serialConnector.IsOpen &&  keyboard.isValidedModifierKeys(key))
            {
                // 入力状態の修飾キーの押下状態を解除する
                serialConnector.Write(keyboard.getChar((int)e.KeyCode, (int)e.Modifiers, false) + 3 + ";");
            }
        }

        private void CtrlPanel_Load(object sender, EventArgs e)
        {
            // シリアル接続
            connectCOM();
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
            /*
            //　パネルからはみ出した場合は前回の位置に補正
            if (x < 0 || x > mousePanel.Size.Width)
            {
                x = _prePoint.Value.X;
            }
            if (y < 0 || y > mousePanel.Size.Height)
            {
                y = _prePoint.Value.Y;
            }
            */
            if (serialConnector.IsOpen)
            {
                string cmd = mouse.mouseMove(new Point(x, y), mousePanel.Size.Width, mousePanel.Size.Height);
                if (cmd != null)
                { 
                    serialConnector.Write(cmd);
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
