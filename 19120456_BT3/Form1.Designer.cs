
namespace WinFormsApp1
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
            this.openGLControl = new SharpGL.OpenGLControl();
            this.bt_Line = new System.Windows.Forms.Button();
            this.bt_Circle = new System.Windows.Forms.Button();
            this.bt_LineColor = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.bt_Rec = new System.Windows.Forms.Button();
            this.bt_Hexa = new System.Windows.Forms.Button();
            this.bt_Ellipse = new System.Windows.Forms.Button();
            this.bt_Tri = new System.Windows.Forms.Button();
            this.bt_Penta = new System.Windows.Forms.Button();
            this.lb_Size = new System.Windows.Forms.Label();
            this.nud_Size = new System.Windows.Forms.NumericUpDown();
            this.bt_ClearAll = new System.Windows.Forms.Button();
            this.bt_Undo = new System.Windows.Forms.Button();
            this.bt_Polygon = new System.Windows.Forms.Button();
            this.bt_BGColor = new System.Windows.Forms.Button();
            this.bt_Choose = new System.Windows.Forms.Button();
            this.bt_FloodFill = new System.Windows.Forms.Button();
            this.bt_Scanline = new System.Windows.Forms.Button();
            this.bt_Move = new System.Windows.Forms.Button();
            this.bt_Rotate = new System.Windows.Forms.Button();
            this.bt_Resize = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Size)).BeginInit();
            this.SuspendLayout();
            // 
            // openGLControl
            // 
            this.openGLControl.DrawFPS = false;
            this.openGLControl.Location = new System.Drawing.Point(30, 118);
            this.openGLControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.openGLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl.Size = new System.Drawing.Size(919, 659);
            this.openGLControl.TabIndex = 0;
            this.openGLControl.OpenGLInitialized += new System.EventHandler(this.openGLControl_OpenGLInitialized);
            this.openGLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl_OpenGLDraw);
            this.openGLControl.Resized += new System.EventHandler(this.openGLControl_Resized);
            this.openGLControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ctrl_openGLControl_MouseClick);
            this.openGLControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ctrl_openGLControl_MouseDown);
            this.openGLControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ctrl_openGLControl_MouseMove);
            this.openGLControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ctrl_openGLControl_MouseUp);
            // 
            // bt_Line
            // 
            this.bt_Line.Location = new System.Drawing.Point(30, 28);
            this.bt_Line.Name = "bt_Line";
            this.bt_Line.Size = new System.Drawing.Size(91, 69);
            this.bt_Line.TabIndex = 1;
            this.bt_Line.Text = "Đoạn thẳng";
            this.bt_Line.UseVisualStyleBackColor = true;
            this.bt_Line.Click += new System.EventHandler(this.bt_Line_Click);
            // 
            // bt_Circle
            // 
            this.bt_Circle.Location = new System.Drawing.Point(148, 28);
            this.bt_Circle.Name = "bt_Circle";
            this.bt_Circle.Size = new System.Drawing.Size(90, 69);
            this.bt_Circle.TabIndex = 2;
            this.bt_Circle.Text = "Hình tròn";
            this.bt_Circle.UseVisualStyleBackColor = true;
            this.bt_Circle.Click += new System.EventHandler(this.bt_Circle_Click);
            // 
            // bt_LineColor
            // 
            this.bt_LineColor.Location = new System.Drawing.Point(977, 28);
            this.bt_LineColor.Name = "bt_LineColor";
            this.bt_LineColor.Size = new System.Drawing.Size(150, 69);
            this.bt_LineColor.TabIndex = 3;
            this.bt_LineColor.Text = "Màu viền";
            this.bt_LineColor.UseVisualStyleBackColor = true;
            this.bt_LineColor.Click += new System.EventHandler(this.bt_LineColor_Click);
            // 
            // bt_Rec
            // 
            this.bt_Rec.Location = new System.Drawing.Point(267, 28);
            this.bt_Rec.Name = "bt_Rec";
            this.bt_Rec.Size = new System.Drawing.Size(92, 69);
            this.bt_Rec.TabIndex = 4;
            this.bt_Rec.Text = "Hình chữ nhật";
            this.bt_Rec.UseVisualStyleBackColor = true;
            this.bt_Rec.Click += new System.EventHandler(this.bt_Rec_Click);
            // 
            // bt_Hexa
            // 
            this.bt_Hexa.Location = new System.Drawing.Point(740, 28);
            this.bt_Hexa.Name = "bt_Hexa";
            this.bt_Hexa.Size = new System.Drawing.Size(87, 69);
            this.bt_Hexa.TabIndex = 6;
            this.bt_Hexa.Text = "Lục giác đều";
            this.bt_Hexa.UseVisualStyleBackColor = true;
            this.bt_Hexa.Click += new System.EventHandler(this.bt_Hexa_Click);
            // 
            // bt_Ellipse
            // 
            this.bt_Ellipse.Location = new System.Drawing.Point(387, 28);
            this.bt_Ellipse.Name = "bt_Ellipse";
            this.bt_Ellipse.Size = new System.Drawing.Size(86, 69);
            this.bt_Ellipse.TabIndex = 7;
            this.bt_Ellipse.Text = "Ellipse";
            this.bt_Ellipse.UseVisualStyleBackColor = true;
            this.bt_Ellipse.Click += new System.EventHandler(this.bt_Ellipse_Click);
            // 
            // bt_Tri
            // 
            this.bt_Tri.Location = new System.Drawing.Point(501, 28);
            this.bt_Tri.Name = "bt_Tri";
            this.bt_Tri.Size = new System.Drawing.Size(89, 69);
            this.bt_Tri.TabIndex = 8;
            this.bt_Tri.Text = "Tam giác đều";
            this.bt_Tri.UseVisualStyleBackColor = true;
            this.bt_Tri.Click += new System.EventHandler(this.bt_Tri_Click);
            // 
            // bt_Penta
            // 
            this.bt_Penta.Location = new System.Drawing.Point(617, 28);
            this.bt_Penta.Name = "bt_Penta";
            this.bt_Penta.Size = new System.Drawing.Size(96, 69);
            this.bt_Penta.TabIndex = 9;
            this.bt_Penta.Text = "Ngũ giác đều";
            this.bt_Penta.UseVisualStyleBackColor = true;
            this.bt_Penta.Click += new System.EventHandler(this.bt_Penta_Click);
            // 
            // lb_Size
            // 
            this.lb_Size.AutoSize = true;
            this.lb_Size.Location = new System.Drawing.Point(977, 212);
            this.lb_Size.Name = "lb_Size";
            this.lb_Size.Size = new System.Drawing.Size(101, 20);
            this.lb_Size.TabIndex = 10;
            this.lb_Size.Text = "Độ dày nét vẽ";
            // 
            // nud_Size
            // 
            this.nud_Size.Location = new System.Drawing.Point(977, 248);
            this.nud_Size.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_Size.Name = "nud_Size";
            this.nud_Size.Size = new System.Drawing.Size(150, 27);
            this.nud_Size.TabIndex = 11;
            this.nud_Size.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_Size.ValueChanged += new System.EventHandler(this.nud_Size_ValueChanged);
            // 
            // bt_ClearAll
            // 
            this.bt_ClearAll.Location = new System.Drawing.Point(1082, 715);
            this.bt_ClearAll.Name = "bt_ClearAll";
            this.bt_ClearAll.Size = new System.Drawing.Size(91, 62);
            this.bt_ClearAll.TabIndex = 18;
            this.bt_ClearAll.Text = "Xóa tất cả";
            this.bt_ClearAll.UseVisualStyleBackColor = true;
            this.bt_ClearAll.Click += new System.EventHandler(this.bt_ClearAll_Click);
            // 
            // bt_Undo
            // 
            this.bt_Undo.Location = new System.Drawing.Point(977, 715);
            this.bt_Undo.Name = "bt_Undo";
            this.bt_Undo.Size = new System.Drawing.Size(93, 60);
            this.bt_Undo.TabIndex = 19;
            this.bt_Undo.Text = "Undo";
            this.bt_Undo.UseVisualStyleBackColor = true;
            this.bt_Undo.Click += new System.EventHandler(this.bt_Undo_Click);
            // 
            // bt_Polygon
            // 
            this.bt_Polygon.Location = new System.Drawing.Point(855, 28);
            this.bt_Polygon.Name = "bt_Polygon";
            this.bt_Polygon.Size = new System.Drawing.Size(94, 69);
            this.bt_Polygon.TabIndex = 20;
            this.bt_Polygon.Text = "Đa giác";
            this.bt_Polygon.UseVisualStyleBackColor = true;
            this.bt_Polygon.Click += new System.EventHandler(this.bt_Poligon_Click);
            // 
            // bt_BGColor
            // 
            this.bt_BGColor.Location = new System.Drawing.Point(977, 118);
            this.bt_BGColor.Name = "bt_BGColor";
            this.bt_BGColor.Size = new System.Drawing.Size(150, 69);
            this.bt_BGColor.TabIndex = 21;
            this.bt_BGColor.Text = "Màu nền";
            this.bt_BGColor.UseVisualStyleBackColor = true;
            this.bt_BGColor.Click += new System.EventHandler(this.bt_BGColor_click);
            // 
            // bt_Choose
            // 
            this.bt_Choose.Location = new System.Drawing.Point(976, 542);
            this.bt_Choose.Name = "bt_Choose";
            this.bt_Choose.Size = new System.Drawing.Size(197, 60);
            this.bt_Choose.TabIndex = 22;
            this.bt_Choose.Text = "Chọn hình để tô màu";
            this.bt_Choose.UseVisualStyleBackColor = true;
            this.bt_Choose.Click += new System.EventHandler(this.bt_Choose_Click);
            // 
            // bt_FloodFill
            // 
            this.bt_FloodFill.Enabled = false;
            this.bt_FloodFill.Location = new System.Drawing.Point(976, 634);
            this.bt_FloodFill.Name = "bt_FloodFill";
            this.bt_FloodFill.Size = new System.Drawing.Size(94, 55);
            this.bt_FloodFill.TabIndex = 23;
            this.bt_FloodFill.Text = "Tô loang";
            this.bt_FloodFill.UseVisualStyleBackColor = true;
            this.bt_FloodFill.Click += new System.EventHandler(this.bt_FloodFill_click);
            // 
            // bt_Scanline
            // 
            this.bt_Scanline.Enabled = false;
            this.bt_Scanline.Location = new System.Drawing.Point(1082, 634);
            this.bt_Scanline.Name = "bt_Scanline";
            this.bt_Scanline.Size = new System.Drawing.Size(91, 55);
            this.bt_Scanline.TabIndex = 24;
            this.bt_Scanline.Text = "Tô theo dòng quét";
            this.bt_Scanline.UseVisualStyleBackColor = true;
            this.bt_Scanline.Click += new System.EventHandler(this.bt_Scanline_click);
            // 
            // bt_Move
            // 
            this.bt_Move.Location = new System.Drawing.Point(976, 317);
            this.bt_Move.Name = "bt_Move";
            this.bt_Move.Size = new System.Drawing.Size(196, 47);
            this.bt_Move.TabIndex = 25;
            this.bt_Move.Text = "Di chuyển";
            this.bt_Move.UseVisualStyleBackColor = true;
            this.bt_Move.Click += new System.EventHandler(this.bt_Move_Click);
            // 
            // bt_Rotate
            // 
            this.bt_Rotate.Location = new System.Drawing.Point(976, 396);
            this.bt_Rotate.Name = "bt_Rotate";
            this.bt_Rotate.Size = new System.Drawing.Size(196, 48);
            this.bt_Rotate.TabIndex = 26;
            this.bt_Rotate.Text = "Xoay";
            this.bt_Rotate.UseVisualStyleBackColor = true;
            this.bt_Rotate.Click += new System.EventHandler(this.bt_Rotate_Click);
            // 
            // bt_Resize
            // 
            this.bt_Resize.Location = new System.Drawing.Point(977, 471);
            this.bt_Resize.Name = "bt_Resize";
            this.bt_Resize.Size = new System.Drawing.Size(195, 44);
            this.bt_Resize.TabIndex = 27;
            this.bt_Resize.Text = "Co giãn";
            this.bt_Resize.UseVisualStyleBackColor = true;
            this.bt_Resize.Click += new System.EventHandler(this.bt_Resize_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1185, 792);
            this.Controls.Add(this.bt_Resize);
            this.Controls.Add(this.bt_Rotate);
            this.Controls.Add(this.bt_Move);
            this.Controls.Add(this.bt_Scanline);
            this.Controls.Add(this.bt_FloodFill);
            this.Controls.Add(this.bt_Choose);
            this.Controls.Add(this.bt_BGColor);
            this.Controls.Add(this.bt_Polygon);
            this.Controls.Add(this.bt_Undo);
            this.Controls.Add(this.bt_ClearAll);
            this.Controls.Add(this.nud_Size);
            this.Controls.Add(this.lb_Size);
            this.Controls.Add(this.bt_Penta);
            this.Controls.Add(this.bt_Tri);
            this.Controls.Add(this.bt_Ellipse);
            this.Controls.Add(this.bt_Hexa);
            this.Controls.Add(this.bt_Rec);
            this.Controls.Add(this.bt_LineColor);
            this.Controls.Add(this.bt_Circle);
            this.Controls.Add(this.bt_Line);
            this.Controls.Add(this.openGLControl);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Size)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpGL.OpenGLControl openGLControl;
        private System.Windows.Forms.Button bt_Line;
        private System.Windows.Forms.Button bt_Circle;
        private System.Windows.Forms.Button bt_LineColor;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button bt_Rec;
        private System.Windows.Forms.Button bt_Hexa;
        private System.Windows.Forms.Button bt_Ellipse;
        private System.Windows.Forms.Button bt_Tri;
        private System.Windows.Forms.Button bt_Penta;
        private System.Windows.Forms.Label lb_Size;
        private System.Windows.Forms.NumericUpDown nud_Size;
        private System.Windows.Forms.Button bt_ClearAll;
        private System.Windows.Forms.Button bt_Undo;
        private System.Windows.Forms.Button bt_Polygon;
        private System.Windows.Forms.Button bt_BGColor;
        private System.Windows.Forms.Button bt_Choose;
        private System.Windows.Forms.Button bt_FloodFill;
        private System.Windows.Forms.Button bt_Scanline;
        private System.Windows.Forms.Button bt_Move;
        private System.Windows.Forms.Button bt_Rotate;
        private System.Windows.Forms.Button bt_Resize;
    }
}

