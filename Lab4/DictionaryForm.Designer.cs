namespace Lab4 {
    partial class DictionaryForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.leftPanel = new System.Windows.Forms.Panel();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.nextButton = new System.Windows.Forms.Button();
            this.testButton = new System.Windows.Forms.Button();
            this.translateButton = new System.Windows.Forms.Button();
            this.fileMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addCardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteCardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewCardsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftPanelHeader = new System.Windows.Forms.Label();
            this.rightPanelHeader = new System.Windows.Forms.Label();
            this.directionButton = new System.Windows.Forms.Button();
            this.fileMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // leftPanel
            // 
            this.leftPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.leftPanel.Location = new System.Drawing.Point(11, 79);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(291, 366);
            this.leftPanel.TabIndex = 0;
            // 
            // rightPanel
            // 
            this.rightPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.rightPanel.Location = new System.Drawing.Point(501, 79);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(294, 366);
            this.rightPanel.TabIndex = 1;
            // 
            // nextButton
            // 
            this.nextButton.Font = new System.Drawing.Font("HP Simplified", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nextButton.Location = new System.Drawing.Point(309, 79);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(186, 55);
            this.nextButton.TabIndex = 2;
            this.nextButton.Text = "Следующая карточка";
            this.nextButton.UseVisualStyleBackColor = true;
            // 
            // testButton
            // 
            this.testButton.Font = new System.Drawing.Font("HP Simplified", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.testButton.Location = new System.Drawing.Point(309, 381);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(186, 65);
            this.testButton.TabIndex = 3;
            this.testButton.Text = "Пройти тест";
            this.testButton.UseVisualStyleBackColor = true;
            // 
            // translateButton
            // 
            this.translateButton.Font = new System.Drawing.Font("HP Simplified", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.translateButton.Location = new System.Drawing.Point(309, 140);
            this.translateButton.Name = "translateButton";
            this.translateButton.Size = new System.Drawing.Size(186, 55);
            this.translateButton.TabIndex = 4;
            this.translateButton.Text = "Перевернуть";
            this.translateButton.UseVisualStyleBackColor = true;
            // 
            // fileMenu
            // 
            this.fileMenu.BackColor = System.Drawing.SystemColors.ControlLight;
            this.fileMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.fileMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.fileMenu.Location = new System.Drawing.Point(0, 0);
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(807, 29);
            this.fileMenu.TabIndex = 5;
            this.fileMenu.Text = "fileMenu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("HP Simplified", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(56, 25);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.openToolStripMenuItem.Text = "Отркыть";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.saveAsToolStripMenuItem.Text = "Сохранить как";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(179, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.exitToolStripMenuItem.Text = "Выход";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCardToolStripMenuItem,
            this.deleteCardToolStripMenuItem,
            this.viewCardsToolStripMenuItem,
            this.statsToolStripMenuItem});
            this.toolsToolStripMenuItem.Font = new System.Drawing.Font("HP Simplified", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(110, 25);
            this.toolsToolStripMenuItem.Text = "Инструменты";
            // 
            // addCardToolStripMenuItem
            // 
            this.addCardToolStripMenuItem.Name = "addCardToolStripMenuItem";
            this.addCardToolStripMenuItem.Size = new System.Drawing.Size(237, 26);
            this.addCardToolStripMenuItem.Text = "Добавить карточку";
            // 
            // deleteCardToolStripMenuItem
            // 
            this.deleteCardToolStripMenuItem.Name = "deleteCardToolStripMenuItem";
            this.deleteCardToolStripMenuItem.Size = new System.Drawing.Size(237, 26);
            this.deleteCardToolStripMenuItem.Text = "Удалить карточку";
            // 
            // viewCardsToolStripMenuItem
            // 
            this.viewCardsToolStripMenuItem.Name = "viewCardsToolStripMenuItem";
            this.viewCardsToolStripMenuItem.Size = new System.Drawing.Size(237, 26);
            this.viewCardsToolStripMenuItem.Text = "Просмотреть карточки";
            // 
            // statsToolStripMenuItem
            // 
            this.statsToolStripMenuItem.Name = "statsToolStripMenuItem";
            this.statsToolStripMenuItem.Size = new System.Drawing.Size(237, 26);
            this.statsToolStripMenuItem.Text = "Статистика";
            // 
            // leftPanelHeader
            // 
            this.leftPanelHeader.AutoSize = true;
            this.leftPanelHeader.Font = new System.Drawing.Font("HP Simplified", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.leftPanelHeader.Location = new System.Drawing.Point(96, 50);
            this.leftPanelHeader.Name = "leftPanelHeader";
            this.leftPanelHeader.Size = new System.Drawing.Size(108, 26);
            this.leftPanelHeader.TabIndex = 6;
            this.leftPanelHeader.Text = "Английский";
            // 
            // rightPanelHeader
            // 
            this.rightPanelHeader.AutoSize = true;
            this.rightPanelHeader.Font = new System.Drawing.Font("HP Simplified", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rightPanelHeader.Location = new System.Drawing.Point(607, 50);
            this.rightPanelHeader.Name = "rightPanelHeader";
            this.rightPanelHeader.Size = new System.Drawing.Size(79, 26);
            this.rightPanelHeader.TabIndex = 7;
            this.rightPanelHeader.Text = "Русский";
            // 
            // directionButton
            // 
            this.directionButton.Font = new System.Drawing.Font("HP Simplified", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.directionButton.Location = new System.Drawing.Point(308, 201);
            this.directionButton.Name = "directionButton";
            this.directionButton.Size = new System.Drawing.Size(186, 55);
            this.directionButton.TabIndex = 8;
            this.directionButton.Text = "<Направление>";
            this.directionButton.UseVisualStyleBackColor = true;
            // 
            // DictionaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(807, 477);
            this.Controls.Add(this.directionButton);
            this.Controls.Add(this.rightPanelHeader);
            this.Controls.Add(this.leftPanelHeader);
            this.Controls.Add(this.translateButton);
            this.Controls.Add(this.testButton);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.rightPanel);
            this.Controls.Add(this.leftPanel);
            this.Controls.Add(this.fileMenu);
            this.MainMenuStrip = this.fileMenu;
            this.Name = "DictionaryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Англо-Русский словарь";
            this.fileMenu.ResumeLayout(false);
            this.fileMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.Button translateButton;
        private System.Windows.Forms.MenuStrip fileMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addCardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteCardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewCardsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label leftPanelHeader;
        private System.Windows.Forms.Label rightPanelHeader;
        private System.Windows.Forms.Button directionButton;
    }
}

