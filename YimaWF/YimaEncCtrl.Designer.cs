namespace YimaWF
{
    partial class YimaEncCtrl
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YimaEncCtrl));
            this.axYimaEnc = new AxYIMAENCLib.AxYimaEnc();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.CurClientPoint = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.CurGeoPoint = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.CurScale = new System.Windows.Forms.ToolStripStatusLabel();
            this.TargetDataTimer = new System.Windows.Forms.Timer(this.components);
            this.targetContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShowDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowTrackMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TargetCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RadarTimer = new System.Windows.Forms.Timer(this.components);
            this.normalContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShowRadarTargetItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowAISTargetItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowMergeTargetItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowRadarMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowOptLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowAllTrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowSpeedLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowRadarTargetStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowAISTargetStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowMergeTargetStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CancelAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.CancelPoint = new System.Windows.Forms.ToolStripMenuItem();
            this.EndAdd = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.axYimaEnc)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.targetContextMenu.SuspendLayout();
            this.normalContextMenu.SuspendLayout();
            this.addContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // axYimaEnc
            // 
            this.axYimaEnc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.axYimaEnc.Enabled = true;
            this.axYimaEnc.Location = new System.Drawing.Point(0, 0);
            this.axYimaEnc.Name = "axYimaEnc";
            this.axYimaEnc.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axYimaEnc.OcxState")));
            this.axYimaEnc.Size = new System.Drawing.Size(483, 399);
            this.axYimaEnc.TabIndex = 0;
            this.axYimaEnc.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CurClientPoint,
            this.toolStripStatusLabel1,
            this.CurGeoPoint,
            this.toolStripStatusLabel2,
            this.CurScale});
            this.statusStrip1.Location = new System.Drawing.Point(0, 402);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(483, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // CurClientPoint
            // 
            this.CurClientPoint.Name = "CurClientPoint";
            this.CurClientPoint.Size = new System.Drawing.Size(47, 17);
            this.CurClientPoint.Text = "X:0 Y:0";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(164, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // CurGeoPoint
            // 
            this.CurGeoPoint.Name = "CurGeoPoint";
            this.CurGeoPoint.Size = new System.Drawing.Size(61, 17);
            this.CurGeoPoint.Text = "X2:0 Y2:0";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(164, 17);
            this.toolStripStatusLabel2.Spring = true;
            // 
            // CurScale
            // 
            this.CurScale.Name = "CurScale";
            this.CurScale.Size = new System.Drawing.Size(32, 17);
            this.CurScale.Text = "1:10";
            // 
            // TargetDataTimer
            // 
            this.TargetDataTimer.Tick += new System.EventHandler(this.TargetDataTimer_Tick);
            // 
            // targetContextMenu
            // 
            this.targetContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowDetail,
            this.ShowTrackMenuItem,
            this.TargetCenterToolStripMenuItem});
            this.targetContextMenu.Name = "targetContextMenu";
            this.targetContextMenu.Size = new System.Drawing.Size(149, 70);
            // 
            // ShowDetail
            // 
            this.ShowDetail.Name = "ShowDetail";
            this.ShowDetail.Size = new System.Drawing.Size(148, 22);
            this.ShowDetail.Text = "目标详细信息";
            this.ShowDetail.Click += new System.EventHandler(this.ShowDetail_Click);
            // 
            // ShowTrackMenuItem
            // 
            this.ShowTrackMenuItem.Checked = true;
            this.ShowTrackMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowTrackMenuItem.Name = "ShowTrackMenuItem";
            this.ShowTrackMenuItem.Size = new System.Drawing.Size(148, 22);
            this.ShowTrackMenuItem.Text = "显示航迹";
            this.ShowTrackMenuItem.Click += new System.EventHandler(this.ShowTrackMenuItem_Click);
            // 
            // TargetCenterToolStripMenuItem
            // 
            this.TargetCenterToolStripMenuItem.Name = "TargetCenterToolStripMenuItem";
            this.TargetCenterToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.TargetCenterToolStripMenuItem.Text = "目标归心";
            this.TargetCenterToolStripMenuItem.Click += new System.EventHandler(this.TargetCenterToolStripMenuItem_Click);
            // 
            // RadarTimer
            // 
            this.RadarTimer.Tick += new System.EventHandler(this.RadarTimer_Tick);
            // 
            // normalContextMenu
            // 
            this.normalContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowRadarTargetItem,
            this.ShowAISTargetItem,
            this.ShowMergeTargetItem,
            this.ShowRadarMenuItem,
            this.ShowOptLineToolStripMenuItem,
            this.ShowAllTrackToolStripMenuItem,
            this.ShowSpeedLineToolStripMenuItem,
            this.ShowRadarTargetStatusToolStripMenuItem,
            this.ShowAISTargetStatusToolStripMenuItem,
            this.ShowMergeTargetStatusToolStripMenuItem});
            this.normalContextMenu.Name = "normalContextMenu";
            this.normalContextMenu.Size = new System.Drawing.Size(173, 224);
            // 
            // ShowRadarTargetItem
            // 
            this.ShowRadarTargetItem.Checked = true;
            this.ShowRadarTargetItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowRadarTargetItem.Name = "ShowRadarTargetItem";
            this.ShowRadarTargetItem.Size = new System.Drawing.Size(172, 22);
            this.ShowRadarTargetItem.Text = "雷达态势";
            this.ShowRadarTargetItem.Click += new System.EventHandler(this.ShowRadarTargetItem_Click);
            // 
            // ShowAISTargetItem
            // 
            this.ShowAISTargetItem.Checked = true;
            this.ShowAISTargetItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowAISTargetItem.Name = "ShowAISTargetItem";
            this.ShowAISTargetItem.Size = new System.Drawing.Size(172, 22);
            this.ShowAISTargetItem.Text = "AIS态势";
            this.ShowAISTargetItem.Click += new System.EventHandler(this.ShowAISTargetItem_Click);
            // 
            // ShowMergeTargetItem
            // 
            this.ShowMergeTargetItem.Checked = true;
            this.ShowMergeTargetItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowMergeTargetItem.Name = "ShowMergeTargetItem";
            this.ShowMergeTargetItem.Size = new System.Drawing.Size(172, 22);
            this.ShowMergeTargetItem.Text = "融合态势";
            this.ShowMergeTargetItem.Click += new System.EventHandler(this.ShowMergeTargetItem_Click);
            // 
            // ShowRadarMenuItem
            // 
            this.ShowRadarMenuItem.Name = "ShowRadarMenuItem";
            this.ShowRadarMenuItem.Size = new System.Drawing.Size(172, 22);
            this.ShowRadarMenuItem.Text = "雷达扫描线";
            this.ShowRadarMenuItem.Click += new System.EventHandler(this.ShowRadarMenuItem_Click);
            // 
            // ShowOptLineToolStripMenuItem
            // 
            this.ShowOptLineToolStripMenuItem.Checked = true;
            this.ShowOptLineToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowOptLineToolStripMenuItem.Name = "ShowOptLineToolStripMenuItem";
            this.ShowOptLineToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.ShowOptLineToolStripMenuItem.Text = "光电观察范围";
            this.ShowOptLineToolStripMenuItem.Click += new System.EventHandler(this.ShowOptLineToolStripMenuItem_Click);
            // 
            // ShowAllTrackToolStripMenuItem
            // 
            this.ShowAllTrackToolStripMenuItem.Name = "ShowAllTrackToolStripMenuItem";
            this.ShowAllTrackToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.ShowAllTrackToolStripMenuItem.Text = "航迹显示";
            this.ShowAllTrackToolStripMenuItem.Click += new System.EventHandler(this.ShowAllTrackToolStripMenuItem_Click);
            // 
            // ShowSpeedLineToolStripMenuItem
            // 
            this.ShowSpeedLineToolStripMenuItem.Name = "ShowSpeedLineToolStripMenuItem";
            this.ShowSpeedLineToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.ShowSpeedLineToolStripMenuItem.Text = "航首线显示";
            this.ShowSpeedLineToolStripMenuItem.Click += new System.EventHandler(this.ShowSpeedLineToolStripMenuItem_Click);
            // 
            // ShowRadarTargetStatusToolStripMenuItem
            // 
            this.ShowRadarTargetStatusToolStripMenuItem.Checked = true;
            this.ShowRadarTargetStatusToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowRadarTargetStatusToolStripMenuItem.Name = "ShowRadarTargetStatusToolStripMenuItem";
            this.ShowRadarTargetStatusToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.ShowRadarTargetStatusToolStripMenuItem.Text = "雷达目标状态显示";
            this.ShowRadarTargetStatusToolStripMenuItem.Click += new System.EventHandler(this.ShowRadarTargetStatusToolStripMenuItem_Click);
            // 
            // ShowAISTargetStatusToolStripMenuItem
            // 
            this.ShowAISTargetStatusToolStripMenuItem.Checked = true;
            this.ShowAISTargetStatusToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowAISTargetStatusToolStripMenuItem.Name = "ShowAISTargetStatusToolStripMenuItem";
            this.ShowAISTargetStatusToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.ShowAISTargetStatusToolStripMenuItem.Text = "AIS目标状态显示";
            this.ShowAISTargetStatusToolStripMenuItem.Click += new System.EventHandler(this.ShowAISTargetStatusToolStripMenuItem_Click);
            // 
            // ShowMergeTargetStatusToolStripMenuItem
            // 
            this.ShowMergeTargetStatusToolStripMenuItem.Checked = true;
            this.ShowMergeTargetStatusToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowMergeTargetStatusToolStripMenuItem.Name = "ShowMergeTargetStatusToolStripMenuItem";
            this.ShowMergeTargetStatusToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.ShowMergeTargetStatusToolStripMenuItem.Text = "融合目标状态显示";
            this.ShowMergeTargetStatusToolStripMenuItem.Click += new System.EventHandler(this.ShowMergeTargetStatusToolStripMenuItem_Click);
            // 
            // addContextMenu
            // 
            this.addContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CancelAdd,
            this.CancelPoint,
            this.EndAdd});
            this.addContextMenu.Name = "addFZContextMenu";
            this.addContextMenu.Size = new System.Drawing.Size(125, 70);
            // 
            // CancelAdd
            // 
            this.CancelAdd.Name = "CancelAdd";
            this.CancelAdd.Size = new System.Drawing.Size(124, 22);
            this.CancelAdd.Text = "取消添加";
            this.CancelAdd.Click += new System.EventHandler(this.CancelAdd_Click);
            // 
            // CancelPoint
            // 
            this.CancelPoint.Name = "CancelPoint";
            this.CancelPoint.Size = new System.Drawing.Size(124, 22);
            this.CancelPoint.Text = "撤销";
            this.CancelPoint.Click += new System.EventHandler(this.CancelPoint_Click);
            // 
            // EndAdd
            // 
            this.EndAdd.Name = "EndAdd";
            this.EndAdd.Size = new System.Drawing.Size(124, 22);
            this.EndAdd.Text = "完成绘制";
            this.EndAdd.Click += new System.EventHandler(this.EndAdd_Click);
            // 
            // YimaEncCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.axYimaEnc);
            this.Name = "YimaEncCtrl";
            this.Size = new System.Drawing.Size(483, 424);
            ((System.ComponentModel.ISupportInitialize)(this.axYimaEnc)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.targetContextMenu.ResumeLayout(false);
            this.normalContextMenu.ResumeLayout(false);
            this.addContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxYIMAENCLib.AxYimaEnc axYimaEnc;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel CurClientPoint;
        private System.Windows.Forms.ToolStripStatusLabel CurGeoPoint;
        private System.Windows.Forms.ToolStripStatusLabel CurScale;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Timer TargetDataTimer;
        private System.Windows.Forms.ContextMenuStrip targetContextMenu;
        private System.Windows.Forms.ToolStripMenuItem ShowDetail;
        private System.Windows.Forms.Timer RadarTimer;
        private System.Windows.Forms.ContextMenuStrip normalContextMenu;
        private System.Windows.Forms.ToolStripMenuItem ShowRadarTargetItem;
        private System.Windows.Forms.ToolStripMenuItem ShowAISTargetItem;
        private System.Windows.Forms.ToolStripMenuItem ShowMergeTargetItem;
        private System.Windows.Forms.ToolStripMenuItem ShowRadarMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowOptLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowTrackMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowAllTrackToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip addContextMenu;
        private System.Windows.Forms.ToolStripMenuItem CancelAdd;
        private System.Windows.Forms.ToolStripMenuItem CancelPoint;
        private System.Windows.Forms.ToolStripMenuItem EndAdd;
        private System.Windows.Forms.ToolStripMenuItem TargetCenterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowSpeedLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowRadarTargetStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowAISTargetStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowMergeTargetStatusToolStripMenuItem;
    }
}
