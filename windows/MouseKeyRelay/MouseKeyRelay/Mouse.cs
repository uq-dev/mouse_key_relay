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
        private bool isDraging = false;
        private bool isMoving = false;
        // private Point? _diffPoint = null;

        private Point? prePoint = null;
        private int mouseSpeed = 0;

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

        public Mouse(int mouseSpeed) {
            this.mouseSpeed = mouseSpeed;
            this.prePoint = new Point(0, 0);
        }
        public void mouseDown(Point point)
        {
            isDraging = true;
            prePoint = point;
        }
        public void mouseUp()
        {
            isDraging = false;
            isMoving = false;
        }
        public void mouseMove(Point point, int panelWidth, int panelHeight, ref int codeX, ref int codeY)
        {
            if (!isDraging)
            {
                return;
            }
            isMoving = true;

            int x = point.X;
            int y = point.Y;
            //　パネルからはみ出した場合は前回の位置に補正
            if (x < 0 || x > panelWidth)
            {
                x = prePoint.Value.X;
            }
            if (y < 0 || y > panelHeight)
            {
                y = prePoint.Value.Y;
            }
            int diffX = prePoint.Value.X - x;
            int diffY = prePoint.Value.Y - y;

            // マウス移動距離を計算して転送する
            if (prePoint.Value.X - x < 0)
            {
                codeX = 0x600 + Math.Abs(diffX) * mouseSpeed;
            }
            if (prePoint.Value.X - x > 0)
            {
                codeX = 0x680 + Math.Abs(diffX) * mouseSpeed;
            }
            if (prePoint.Value.Y - y < 0)
            {
                codeY = 0x700 + Math.Abs(diffY) * mouseSpeed;
            }
            if (prePoint.Value.Y -y > 0)
            {
                codeY = 0x780 + Math.Abs(diffY) * mouseSpeed;
            }

            // 現在位置を更新
            prePoint = point;
        }
        public int mouseBtnClick(MouseButtons btn) {
            // マウスクリック
            if (isMoving || !btnCmdDicClick.ContainsKey(btn))
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
