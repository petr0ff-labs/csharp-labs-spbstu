namespace Lab3 {
    partial class MainForm {
        
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
            this.pointsButton = new System.Windows.Forms.Button();
            this.paramsButton = new System.Windows.Forms.Button();
            this.curveLineButton = new System.Windows.Forms.Button();
            this.polyLineButton = new System.Windows.Forms.Button();
            this.bezLineButton = new System.Windows.Forms.Button();
            this.fillPointsButton = new System.Windows.Forms.Button();
            this.moveButton = new System.Windows.Forms.Button();
            this.clearFromButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pointsButton
            // 
            this.pointsButton.Location = new System.Drawing.Point(20, 15);
            this.pointsButton.Name = "pointsButton";
            this.pointsButton.Size = new System.Drawing.Size(120, 40);
            this.pointsButton.TabIndex = 0;
            this.pointsButton.Text = "Точки";
            this.pointsButton.UseVisualStyleBackColor = true;
            // 
            // paramsButton
            // 
            this.paramsButton.Location = new System.Drawing.Point(20, 61);
            this.paramsButton.Name = "paramsButton";
            this.paramsButton.Size = new System.Drawing.Size(120, 40);
            this.paramsButton.TabIndex = 2;
            this.paramsButton.Text = "Параметры";
            this.paramsButton.UseVisualStyleBackColor = true;
            // 
            // curveLineButton
            // 
            this.curveLineButton.Location = new System.Drawing.Point(20, 107);
            this.curveLineButton.Name = "curveLineButton";
            this.curveLineButton.Size = new System.Drawing.Size(120, 40);
            this.curveLineButton.TabIndex = 3;
            this.curveLineButton.Text = "Кривая";
            this.curveLineButton.UseVisualStyleBackColor = true;
            // 
            // polyLineButton
            // 
            this.polyLineButton.Location = new System.Drawing.Point(20, 153);
            this.polyLineButton.Name = "polyLineButton";
            this.polyLineButton.Size = new System.Drawing.Size(120, 40);
            this.polyLineButton.TabIndex = 4;
            this.polyLineButton.Text = "Ломанная";
            this.polyLineButton.UseVisualStyleBackColor = true;
            // 
            // bezLineButton
            // 
            this.bezLineButton.Location = new System.Drawing.Point(20, 199);
            this.bezLineButton.Name = "bezLineButton";
            this.bezLineButton.Size = new System.Drawing.Size(120, 40);
            this.bezLineButton.TabIndex = 5;
            this.bezLineButton.Text = "Безье";
            this.bezLineButton.UseVisualStyleBackColor = true;
            // 
            // fillPointsButton
            // 
            this.fillPointsButton.Location = new System.Drawing.Point(20, 245);
            this.fillPointsButton.Name = "fillPointsButton";
            this.fillPointsButton.Size = new System.Drawing.Size(120, 40);
            this.fillPointsButton.TabIndex = 6;
            this.fillPointsButton.Text = "Заполненная";
            this.fillPointsButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.moveButton.Location = new System.Drawing.Point(20, 291);
            this.moveButton.Name = "cancelButton";
            this.moveButton.Size = new System.Drawing.Size(120, 40);
            this.moveButton.TabIndex = 7;
            this.moveButton.Text = "Движение";
            this.moveButton.UseVisualStyleBackColor = true;
            // 
            // clearFromButton
            // 
            this.clearFromButton.Location = new System.Drawing.Point(20, 337);
            this.clearFromButton.Name = "clearFromButton";
            this.clearFromButton.Size = new System.Drawing.Size(120, 40);
            this.clearFromButton.TabIndex = 8;
            this.clearFromButton.Text = "Очистить";
            this.clearFromButton.UseVisualStyleBackColor = true;
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(20, 382);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(120, 40);
            this.exitButton.TabIndex = 4;
            this.exitButton.Text = "Выход";
            this.exitButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 495);
            this.Controls.Add(this.clearFromButton);
            this.Controls.Add(this.moveButton);
            this.Controls.Add(this.fillPointsButton);
            this.Controls.Add(this.bezLineButton);
            this.Controls.Add(this.polyLineButton);
            this.Controls.Add(this.curveLineButton);
            this.Controls.Add(this.paramsButton);
            this.Controls.Add(this.pointsButton);
            this.Controls.Add(this.exitButton);
            this.Name = "MainForm";
            this.Text = "ФОРМА";
            this.ResumeLayout(false);

        }
        
        #endregion

        private System.Windows.Forms.Button pointsButton;
        private System.Windows.Forms.Button paramsButton;
        private System.Windows.Forms.Button curveLineButton;
        private System.Windows.Forms.Button polyLineButton;
        private System.Windows.Forms.Button bezLineButton;
        private System.Windows.Forms.Button fillPointsButton;
        private System.Windows.Forms.Button moveButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button clearFromButton;
    }
}

