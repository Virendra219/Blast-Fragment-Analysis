
namespace project2
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.GetImg = new System.Windows.Forms.Button();
            this.ShowOutlines = new System.Windows.Forms.Button();
            this.Shift_to_editor = new System.Windows.Forms.Button();
            this.Remove_noise = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Editor = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.editImg = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Mark_Shadow = new System.Windows.Forms.Button();
            this.Break_Jn = new System.Windows.Forms.Button();
            this.Draw = new System.Windows.Forms.Button();
            this.Erase = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.zoomer = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.t2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.t1 = new System.Windows.Forms.TextBox();
            this.FillColour = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.Editor.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editImg)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomer)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.19703F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.802969F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1262, 691);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.GetImg);
            this.flowLayoutPanel1.Controls.Add(this.ShowOutlines);
            this.flowLayoutPanel1.Controls.Add(this.Shift_to_editor);
            this.flowLayoutPanel1.Controls.Add(this.Remove_noise);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 653);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1003, 35);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // GetImg
            // 
            this.GetImg.Location = new System.Drawing.Point(3, 3);
            this.GetImg.Name = "GetImg";
            this.GetImg.Size = new System.Drawing.Size(187, 29);
            this.GetImg.TabIndex = 1;
            this.GetImg.Text = "Get Image";
            this.GetImg.UseVisualStyleBackColor = true;
            this.GetImg.Click += new System.EventHandler(this.GetImg_Click);
            // 
            // ShowOutlines
            // 
            this.ShowOutlines.Location = new System.Drawing.Point(196, 3);
            this.ShowOutlines.Name = "ShowOutlines";
            this.ShowOutlines.Size = new System.Drawing.Size(160, 29);
            this.ShowOutlines.TabIndex = 2;
            this.ShowOutlines.Text = "Show outlines";
            this.ShowOutlines.UseVisualStyleBackColor = true;
            this.ShowOutlines.Click += new System.EventHandler(this.ShowOutlines_Click);
            // 
            // Shift_to_editor
            // 
            this.Shift_to_editor.Location = new System.Drawing.Point(362, 3);
            this.Shift_to_editor.Name = "Shift_to_editor";
            this.Shift_to_editor.Size = new System.Drawing.Size(148, 29);
            this.Shift_to_editor.TabIndex = 1;
            this.Shift_to_editor.Text = "Shift to editor";
            this.Shift_to_editor.UseVisualStyleBackColor = true;
            this.Shift_to_editor.Click += new System.EventHandler(this.Shift_to_editor_Click);
            // 
            // Remove_noise
            // 
            this.Remove_noise.Location = new System.Drawing.Point(516, 3);
            this.Remove_noise.Name = "Remove_noise";
            this.Remove_noise.Size = new System.Drawing.Size(148, 29);
            this.Remove_noise.TabIndex = 3;
            this.Remove_noise.Text = "Remove noise";
            this.Remove_noise.UseVisualStyleBackColor = true;
            this.Remove_noise.Click += new System.EventHandler(this.Remove_noise_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.Editor);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1003, 644);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(995, 611);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // Editor
            // 
            this.Editor.Controls.Add(this.panel1);
            this.Editor.Location = new System.Drawing.Point(4, 29);
            this.Editor.Name = "Editor";
            this.Editor.Padding = new System.Windows.Forms.Padding(3);
            this.Editor.Size = new System.Drawing.Size(995, 611);
            this.Editor.TabIndex = 1;
            this.Editor.Text = "Editor";
            this.Editor.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.editImg);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(989, 605);
            this.panel1.TabIndex = 0;
            // 
            // editImg
            // 
            this.editImg.Cursor = System.Windows.Forms.Cursors.Default;
            this.editImg.Location = new System.Drawing.Point(3, 3);
            this.editImg.Name = "editImg";
            this.editImg.Size = new System.Drawing.Size(989, 605);
            this.editImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.editImg.TabIndex = 1;
            this.editImg.TabStop = false;
            this.editImg.MouseClick += new System.Windows.Forms.MouseEventHandler(this.editImg_MouseClick);
            this.editImg.MouseDown += new System.Windows.Forms.MouseEventHandler(this.editImg_MouseDown);
            this.editImg.MouseMove += new System.Windows.Forms.MouseEventHandler(this.editImg_MouseMove);
            this.editImg.MouseUp += new System.Windows.Forms.MouseEventHandler(this.editImg_MouseUp);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.tableLayoutPanel2);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(1012, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(247, 644);
            this.flowLayoutPanel2.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.Mark_Shadow, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.Break_Jn, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.Draw, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.Erase, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.FillColour, 0, 6);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 7;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 119F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(247, 418);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // Mark_Shadow
            // 
            this.Mark_Shadow.Location = new System.Drawing.Point(3, 344);
            this.Mark_Shadow.Name = "Mark_Shadow";
            this.Mark_Shadow.Size = new System.Drawing.Size(109, 28);
            this.Mark_Shadow.TabIndex = 10;
            this.Mark_Shadow.Text = "Mark shadow";
            this.Mark_Shadow.UseVisualStyleBackColor = true;
            this.Mark_Shadow.Click += new System.EventHandler(this.Mark_Shadow_Click);
            // 
            // Break_Jn
            // 
            this.Break_Jn.Location = new System.Drawing.Point(3, 308);
            this.Break_Jn.Name = "Break_Jn";
            this.Break_Jn.Size = new System.Drawing.Size(126, 29);
            this.Break_Jn.TabIndex = 9;
            this.Break_Jn.Text = "Break Junctions";
            this.Break_Jn.UseVisualStyleBackColor = true;
            this.Break_Jn.Click += new System.EventHandler(this.Break_Jn_Click);
            // 
            // Draw
            // 
            this.Draw.Location = new System.Drawing.Point(3, 273);
            this.Draw.Name = "Draw";
            this.Draw.Size = new System.Drawing.Size(109, 29);
            this.Draw.TabIndex = 8;
            this.Draw.Text = "Draw";
            this.Draw.UseVisualStyleBackColor = true;
            this.Draw.Click += new System.EventHandler(this.Draw_Click);
            // 
            // Erase
            // 
            this.Erase.Location = new System.Drawing.Point(3, 237);
            this.Erase.Name = "Erase";
            this.Erase.Size = new System.Drawing.Size(109, 29);
            this.Erase.TabIndex = 7;
            this.Erase.Text = "Erase";
            this.Erase.UseVisualStyleBackColor = true;
            this.Erase.Click += new System.EventHandler(this.Erase_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.zoomer);
            this.groupBox2.Location = new System.Drawing.Point(3, 122);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(238, 107);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Zoomer";
            // 
            // zoomer
            // 
            this.zoomer.LargeChange = 1;
            this.zoomer.Location = new System.Drawing.Point(0, 45);
            this.zoomer.Maximum = 5;
            this.zoomer.Name = "zoomer";
            this.zoomer.Size = new System.Drawing.Size(232, 56);
            this.zoomer.TabIndex = 4;
            this.zoomer.Scroll += new System.EventHandler(this.zoomer_Scroll);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.t2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.t1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(238, 113);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sobel Thresholds";
            // 
            // t2
            // 
            this.t2.Location = new System.Drawing.Point(122, 68);
            this.t2.Name = "t2";
            this.t2.Size = new System.Drawing.Size(77, 27);
            this.t2.TabIndex = 7;
            this.t2.Text = "10";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "T2 (<T1)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "T1";
            // 
            // t1
            // 
            this.t1.Location = new System.Drawing.Point(122, 26);
            this.t1.Name = "t1";
            this.t1.Size = new System.Drawing.Size(77, 27);
            this.t1.TabIndex = 4;
            this.t1.Text = "10";
            // 
            // FillColour
            // 
            this.FillColour.Location = new System.Drawing.Point(3, 378);
            this.FillColour.Name = "FillColour";
            this.FillColour.Size = new System.Drawing.Size(94, 29);
            this.FillColour.TabIndex = 11;
            this.FillColour.Text = "Fill";
            this.FillColour.UseVisualStyleBackColor = true;
            this.FillColour.Click += new System.EventHandler(this.FillColour_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 691);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.Editor.ResumeLayout(false);
            this.Editor.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.editImg)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomer)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button GetImg;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button ShowOutlines;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage Editor;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Shift_to_editor;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox t2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox t1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TrackBar zoomer;
        private System.Windows.Forms.Button Remove_noise;
        private System.Windows.Forms.Button Erase;
        private System.Windows.Forms.Button Draw;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button Break_Jn;
        private System.Windows.Forms.Button Mark_Shadow;
        private System.Windows.Forms.Button FillColour;
        private System.Windows.Forms.PictureBox editImg;
    }
}

