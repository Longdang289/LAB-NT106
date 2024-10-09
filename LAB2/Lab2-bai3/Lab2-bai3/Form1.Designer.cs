namespace Lab2_bai3
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            calculate = new Button();
            input = new RichTextBox();
            openFile = new Button();
            output = new RichTextBox();
            inputPath = new TextBox();
            outputPath = new TextBox();
            SuspendLayout();
            // 
            // calculate
            // 
            calculate.Location = new Point(353, 246);
            calculate.Name = "calculate";
            calculate.Size = new Size(94, 29);
            calculate.TabIndex = 0;
            calculate.Text = "Enter";
            calculate.UseVisualStyleBackColor = true;
            calculate.Click += calculate_Click;
            // 
            // input
            // 
            input.Location = new Point(12, 39);
            input.Name = "input";
            input.Size = new Size(335, 399);
            input.TabIndex = 1;
            input.Text = "";
            // 
            // openFile
            // 
            openFile.Location = new Point(353, 142);
            openFile.Name = "openFile";
            openFile.Size = new Size(94, 29);
            openFile.TabIndex = 3;
            openFile.Text = "Open File";
            openFile.UseVisualStyleBackColor = true;
            // 
            // output
            // 
            output.Location = new Point(453, 39);
            output.Name = "output";
            output.Size = new Size(335, 399);
            output.TabIndex = 4;
            output.Text = "";
            // 
            // inputPath
            // 
            inputPath.Location = new Point(12, 6);
            inputPath.Name = "inputPath";
            inputPath.Size = new Size(335, 27);
            inputPath.TabIndex = 5;
            // 
            // outputPath
            // 
            outputPath.Location = new Point(453, 6);
            outputPath.Name = "outputPath";
            outputPath.Size = new Size(335, 27);
            outputPath.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(outputPath);
            Controls.Add(inputPath);
            Controls.Add(output);
            Controls.Add(openFile);
            Controls.Add(input);
            Controls.Add(calculate);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button calculate;
        private RichTextBox input;
        private Button openFile;
        private RichTextBox output;
        private TextBox inputPath;
        private TextBox outputPath;
    }
}
