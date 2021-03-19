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
using OpenCvSharp;


namespace MouseKeyRelay
{
    public partial class CtrlPanel : Form
    {
        private SerialPort serialConnector;
        private Keyboard keyboard;
        private Mouse mouse;

        Mat frame;
        VideoCapture capture;
        Bitmap bmp;
        Graphics graphic;



        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CtrlPanel()
        {
            InitializeComponent();
            serialConnector = new SerialPort();

            capture = new VideoCapture(0);
            if (!capture.IsOpened())
            {
                MessageBox.Show("camera was not found!");
                this.Close();
            }
            capture.FrameWidth = pictureBox1.Width;
            capture.FrameHeight = pictureBox1.Height;

            //取得先のMat作成
            frame = new Mat(pictureBox1.Height, pictureBox1.Width, MatType.CV_8UC3);

            //表示用のBitmap作成
            bmp = new Bitmap(frame.Cols, frame.Rows, (int)frame.Step(), System.Drawing.Imaging.PixelFormat.Format24bppRgb, frame.Data);

            //PictureBoxを出力サイズに合わせる
            pictureBox1.Width = frame.Cols;
            pictureBox1.Height = frame.Rows;

            //描画用のGraphics作成
            graphic = pictureBox1.CreateGraphics();

            //画像取得スレッド開始
            backgroundWorker1.RunWorkerAsync();


        }
        /// <summary>
        /// フォームの初期化
        /// </summary>
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
                debug.Text = ex.Message;
            }
        }




        /// <summary>
        /// COM接続。パラメータはapp.configから読み込む。
        /// </summary>
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
            if (serialConnector.IsOpen)
            {
                serialConnector.Write("0;");
            }
        }

        /// <summary>
        /// キー入力受付の開始/終了
        /// チェックをオンにするとキー入力をリレーする。
        /// </summary>
        private void inputStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (cboxKeyInput.Checked)
            {
                KeyDown += new KeyEventHandler(keyDown);
                KeyUp += new KeyEventHandler(keyUp);
                inputKey.Text = "please input";
                this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.mousePanel_MouseWheel);
            }
            else
            {
                KeyDown -= new KeyEventHandler(keyDown);
                KeyUp -= new KeyEventHandler(keyUp);
                this.MouseWheel -= new System.Windows.Forms.MouseEventHandler(this.mousePanel_MouseWheel);
                inputKey.Text = "";
            }
        }

        /// <summary>
        /// キーボードのキーダウン
        /// </summary>
        void keyDown(object sender, KeyEventArgs e)
        {
            keyInput(e.KeyCode, true);
        }
        /// <summary>
        /// キーボードのキーアップ
        /// </summary>
        void keyUp(object sender, KeyEventArgs e)
        {
            keyInput(e.KeyCode, false);
        }
        private void keyInput(Keys key, bool pushed) {
            // 修飾キーの入力状態を反映する
            switch (key)
            {
                case Keys.ShiftKey:
                case Keys.LShiftKey:
                case Keys.RShiftKey:
                    shiftStatus.Checked = pushed;
                    break;
                case Keys.ControlKey:
                case Keys.LControlKey:
                case Keys.RControlKey:
                    ctrlStatus.Checked = pushed;
                    break;
                case Keys.Menu:
                case Keys.LMenu:
                case Keys.RMenu:
                    altStatus.Checked = pushed;
                    break;
            }
            // 転送キーを取得
            int outKey = keyboard.getChar(key);
            if (serialConnector.IsOpen && outKey > 0)
            {
                if (pushed)
                {
                    // キーダウン
                    serialConnector.Write(outKey + ";");
                    outputKey.Text = (int)key + " : " + key.ToString() + " out : " + outKey;
                    cboxKeyInput.Checked = true;
                    inputKey.Text = "";
                } else {
                    // キーアップ
                    // PrintScreenキーはkeyUpのタイミングでないとキャッチしないので入力もここで行う
                    if (key == Keys.PrintScreen)
                    {
                        serialConnector.Write(outKey + ";");
                        outputKey.Text = (int)key + " : " + key.ToString() + " out : " + outKey;
                    }
                    serialConnector.Write(outKey + 0x200 + ";");
                }
            }
        }
        /// <summary>
        /// マウスクリック
        /// </summary>
        private void mousePanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (serialConnector.IsOpen) {
                int cmdCode = mouse.mouseBtnClick(e.Button);
                if (cmdCode > 0) {
                    serialConnector.Write(cmdCode + ";");
                }
            }
        }
        /// <summary>
        /// マウスダブルクリック
        /// </summary>
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
        /// <summary>
        /// トラックパッド領域でのマウスボタンダウン
        /// </summary>
        private void mousePanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (serialConnector.IsOpen)
            {
                if (e.Button != MouseButtons.Left)
                {
                    return;
                }
                Cursor.Current = Cursors.Hand;

                mouse.mouseDown(e.Location);
            }
        }
        /// <summary>
        /// トラックパッド領域でのマウス移動
        /// </summary>
        private void mousePanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (serialConnector.IsOpen)
            {
                int codeX = 0, codeY = 0;
                mouse.mouseMove(new System.Drawing.Point(e.X, e.Y), mousePanel.Size.Width, mousePanel.Size.Height, ref codeX, ref codeY);
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
        /// <summary>
        /// トラックパッド領域でのマウスボタンアップ
        /// </summary>
        private void mousePanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (serialConnector.IsOpen)
            {
                Cursor.Current = Cursors.Default;

                if (e.Button != MouseButtons.Left)
                {
                    return;
                }
                mouse.mouseUp();
            }
        }
        /// <summary>
        /// マウス左ボタン
        /// </summary>
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
        /// <summary>
        /// マウス右ボタン
        /// </summary>
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
        /// <summary>
        /// マウス中央ボタン
        /// </summary>
        private void cboxMouseMid_CheckedChanged(object sender, EventArgs e)
        {
            if (serialConnector.IsOpen)
            {
                if (cboxMouseMid.Checked)
                {
                    serialConnector.Write(mouse.mouseBtnPush(MouseButtons.Middle) + ";");
                }
                else
                {
                    serialConnector.Write(mouse.mouseBtnRelease(MouseButtons.Middle) + ";");
                }
            }
        }
        /// <summary>
        /// マウスホイールイベント
        /// </summary>
        private void mousePanel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (serialConnector.IsOpen)
            {
                serialConnector.Write(mouse.mouseWheel(e.Delta) + ";");
                //outputKey.Text = "" + mouse.mouseWheel(e.Delta);
            }
        }

        private void CtrlPanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            //スレッドの終了を待機
            backgroundWorker1.CancelAsync();
            while (backgroundWorker1.IsBusy)
                Application.DoEvents();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = (BackgroundWorker)sender;

            while (!backgroundWorker1.CancellationPending)
            {
                //画像取得
                capture.Grab();
                OpenCvSharp.Internal.NativeMethods.videoio_VideoCapture_operatorRightShift_Mat(capture.CvPtr, frame.CvPtr);
                bw.ReportProgress(0);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //描画
            graphic.DrawImage(bmp, 0, 0, frame.Cols, frame.Rows);
        }
    }
}
