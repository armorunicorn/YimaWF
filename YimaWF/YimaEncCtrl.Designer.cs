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
            this.OptLinkageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowTrackMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TargetCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AutoTrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ManualTrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RadarTimer = new System.Windows.Forms.Timer(this.components);
            this.normalContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShowRadarTargetItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowAISTargetItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowMergeTargetItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowOptMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowRadarMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.光电观察范围ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowAllTrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CancelAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.CancelPoint = new System.Windows.Forms.ToolStripMenuItem();
            this.EndAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowSpeedLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.TargetDataTimer.Interval = 50;
            this.TargetDataTimer.Tick += new System.EventHandler(this.TargetDataTimer_Tick);
            // 
            // targetContextMenu
            // 
            this.targetContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowDetail,
            this.OptLinkageToolStripMenuItem,
            this.ShowTrackMenuItem,
            this.TargetCenterToolStripMenuItem,
            this.AutoTrackToolStripMenuItem,
            this.ManualTrackToolStripMenuItem});
            this.targetContextMenu.Name = "targetContextMenu";
            this.targetContextMenu.Size = new System.Drawing.Size(149, 136);
            // 
            // ShowDetail
            // 
            this.ShowDetail.Name = "ShowDetail";
            this.ShowDetail.Size = new System.Drawing.Size(148, 22);
            this.ShowDetail.Text = "目标详细信息";
            this.ShowDetail.Click += new System.EventHandler(this.ShowDetail_Click);
            // 
            // OptLinkageToolStripMenuItem
            // 
            this.OptLinkageToolStripMenuItem.Name = "OptLinkageToolStripMenuItem";
            this.OptLinkageToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.OptLinkageToolStripMenuItem.Text = "光电联动";
            this.OptLinkageToolStripMenuItem.Click += new System.EventHandler(this.OptLinkageToolStripMenuItem_Click);
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
            this.TargetCenterToolStripMenuItem.Text = "目标居中";
            this.TargetCenterToolStripMenuItem.Click += new System.EventHandler(this.TargetCenterToolStripMenuItem_Click);
            // 
            // AutoTrackToolStripMenuItem
            // 
            this.AutoTrackToolStripMenuItem.Name = "AutoTrackToolStripMenuItem";
            this.AutoTrackToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.AutoTrackToolStripMenuItem.Text = "自动跟踪";
            this.AutoTrackToolStripMenuItem.Click += new System.EventHandler(this.AutoTrackToolStripMenuItem_Click);
            // 
            // ManualTrackToolStripMenuItem
            // 
            this.ManualTrackToolStripMenuItem.Name = "ManualTrackToolStripMenuItem";
            this.ManualTrackToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.ManualTrackToolStripMenuItem.Text = "手动跟踪";
            this.ManualTrackToolStripMenuItem.Click += new System.EventHandler(this.ManualTrackToolStripMenuItem_Click);
            // 
            // RadarTimer
            // 
            this.RadarTimer.Interval = 27;
            this.RadarTimer.Tick += new System.EventHandler(this.RadarTimer_Tick);
            // 
            // normalContextMenu
            // 
            this.normalContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowRadarTargetItem,
            this.ShowAISTargetItem,
            this.ShowMergeTargetItem,
            this.ShowOptMenuItem,
            this.ShowRadarMenuItem,
            this.光电观察范围ToolStripMenuItem,
            this.ShowAllTrackToolStripMenuItem,
            this.ShowSpeedLineToolStripMenuItem});
            this.normalContextMenu.Name = "normalContextMenu";
            this.normalContextMenu.Size = new System.Drawing.Size(153, 202);
            // 
            // ShowRadarTargetItem
            // 
            this.ShowRadarTargetItem.Checked = true;
            this.ShowRadarTargetItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowRadarTargetItem.Name = "ShowRadarTargetItem";
            this.ShowRadarTargetItem.Size = new System.Drawing.Size(152, 22);
            this.ShowRadarTargetItem.Text = "雷达态势";
            this.ShowRadarTargetItem.Click += new System.EventHandler(this.ShowRadarTargetItem_Click);
            // 
            // ShowAISTargetItem
            // 
            this.ShowAISTargetItem.Checked = true;
            this.ShowAISTargetItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowAISTargetItem.Name = "ShowAISTargetItem";
            this.ShowAISTargetItem.Size = new System.Drawing.Size(152, 22);
            this.ShowAISTargetItem.Text = "AIS态势";
            this.ShowAISTargetItem.Click += new System.EventHandler(this.ShowAISTargetItem_Click);
            // 
            // ShowMergeTargetItem
            // 
            this.ShowMergeTargetItem.Checked = true;
            this.ShowMergeTargetItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowMergeTargetItem.Name = "ShowMergeTargetItem";
            this.ShowMergeTargetItem.Size = new System.Drawing.Size(152, 22);
            this.ShowMergeTargetItem.Text = "融合态势";
            this.ShowMergeTargetItem.Click += new System.EventHandler(this.ShowMergeTargetItem_Click);
            // 
            // ShowOptMenuItem
            // 
            this.ShowOptMenuItem.Checked = true;
            this.ShowOptMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowOptMenuItem.Name = "ShowOptMenuItem";
            this.ShowOptMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ShowOptMenuItem.Text = "光电态势";
            this.ShowOptMenuItem.Click += new System.EventHandler(this.ShowOptMenuItem_Click);
            // 
            // ShowRadarMenuItem
            // 
            this.ShowRadarMenuItem.Checked = true;
            this.ShowRadarMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowRadarMenuItem.Name = "ShowRadarMenuItem";
            this.ShowRadarMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ShowRadarMenuItem.Text = "雷达扫描线";
            this.ShowRadarMenuItem.Click += new System.EventHandler(this.ShowRadarMenuItem_Click);
            // 
            // 光电观察范围ToolStripMenuItem
            // 
            this.光电观察范围ToolStripMenuItem.Checked = true;
            this.光电观察范围ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.光电观察范围ToolStripMenuItem.Name = "光电观察范围ToolStripMenuItem";
            this.光电观察范围ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.光电观察范围ToolStripMenuItem.Text = "光电观察范围";
            // 
            // ShowAllTrackToolStripMenuItem
            // 
            this.ShowAllTrackToolStripMenuItem.Checked = true;
            this.ShowAllTrackToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowAllTrackToolStripMenuItem.Name = "ShowAllTrackToolStripMenuItem";
            this.ShowAllTrackToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ShowAllTrackToolStripMenuItem.Text = "航迹显示";
            this.ShowAllTrackToolStripMenuItem.Click += new System.EventHandler(this.ShowAllTrackToolStripMenuItem_Click);
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
            // ShowSpeedLineToolStripMenuItem
            // 
            this.ShowSpeedLineToolStripMenuItem.Checked = true;
            this.ShowSpeedLineToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowSpeedLineToolStripMenuItem.Name = "ShowSpeedLineToolStripMenuItem";
            this.ShowSpeedLineToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ShowSpeedLineToolStripMenuItem.Text = "航首线显示";
            this.ShowSpeedLineToolStripMenuItem.Click += new System.EventHandler(this.ShowSpeedLineToolStripMenuItem_Click);
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
        private System.Windows.Forms.ToolStripMenuItem ShowOptMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowRadarMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 光电观察范围ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OptLinkageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowTrackMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowAllTrackToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip addContextMenu;
        private System.Windows.Forms.ToolStripMenuItem CancelAdd;
        private System.Windows.Forms.ToolStripMenuItem CancelPoint;
        private System.Windows.Forms.ToolStripMenuItem EndAdd;
        private System.Windows.Forms.ToolStripMenuItem TargetCenterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AutoTrackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ManualTrackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowSpeedLineToolStripMenuItem;
    }
}
