namespace _2048WinFormsApp
{
    partial class StartForm
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
            startButton = new Button();
            nameTextBox = new TextBox();
            nameLabel = new Label();
            mapSizeTrackBar = new TrackBar();
            mapSizeLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)mapSizeTrackBar).BeginInit();
            SuspendLayout();
            // 
            // startButton
            // 
            startButton.Font = new Font("Showcard Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            startButton.Location = new Point(118, 111);
            startButton.Name = "startButton";
            startButton.Size = new Size(75, 23);
            startButton.TabIndex = 6;
            startButton.Text = "START";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // nameTextBox
            // 
            nameTextBox.Font = new Font("Showcard Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            nameTextBox.Location = new Point(62, 66);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(189, 22);
            nameTextBox.TabIndex = 5;
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Font = new Font("Showcard Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            nameLabel.Location = new Point(115, 30);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(78, 15);
            nameLabel.TabIndex = 4;
            nameLabel.Text = "YOUR NAME";
            // 
            // mapSizeTrackBar
            // 
            mapSizeTrackBar.Location = new Point(100, 190);
            mapSizeTrackBar.Maximum = 9;
            mapSizeTrackBar.Minimum = 4;
            mapSizeTrackBar.Name = "mapSizeTrackBar";
            mapSizeTrackBar.Size = new Size(104, 45);
            mapSizeTrackBar.TabIndex = 7;
            mapSizeTrackBar.Value = 4;
            // 
            // mapSizeLabel
            // 
            mapSizeLabel.AutoSize = true;
            mapSizeLabel.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            mapSizeLabel.Location = new Point(118, 163);
            mapSizeLabel.Name = "mapSizeLabel";
            mapSizeLabel.Size = new Size(79, 20);
            mapSizeLabel.TabIndex = 10;
            mapSizeLabel.Text = "Mapsize";
            // 
            // StartForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(315, 279);
            Controls.Add(mapSizeLabel);
            Controls.Add(mapSizeTrackBar);
            Controls.Add(startButton);
            Controls.Add(nameTextBox);
            Controls.Add(nameLabel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "StartForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "NameForm";
            ((System.ComponentModel.ISupportInitialize)mapSizeTrackBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button startButton;
        public TextBox nameTextBox;
        private Label nameLabel;
        public TrackBar mapSizeTrackBar;
        private Label mapSizeLabel;
    }
}