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
            this.outputKey.TabStop = false;
            // 
            // debug
            // 
            this.debug.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.debug.Location = new System.Drawing.Point(11, 246);
            this.debug.Multiline = true;
            this.debug.Name = "debug";
            this.debug.ReadOnly = true;
            this.debug.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.debug.Size = new System.Drawing.Size(217, 49);
            this.debug.TabIndex = 7;
            this.debug.TabStop = false;
            // 
            // mousePanel
            // 
            this.mousePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mousePanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.mousePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mousePanel.Location = new System.Drawing.Point(11, 103);
            this.mousePanel.MinimumSize = new System.Drawing.Size(218, 127);
            this.mousePanel.Name = "mousePanel";
            this.mousePanel.Size = new System.Drawing.Size(218, 127);
            this.mousePanel.TabIndex = 10;
            this.mousePanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mousePanel_MouseClick);
            this.mousePanel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mousePanel_MouseDoubleClick);
            this.mousePanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mousePanel_MouseDown);
            this.mousePanel.MouseLeave += new System.EventHandler(this.mousePanel_MouseLeave);
            this.mousePanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mousePanel_MouseMove);
            this.mousePanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mousePanel_MouseUp);
            this.mousePanel.Resize += new System.EventHandler(this.mousePanel_Resize);
            // 
            // CtrlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 307);
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
            this.Opacity = 0.5D;
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
    }
}

