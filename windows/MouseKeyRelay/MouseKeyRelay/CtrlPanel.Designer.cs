namespace MouseKeyRelay
{
    partial class CtrlPanel
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.cboxKeyInput = new System.Windows.Forms.CheckBox();
            this.inputKey = new System.Windows.Forms.TextBox();
            this.ctrlStatus = new System.Windows.Forms.CheckBox();
            this.altStatus = new System.Windows.Forms.CheckBox();
            this.shiftStatus = new System.Windows.Forms.CheckBox();
            this.outputKey = new System.Windows.Forms.TextBox();
            this.debug = new System.Windows.Forms.TextBox();
            this.mousePanel = new System.Windows.Forms.Panel();
            this.cboxMouseLeft = new System.Windows.Forms.CheckBox();
            this.cboxMouseRight = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cboxKeyInput
            // 
            this.cboxKeyInput.AutoSize = true;
            this.cboxKeyInput.Location = new System.Drawing.Point(15, 12);
            this.cboxKeyInput.Name = "cboxKeyInput";
            this.cboxKeyInput.Size = new System.Drawing.Size(49, 16);
            this.cboxKeyInput.TabIndex = 0;
            this.cboxKeyInput.TabStop = false;
            this.cboxKeyInput.Text = "input";
            this.cboxKeyInput.UseVisualStyleBackColor = true;
            this.cboxKeyInput.CheckedChanged += new System.EventHandler(this.inputStatus_CheckedChanged);
            // 
            // inputKey
            // 
            this.inputKey.AcceptsReturn = true;
            this.inputKey.AcceptsTab = true;
            this.inputKey.Location = new System.Drawing.Point(11, 41);
            this.inputKey.Name = "inputKey";
            this.inputKey.Size = new System.Drawing.Size(218, 19);
            this.inputKey.TabIndex = 1;
            // 
            // ctrlStatus
            // 
            this.ctrlStatus.AutoSize = true;
            this.ctrlStatus.Enabled = false;
            this.ctrlStatus.Location = new System.Drawing.Point(145, 12);
            this.ctrlStatus.Name = "ctrlStatus";
            this.ctrlStatus.Size = new System.Drawing.Size(41, 16);
            this.ctrlStatus.TabIndex = 2;
            this.ctrlStatus.TabStop = false;
            this.ctrlStatus.Text = "ctrl";
            this.ctrlStatus.UseVisualStyleBackColor = true;
            // 
            // altStatus
            // 
            this.altStatus.AutoSize = true;
            this.altStatus.Enabled = false;
            this.altStatus.Location = new System.Drawing.Point(192, 12);
            this.altStatus.Name = "altStatus";
            this.altStatus.Size = new System.Drawing.Size(37, 16);
            this.altStatus.TabIndex = 3;
            this.altStatus.TabStop = false;
            this.altStatus.Text = "alt";
            this.altStatus.UseVisualStyleBackColor = true;
            // 
            // shiftStatus
            // 
            this.shiftStatus.AutoSize = true;
            this.shiftStatus.Enabled = false;
            this.shiftStatus.Location = new System.Drawing.Point(92, 12);
            this.shiftStatus.Name = "shiftStatus";
            this.shiftStatus.Size = new System.Drawing.Size(47, 16);
            this.shiftStatus.TabIndex = 4;
            this.shiftStatus.TabStop = false;
            this.shiftStatus.Text = "shift";
            this.shiftStatus.UseVisualStyleBackColor = true;
            // 
            // outputKey
            // 
            this.outputKey.Enabled = false;
            this.outputKey.Location = new System.Drawing.Point(11, 67);
            this.outputKey.Name = "outputKey";
            this.outputKey.Size = new System.Drawing.Size(218, 19);
            this.outputKey.TabIndex = 5;
            // 
            // debug
            // 
            this.debug.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.debug.Enabled = false;
            this.debug.Location = new System.Drawing.Point(11, 255);
            this.debug.Multiline = true;
            this.debug.Name = "debug";
            this.debug.Size = new System.Drawing.Size(218, 49);
            this.debug.TabIndex = 7;
            this.debug.TabStop = false;
            // 
            // mousePanel
            // 
            this.mousePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mousePanel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.mousePanel.Location = new System.Drawing.Point(11, 114);
            this.mousePanel.MinimumSize = new System.Drawing.Size(218, 127);
            this.mousePanel.Name = "mousePanel";
            this.mousePanel.Size = new System.Drawing.Size(218, 127);
            this.mousePanel.TabIndex = 10;
            this.mousePanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mousePanel_MouseClick);
            this.mousePanel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mousePanel_MouseDoubleClick);
            this.mousePanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mousePanel_MouseDown);
            this.mousePanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mousePanel_MouseMove);
            this.mousePanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mousePanel_MouseUp);
            // 
            // cboxMouseLeft
            // 
            this.cboxMouseLeft.AutoSize = true;
            this.cboxMouseLeft.Location = new System.Drawing.Point(11, 92);
            this.cboxMouseLeft.Name = "cboxMouseLeft";
            this.cboxMouseLeft.Size = new System.Drawing.Size(105, 16);
            this.cboxMouseLeft.TabIndex = 11;
            this.cboxMouseLeft.Text = "MouseLeftDown";
            this.cboxMouseLeft.UseVisualStyleBackColor = true;
            this.cboxMouseLeft.CheckedChanged += new System.EventHandler(this.cboxMouseLeft_CheckedChanged);
            // 
            // cboxMouseRight
            // 
            this.cboxMouseRight.AutoSize = true;
            this.cboxMouseRight.Location = new System.Drawing.Point(118, 92);
            this.cboxMouseRight.Name = "cboxMouseRight";
            this.cboxMouseRight.Size = new System.Drawing.Size(112, 16);
            this.cboxMouseRight.TabIndex = 12;
            this.cboxMouseRight.Text = "MouseRightDown";
            this.cboxMouseRight.UseVisualStyleBackColor = true;
            this.cboxMouseRight.CheckedChanged += new System.EventHandler(this.cboxMouseRight_CheckedChanged);
            // 
            // CtrlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 316);
            this.Controls.Add(this.cboxMouseRight);
            this.Controls.Add(this.cboxMouseLeft);
            this.Controls.Add(this.mousePanel);
            this.Controls.Add(this.debug);
            this.Controls.Add(this.outputKey);
            this.Controls.Add(this.shiftStatus);
            this.Controls.Add(this.altStatus);
            this.Controls.Add(this.ctrlStatus);
            this.Controls.Add(this.inputKey);
            this.Controls.Add(this.cboxKeyInput);
            this.KeyPreview = true;
            this.Name = "CtrlPanel";
            this.Text = "MouseKeyRelay";
            this.Load += new System.EventHandler(this.CtrlPanel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cboxKeyInput;
        private System.Windows.Forms.TextBox inputKey;
        private System.Windows.Forms.CheckBox ctrlStatus;
        private System.Windows.Forms.CheckBox altStatus;
        private System.Windows.Forms.CheckBox shiftStatus;
        private System.Windows.Forms.TextBox outputKey;
        private System.Windows.Forms.TextBox debug;
        private System.Windows.Forms.Panel mousePanel;
        private System.Windows.Forms.CheckBox cboxMouseLeft;
        private System.Windows.Forms.CheckBox cboxMouseRight;
    }
}

