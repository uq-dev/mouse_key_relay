using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace MouseKeyRelay
{
    class Mouse
    {
        private Point? prePoint = null;

        private double displayLateX;
        private double displayLateY;

        private double guestWidth;
        private double guestHeight;

        private int maxDiff = 0x7F; // 一度に入力できる移動量の最大値

        private Dictionary<MouseButtons, int> btnCmdDicRelease = new Dictionary<MouseButtons, int>()
            {
              {MouseButtons.Left, 0x400},
              {MouseButtons.Right, 0x420},
              {MouseButtons.Middle, 0x440}
            };
        private Dictionary<MouseButtons, int> btnCmdDicClick = new Dictionary<MouseButtons, int>()
            {
              {MouseButtons.Left, 0x401},
              {MouseButtons.Right, 0x421},
              {MouseButtons.Middle, 0x441}
            };
        private Dictionary<MouseButtons, int> btnCmdDicPush = new Dictionary<MouseButtons, int>()
            {
              {MouseButtons.Left, 0x402},
              {MouseButtons.Right, 0x422},
              {MouseButtons.Middle, 0x442}
            };
        public Mouse(double hostWidth, double hostHeight, double guestWidth, double guestHeight)
        {
            this.guestWidth = guestWidth;
            this.guestHeight = guestHeight;

            this.prePoint = new Point(0, 0);

            this.displayLateX = guestWidth / hostWidth;
            this.displayLateY = guestHeight / hostHeight;

        }
        public void setPrePoint(Point point) {
            this.prePoint = point;
        }
        public void setPanelSize(double hostWidth, double hostHeight)
        {
            this.displayLateX = guestWidth / hostWidth;
            this.displayLateY = guestHeight / hostHeight;
        }


        public List<int> mousePointReset()
        {
            List<int> result = new List<int>();
            for (int i = 0; i < guestWidth; i += maxDiff)
            {
                result.Add(0x680 + maxDiff);
            }
            for (int i = 0; i < guestHeight; i += maxDiff)
            {
                result.Add(0x780 + maxDiff);
            }
            prePoint = new Point(0, 0);
            return result;
        }

        public List<int> mouseMove(Point point)
        {
            List<int> result = new List<int>();

            int x = point.X;
            int y = point.Y;

            int diffX = prePoint.Value.X - x;
            int diffY = prePoint.Value.Y - y;

            // マウス移動距離を計算して転送する
            if (diffX < 0)
            {
                // 右移動
                int i = (int)Math.Round(Math.Abs(diffX) * displayLateX);

                while (i > maxDiff)
                {
                    result.Add(0x600 + maxDiff);
                    i -= maxDiff;
                }
                result.Add(0x600 + i);
            }
            if (diffX > 0)
            {
                // 左移動
                int i = (int)Math.Round(Math.Abs(diffX) * displayLateX);

                while (i > maxDiff)
                {
                    result.Add(0x680 + maxDiff);
                    i -= maxDiff;
                }
                result.Add(0x680 + i);
            }
            if (diffY < 0)
            {
                // 下移動
                int i = (int)Math.Round(Math.Abs(diffY) * displayLateY);

                while (i > maxDiff)
                {
                    result.Add(0x700 + maxDiff);
                    i -= maxDiff;
                }
                result.Add(0x700 + i);
            }
            if (diffY > 0)
            {
                // 上移動
                int i = (int)Math.Round(Math.Abs(diffY) * displayLateY);

                while (i > maxDiff)
                {
                    result.Add(0x780 + maxDiff);
                    i -= maxDiff;
                }
                result.Add(0x780 + i);

            }
            // 現在位置を更新
            prePoint = point;
            return result;
        }


        public int mouseBtnClick(MouseButtons btn) {
            // マウスクリック
            if (!btnCmdDicClick.ContainsKey(btn))
            {
                return 0;
            }
            return btnCmdDicClick[btn];
        }
        public int mouseBtnPush(MouseButtons btn)
        {
            if (!btnCmdDicPush.ContainsKey(btn))
            {
                return 0;
            }
            return btnCmdDicPush[btn];
        }
        public int mouseBtnRelease(MouseButtons btn)
        {
            if (!btnCmdDicRelease.ContainsKey(btn))
            {
                return 0;
            }
            return btnCmdDicRelease[btn];
        }
        public int mouseWheel(int delta)
        {
            int result = 0;
            if (delta > 0)
            {
                result = 0x460 + (Math.Abs(delta) * SystemInformation.MouseWheelScrollLines / 120);
            }
            if (delta < 0)
            {
                result = 0x470 + (Math.Abs(delta) * SystemInformation.MouseWheelScrollLines / 120);
            }
            return result;
        }

    }
}
