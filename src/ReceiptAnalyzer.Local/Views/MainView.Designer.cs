namespace BS.ReceiptAnalyzer.Local.Views
{
    partial class MainView
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
            StartTaskGroupBox = new GroupBox();
            TaskIdLabel = new Label();
            TaskIdTextBox = new TextBox();
            StartTaskButton = new Button();
            SourceImagePixtureBox = new PictureBox();
            PartialResultGroupBox = new GroupBox();
            ProgressGroupBox = new GroupBox();
            ProcessingLogsTextBox = new RichTextBox();
            ProcessingProgressBar = new ProgressBar();
            StartTaskGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SourceImagePixtureBox).BeginInit();
            ProgressGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // StartTaskGroupBox
            // 
            StartTaskGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            StartTaskGroupBox.Controls.Add(TaskIdLabel);
            StartTaskGroupBox.Controls.Add(TaskIdTextBox);
            StartTaskGroupBox.Controls.Add(StartTaskButton);
            StartTaskGroupBox.Controls.Add(SourceImagePixtureBox);
            StartTaskGroupBox.Location = new Point(12, 12);
            StartTaskGroupBox.Name = "StartTaskGroupBox";
            StartTaskGroupBox.Size = new Size(426, 370);
            StartTaskGroupBox.TabIndex = 0;
            StartTaskGroupBox.TabStop = false;
            StartTaskGroupBox.Text = "Konfiguracja zadania";
            // 
            // TaskIdLabel
            // 
            TaskIdLabel.AutoSize = true;
            TaskIdLabel.Location = new Point(6, 84);
            TaskIdLabel.Name = "TaskIdLabel";
            TaskIdLabel.Size = new Size(74, 15);
            TaskIdLabel.TabIndex = 5;
            TaskIdLabel.Text = "Identyfikator";
            // 
            // TaskIdTextBox
            // 
            TaskIdTextBox.Location = new Point(86, 81);
            TaskIdTextBox.Name = "TaskIdTextBox";
            TaskIdTextBox.ReadOnly = true;
            TaskIdTextBox.Size = new Size(334, 23);
            TaskIdTextBox.TabIndex = 4;
            // 
            // StartTaskButton
            // 
            StartTaskButton.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            StartTaskButton.Location = new Point(6, 22);
            StartTaskButton.Name = "StartTaskButton";
            StartTaskButton.Size = new Size(414, 42);
            StartTaskButton.TabIndex = 3;
            StartTaskButton.Text = "Rozpocznij analizę";
            StartTaskButton.UseVisualStyleBackColor = true;
            StartTaskButton.Click += StartTaskButton_Click;
            // 
            // SourceImagePixtureBox
            // 
            SourceImagePixtureBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            SourceImagePixtureBox.Location = new Point(6, 110);
            SourceImagePixtureBox.Name = "SourceImagePixtureBox";
            SourceImagePixtureBox.Size = new Size(414, 254);
            SourceImagePixtureBox.SizeMode = PictureBoxSizeMode.Zoom;
            SourceImagePixtureBox.TabIndex = 2;
            SourceImagePixtureBox.TabStop = false;
            // 
            // PartialResultGroupBox
            // 
            PartialResultGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PartialResultGroupBox.Location = new Point(444, 12);
            PartialResultGroupBox.Name = "PartialResultGroupBox";
            PartialResultGroupBox.Size = new Size(526, 370);
            PartialResultGroupBox.TabIndex = 1;
            PartialResultGroupBox.TabStop = false;
            PartialResultGroupBox.Text = "Rezultat kroku";
            // 
            // ProgressGroupBox
            // 
            ProgressGroupBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ProgressGroupBox.Controls.Add(ProcessingLogsTextBox);
            ProgressGroupBox.Controls.Add(ProcessingProgressBar);
            ProgressGroupBox.Location = new Point(12, 388);
            ProgressGroupBox.Name = "ProgressGroupBox";
            ProgressGroupBox.Size = new Size(958, 204);
            ProgressGroupBox.TabIndex = 2;
            ProgressGroupBox.TabStop = false;
            ProgressGroupBox.Text = "Postęp zadania";
            // 
            // ProcessingLogsTextBox
            // 
            ProcessingLogsTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ProcessingLogsTextBox.Location = new Point(6, 51);
            ProcessingLogsTextBox.Name = "ProcessingLogsTextBox";
            ProcessingLogsTextBox.ReadOnly = true;
            ProcessingLogsTextBox.Size = new Size(946, 147);
            ProcessingLogsTextBox.TabIndex = 1;
            ProcessingLogsTextBox.Text = "";
            // 
            // ProcessingProgressBar
            // 
            ProcessingProgressBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ProcessingProgressBar.Location = new Point(6, 22);
            ProcessingProgressBar.Name = "ProcessingProgressBar";
            ProcessingProgressBar.Size = new Size(946, 23);
            ProcessingProgressBar.TabIndex = 0;
            // 
            // MainView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 604);
            Controls.Add(ProgressGroupBox);
            Controls.Add(PartialResultGroupBox);
            Controls.Add(StartTaskGroupBox);
            Name = "MainView";
            Text = "MainView";
            StartTaskGroupBox.ResumeLayout(false);
            StartTaskGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SourceImagePixtureBox).EndInit();
            ProgressGroupBox.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox StartTaskGroupBox;
        private GroupBox PartialResultGroupBox;
        private GroupBox ProgressGroupBox;
        private PictureBox SourceImagePixtureBox;
        private Button StartTaskButton;
        private RichTextBox ProcessingLogsTextBox;
        private ProgressBar ProcessingProgressBar;
        private Label TaskIdLabel;
        private TextBox TaskIdTextBox;
    }
}