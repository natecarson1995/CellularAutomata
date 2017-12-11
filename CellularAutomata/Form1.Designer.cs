namespace CellularAutomata
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.drawingArea = new System.Windows.Forms.Panel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuPlay = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReset = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTwoX = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFourX = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEightX = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTwelveX = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTwentyFourX = new System.Windows.Forms.ToolStripMenuItem();
            this.timerAdvance = new System.Windows.Forms.Timer(this.components);
            this.gameRulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConways = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBriansBrain = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHighLife = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSeeds = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // drawingArea
            // 
            this.drawingArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawingArea.Location = new System.Drawing.Point(0, 24);
            this.drawingArea.Name = "drawingArea";
            this.drawingArea.Size = new System.Drawing.Size(624, 578);
            this.drawingArea.TabIndex = 0;
            this.drawingArea.Paint += new System.Windows.Forms.PaintEventHandler(this.drawingArea_Paint);
            this.drawingArea.MouseClick += new System.Windows.Forms.MouseEventHandler(this.drawingArea_MouseClick);
            this.drawingArea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawingArea_MouseMove);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuPlay,
            this.menuReset,
            this.zoomLevelToolStripMenuItem,
            this.gameRulesToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(624, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip";
            // 
            // menuPlay
            // 
            this.menuPlay.Name = "menuPlay";
            this.menuPlay.Size = new System.Drawing.Size(41, 20);
            this.menuPlay.Text = "Play";
            this.menuPlay.Click += new System.EventHandler(this.menuPlay_Click);
            // 
            // menuReset
            // 
            this.menuReset.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem,
            this.randomToolStripMenuItem});
            this.menuReset.Name = "menuReset";
            this.menuReset.Size = new System.Drawing.Size(47, 20);
            this.menuReset.Text = "Reset";
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // randomToolStripMenuItem
            // 
            this.randomToolStripMenuItem.Name = "randomToolStripMenuItem";
            this.randomToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.randomToolStripMenuItem.Text = "Random";
            this.randomToolStripMenuItem.Click += new System.EventHandler(this.randomToolStripMenuItem_Click);
            // 
            // zoomLevelToolStripMenuItem
            // 
            this.zoomLevelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuTwoX,
            this.menuFourX,
            this.menuEightX,
            this.menuTwelveX,
            this.menuTwentyFourX});
            this.zoomLevelToolStripMenuItem.Name = "zoomLevelToolStripMenuItem";
            this.zoomLevelToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.zoomLevelToolStripMenuItem.Text = "Zoom Level";
            // 
            // menuTwoX
            // 
            this.menuTwoX.Name = "menuTwoX";
            this.menuTwoX.Size = new System.Drawing.Size(91, 22);
            this.menuTwoX.Text = "2x";
            this.menuTwoX.Click += new System.EventHandler(this.menuTwoX_Click);
            // 
            // menuFourX
            // 
            this.menuFourX.Name = "menuFourX";
            this.menuFourX.Size = new System.Drawing.Size(91, 22);
            this.menuFourX.Text = "4x";
            this.menuFourX.Click += new System.EventHandler(this.menuFourX_Click);
            // 
            // menuEightX
            // 
            this.menuEightX.Name = "menuEightX";
            this.menuEightX.Size = new System.Drawing.Size(91, 22);
            this.menuEightX.Text = "8x";
            this.menuEightX.Click += new System.EventHandler(this.menuEightX_Click);
            // 
            // menuTwelveX
            // 
            this.menuTwelveX.Name = "menuTwelveX";
            this.menuTwelveX.Size = new System.Drawing.Size(91, 22);
            this.menuTwelveX.Text = "12x";
            this.menuTwelveX.Click += new System.EventHandler(this.menuTwelveX_Click);
            // 
            // menuTwentyFourX
            // 
            this.menuTwentyFourX.Name = "menuTwentyFourX";
            this.menuTwentyFourX.Size = new System.Drawing.Size(91, 22);
            this.menuTwentyFourX.Text = "24x";
            this.menuTwentyFourX.Click += new System.EventHandler(this.menuTwentyFourX_Click);
            // 
            // timerAdvance
            // 
            this.timerAdvance.Tick += new System.EventHandler(this.timerAdvance_Tick);
            // 
            // gameRulesToolStripMenuItem
            // 
            this.gameRulesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuConways,
            this.menuBriansBrain,
            this.menuHighLife,
            this.menuSeeds});
            this.gameRulesToolStripMenuItem.Name = "gameRulesToolStripMenuItem";
            this.gameRulesToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.gameRulesToolStripMenuItem.Text = "Game Rules";
            // 
            // menuConways
            // 
            this.menuConways.Name = "menuConways";
            this.menuConways.Size = new System.Drawing.Size(197, 22);
            this.menuConways.Text = "Conway\'s Game Of Life";
            this.menuConways.Click += new System.EventHandler(this.menuConways_Click);
            // 
            // menuBriansBrain
            // 
            this.menuBriansBrain.Name = "menuBriansBrain";
            this.menuBriansBrain.Size = new System.Drawing.Size(197, 22);
            this.menuBriansBrain.Text = "Brian\'s Brain";
            this.menuBriansBrain.Click += new System.EventHandler(this.menuBriansBrain_Click);
            // 
            // menuHighLife
            // 
            this.menuHighLife.Name = "menuHighLife";
            this.menuHighLife.Size = new System.Drawing.Size(197, 22);
            this.menuHighLife.Text = "High Life";
            this.menuHighLife.Click += new System.EventHandler(this.menuHighLife_Click);
            // 
            // menuSeeds
            // 
            this.menuSeeds.Name = "menuSeeds";
            this.menuSeeds.Size = new System.Drawing.Size(197, 22);
            this.menuSeeds.Text = "Seeds";
            this.menuSeeds.Click += new System.EventHandler(this.menuSeeds_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 602);
            this.Controls.Add(this.drawingArea);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Form1";
            this.Text = "Cellular Automata";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel drawingArea;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuPlay;
        private System.Windows.Forms.Timer timerAdvance;
        private System.Windows.Forms.ToolStripMenuItem menuReset;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem randomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuTwoX;
        private System.Windows.Forms.ToolStripMenuItem menuFourX;
        private System.Windows.Forms.ToolStripMenuItem menuEightX;
        private System.Windows.Forms.ToolStripMenuItem menuTwelveX;
        private System.Windows.Forms.ToolStripMenuItem menuTwentyFourX;
        private System.Windows.Forms.ToolStripMenuItem gameRulesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuConways;
        private System.Windows.Forms.ToolStripMenuItem menuBriansBrain;
        private System.Windows.Forms.ToolStripMenuItem menuHighLife;
        private System.Windows.Forms.ToolStripMenuItem menuSeeds;
    }
}

