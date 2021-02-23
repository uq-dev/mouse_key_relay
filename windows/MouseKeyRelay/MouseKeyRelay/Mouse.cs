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
              {MouseButtons.Left, 0x280},
              {MouseButtons.Right, 0x300},
              {MouseButtons.Middle, 0x380}
            };
        private Dictionary<MouseButtons, int> btnCmdDicClick = new Dictionary<MouseButtons, int>()
            {
              {MouseButtons.Left, 0x281},
              {MouseButtons.Right, 0x301},
              {MouseButtons.Middle, 0x381}
            };
        private Dictionary<MouseButtons, int> btnCmdDicPush = new Dictionary<MouseButtons, int>()
            {
              {MouseButtons.Left, 0x282},
              {MouseButtons.Right, 0x302},
              {MouseButtons.Middle, 0x382}
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
        public string mouseMove(Point point, int panelWidth, int panelHeight)
        {
            if (!isDraging)
            {
                return null;
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


            string command = null;
            int commandCode = 0;
            // マウス移動距離を計算して転送する
            if (prePoint.Value.X - x < 0)
            {
                commandCode += 0x400 + Math.Abs(prePoint.Value.X - x) * mouseSpeed;
            }
            if (prePoint.Value.X - x > 0)
            {
                commandCode += 0x800 + Math.Abs(prePoint.Value.X - x) * mouseSpeed;
            }
            if (prePoint.Value.Y - y < 0)
            {
                commandCode += 0x1000 + Math.Abs(prePoint.Value.Y - y) * mouseSpeed;
            }
            if (prePoint.Value.Y -y > 0)
            {
                commandCode += 0x2000 + Math.Abs(prePoint.Value.Y - y) * mouseSpeed;
            }
            command = commandCode + ";";
            // 現在位置を更新
            prePoint = point;
            return command;
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
    }
}
