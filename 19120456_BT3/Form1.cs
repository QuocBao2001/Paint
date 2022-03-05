using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpGL;
using System.Diagnostics;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        // Lưu màu viền người dùng chọn
        Color colorLineColor;
        // Lưu màu nền người dùng chọn
        Color colorBGColor;
        // Lưu số đại điện cho loại hình vẽ (0: đoạn thẳng, 1: hình tròn, 2: hình cn, 3: hình ellipse,
        // 4: tam giác đều, 5: ngũ giác đều, 6: lục giác đều)
        short shShape;
        // Lưu kích thước nét vẽ
        short shSize;
        // Lưu vị trí ấn chuột
        Point pStart;
        //Lưu vị trí thả chuột
        Point pEnd;
        //Lưu các đỉnh của đa giác
        List<Point> polygonPoint = new List<Point>();
        //Lưu danh sách các hình học đã được vẽ
        List<Shape> lShape = new List<Shape>();
        //Lưu danh sách màu vièn của các hình học đã được vẽ
        List<double[]> shColors = new List<double[]>();
        // Lưu danh sách màu nền của các hình đọc đã được tô
        List<double[]> BGColors = new List<double[]>();
        // Lưu danh sách hình học của tập điểm màu tương ứng
        List<int> BGshIndex = new List<int>();
        // Lưu hình học tạm thời đang trong quá trình vẽ
        Shape tempShape;
        // Lưu màu viền của hình học tạm thời đang trong quá trình vẽ
        double[] tempColor = new double[4];
        // Lưu các điểm được tô màu của hình
        List<List<Point>> colorPoint = new List<List<Point>>();
        // Lưu control point của các shape
        List<Point> controlPoint = new List<Point>();
        // Lưu các điểm dùng để hiển thị controlPoint của các shape
        List<Point> controlPointDrawed = new List<Point>();
        // Lưu index của hình được chọn
        int ShapeIndex = -1;
        public Form1()
        {
            InitializeComponent();

            // Khởi tạo giá trị màu nét vẽ, màu nền là màu White, hình học là đoạn thẳng, kích thước là 1
            colorLineColor = Color.White;
            colorBGColor = Color.White;
            shShape = 0;
            shSize = 1;
        }
        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            // Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            // Set the clear color.
            gl.ClearColor(0, 0, 0, 0);
            // Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            // Load the identity.
            gl.LoadIdentity();
        }
        private void openGLControl_Resized(object sender, EventArgs e)
        {
            // Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            // Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            // Load the identity.
            gl.LoadIdentity();
            // Create a perspective transformation.
            gl.Viewport(0, 0, openGLControl.Width, openGLControl.Height);
            gl.Ortho2D(0, openGLControl.Width, 0, openGLControl.Height);
        }
        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            // Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            // Clear the color and depth buffer.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            // Đặt chế độ vẽ điểm
            gl.Begin(OpenGL.GL_POINTS);
            if (true)
            {
                // Nếu ở chế độ chọn hình thì hiển thị các controlPoint
                gl.Color(0.0, 1.0, 0.0, 0.0);
                if(controlPointDrawed.Count != 0)
                {
                    for(int i = 0; i < controlPointDrawed.Count; i++)
                    {
                        gl.Vertex(controlPointDrawed[i].X, gl.RenderContextProvider.Height - controlPointDrawed[i].Y);
                    }
                }
                // Nếu có hình ảnh đang được vẽ thì minh họa lên màn hình kết quả sẽ đạt được
                gl.Color(tempColor[0], tempColor[1], tempColor[2], tempColor[3]);
                if (tempShape != null)
                {
                    // Vẽ từng điểm của hình học
                    List<Point> tempPointList = tempShape.getPointOfShape();
                    for (int i = 0; i < tempPointList.Count; i++)
                    {
                        gl.Vertex(tempPointList[i].X, gl.RenderContextProvider.Height - tempPointList[i].Y);
                    }
                }
                // Nếu có hình được tô thì vẽ những màu tô của các hình đó
                if (colorPoint.Count != 0)
                {
                    for(int i = 0; i < colorPoint.Count; i++)
                    {
                        gl.Color(BGColors[i][0], BGColors[i][1], BGColors[i][2]);
                        for (int j = 0; j <colorPoint[i].Count; j++)
                        gl.Vertex(colorPoint[i][j].X, gl.RenderContextProvider.Height - colorPoint[i][j].Y);
                    }
                }
                // Thể hiện những điểm của các hình học đã được vẽ
                for (int i = 0; i < lShape.Count; i++)
                {
                    gl.Color(shColors[i][0], shColors[i][1], shColors[i][2], shColors[i][3]);
                    List<Point> pointList = lShape[i].getPointOfShape();
                    for (int j = 0; j < pointList.Count; j++)
                    {
                        gl.Vertex(pointList[j].X, gl.RenderContextProvider.Height - pointList[j].Y);
                    }
                }
            }
            gl.End();
            gl.Flush();
        }
        private void bt_LineColor_Click(object sender, EventArgs e)
        {
            // Lấy màu viền người dùng chọn
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                colorLineColor = colorDialog1.Color;
            }
        }
        private void bt_BGColor_click(object sender, EventArgs e)
        {
            // Lấy màu nền người dùng chọn
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                colorBGColor = colorDialog1.Color;
            }
        }
        private void ctrl_openGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (shShape != 7 && shShape > -1)
            {
                // Bắt đầu vẽ, lấy vị trí bắt đầu và màu đã chọn để vẽ
                pStart = e.Location;
                pEnd = pStart;
                double[] currentColor = { colorLineColor.R / 255.0, colorLineColor.G / 255.0, colorLineColor.B / 255.0, 0 };
                shColors.Add(currentColor);

                tempColor = currentColor;
            }
            else if (shShape != -1 && shShape != 7)
            {
                controlPointDrawed.Clear();
                for (int i = 0; i < lShape.Count; i++)
                {
                    controlPointDrawed = controlPointDrawed.Concat(lShape[i].getControlPointDrawed(e.Location)).ToList();
                    if (controlPointDrawed.Count != 0)
                    {
                        ShapeIndex = i;
                        break;
                    }
                }
                pStart = e.Location;
                pEnd = pStart;
                tempColor = shColors[ShapeIndex];
            }
        }
        private void ctrl_openGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (shShape != 7 && shShape > -1)
            {
                // Khi nhất chuột, đánh dấu kết thúc việc vẽ, tạo đối tượng và thêm đối tượng vào danh sách các đối tượng đã vẽ
                pEnd = e.Location;
                Shape newShape = new Shape(shShape, pStart, pEnd, shSize, openGLControl.OpenGL);
                lShape.Add(newShape);
                // Xóa hình vẽ tạm
                tempShape = null;
            }
            else if (shShape < -1)
            {
                pEnd = e.Location;
                // Lấy danh sách controlPoint của hình học được chọn
                List<Point> tempControlPoint = lShape[ShapeIndex].getControlPoint();
                // Lấy loại hình học
                short newShShape = lShape[ShapeIndex].getShShape();
                // Lấy kích thước nét vẽ
                short newSize = lShape[ShapeIndex].getSize();
                AffineTransform affine = new AffineTransform(tempControlPoint, pStart, pEnd);
                // Loại bỏ hình học hiện tại để thực hiện affine
                lShape.RemoveAt(ShapeIndex);
                // Loại bỏ màu được tô của hình học đó
                for(int i = 0; i < colorPoint.Count; i++)
                {
                    if (BGshIndex[i] == ShapeIndex)
                    {
                        colorPoint.RemoveAt(i);
                        BGColors.RemoveAt(i);
                        break;
                    }
                }
                if (shShape == -2)
                    affine.Move();
                if (shShape == -3)
                    affine.Rotate();
                if (shShape == -4)
                    affine.Resize();
                Shape newShape = new Shape(newShShape, pStart, pEnd, newSize, openGLControl.OpenGL, affine.getPoints());
                lShape.Add(newShape);
                // Loại bỏ hình học tạm
                tempShape = null;
            }
        }
        private void ctrl_openGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            if(shShape != 7 && shShape > -1)
            {
                //Khi nhấn giữ và di chuyển chuột, tạo 1 đối tượng hình học tạm thời để thể hiện lên màng hình
                if (e.Button == MouseButtons.Left)
                {
                    pEnd = e.Location;

                    Shape newShape = new Shape(shShape, pStart, pEnd, shSize, openGLControl.OpenGL);
                    tempShape = newShape;
                }
            }
            else if (shShape == 7)
            {
                if (polygonPoint.Count > 0)
                {
                    Shape newShape = new Shape(shShape, pStart, pEnd, shSize, openGLControl.OpenGL, polygonPoint);
                    tempShape = newShape;
                }
            }
            else if (shShape < -1)
            {
                if (e.Button == MouseButtons.Left)
                {
                    // Xóa các hiển thị điểm neo
                    controlPointDrawed.Clear();
                    pEnd = e.Location;
                    // Lấy danh sách controlPoint của hình học được chọn
                    List<Point> tempControlPoint = lShape[ShapeIndex].getControlPoint();
                    // Lấy loại hình học
                    short newShShape = lShape[ShapeIndex].getShShape();
                    // Lấy kích thước nét vẽ
                    short newSize = lShape[ShapeIndex].getSize();
                    AffineTransform affine = new AffineTransform(tempControlPoint, pStart, pEnd);
                    if (shShape == -2)
                        affine.Move();
                    if (shShape == -3)
                        affine.Rotate();
                    if (shShape == -4)
                        affine.Resize();
                    Shape newShape = new Shape(newShShape, pStart, pEnd, newSize, openGLControl.OpenGL, affine.getPoints());
                    tempShape = newShape;
                }
            }
        }
        private void nud_Size_ValueChanged(object sender, EventArgs e)
        {
            // Lấy kích thước nét vẽ (pixel)
            shSize = (short)nud_Size.Value;
        }
        private void bt_ClearAll_Click(object sender, EventArgs e)
        {
            // Xóa toàn bộ danh sách hình học đã vẽ
            lShape.Clear();
            shColors.Clear();
            controlPointDrawed.Clear();
            colorPoint.Clear();
        }
        //Các hàm thay đổi loại hình học
        private void bt_Line_Click(object sender, EventArgs e)
        {
            shShape = 0;
            controlPointDrawed.Clear();
        }
        private void bt_Circle_Click(object sender, EventArgs e)
        {
            shShape = 1;
            controlPointDrawed.Clear();
        }
        private void bt_Rec_Click(object sender, EventArgs e)
        {
            shShape = 2;
            controlPointDrawed.Clear();
        }
        private void bt_Ellipse_Click(object sender, EventArgs e)
        {
            shShape = 3;
            controlPointDrawed.Clear();
        }
        private void bt_Tri_Click(object sender, EventArgs e)
        {
            shShape = 4;
            controlPointDrawed.Clear();
        }
        private void bt_Penta_Click(object sender, EventArgs e)
        {
            shShape = 5;
            controlPointDrawed.Clear();
        }
        private void bt_Hexa_Click(object sender, EventArgs e)
        {
            shShape = 6;
            controlPointDrawed.Clear();
        }

        private void bt_Undo_Click(object sender, EventArgs e)
        {
            // Xóa hình học cuối cùng được thêm vào danh sách
            if (lShape.Count != 0)
            {
                lShape.RemoveAt(lShape.Count - 1);
                shColors.RemoveAt(shColors.Count - 1);
            }
        }

        private void bt_Poligon_Click(object sender, EventArgs e)
        {
            shShape = 7;
            controlPointDrawed.Clear();
        }

        private void ctrl_openGLControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (shShape == 7)
            {
                double[] currentColor = { colorLineColor.R / 255.0, colorLineColor.G / 255.0, colorLineColor.B / 255.0, 0 };
                tempColor = currentColor;
                controlPoint.Clear();
                if (e.Button == MouseButtons.Left)
                {
                    polygonPoint.Add(e.Location);
                }
                else
                {
                    if (polygonPoint.Count < 3)
                    {
                        // Xóa hình vẽ tạm
                        tempShape = null;
                        // Xóa ds điểm
                        polygonPoint.Clear();
                        return;
                    }
                    Shape newShape = new Shape(shShape, pStart, pEnd, shSize, openGLControl.OpenGL, polygonPoint);
                    lShape.Add(newShape);
                    // Gán màu vẽ
                    shColors.Add(currentColor);
                    // Xóa hình vẽ tạm
                    tempShape = null;
                    // Xóa ds điểm
                    polygonPoint.Clear();
                }
            }
            if (shShape == -1)
            {
                // Hiển thị control point của hình được chọn và cho phép chọn cách tô
                controlPointDrawed.Clear();
                for(int i = 0; i < lShape.Count; i++)
                {
                    controlPointDrawed = controlPointDrawed.Concat(lShape[i].getControlPointDrawed(e.Location)).ToList();
                    if (controlPointDrawed.Count != 0)
                    {
                        ShapeIndex = i;
                        bt_FloodFill.Enabled = true;
                        bt_Scanline.Enabled = true;
                        break;
                    }
                }
            }
        }

        private void bt_Choose_Click(object sender, EventArgs e)
        {
            shShape = -1;
        }

        private void bt_FloodFill_click(object sender, EventArgs e)
        {
            // Điểm seed được chọn là tâm của các điểm điều kiển, do đó chưa thể tô cho đa giác lõm
            Point seed = new Point(0,0);
            controlPoint = lShape[ShapeIndex].getControlPoint();
            for(int i = 0; i < controlPoint.Count; i++)
            {
                seed.X += controlPoint[i].X;
                seed.Y += controlPoint[i].Y;
            }
            seed.X = (int)(seed.X * 1.0 / controlPoint.Count);
            seed.Y = (int)(seed.Y * 1.0 / controlPoint.Count);
            // Tính khoảng cách từ tâm hình đến 1 điểm điều kiển, nếu khoảng cách quá lớn thì không tô
            float DisFromCenter = (seed.X - controlPoint[0].X) * (seed.X - controlPoint[0].X) + (seed.Y - controlPoint[0].Y) * (seed.Y - controlPoint[0].Y);
            if (DisFromCenter < 10000)
            {
                ColorFill newFill = new ColorFill(seed,lShape[ShapeIndex].getPointOfShape());
                colorPoint.Add(newFill.Fill());
                double[] currentColor = { colorBGColor.R / 255.0, colorBGColor.G / 255.0, colorBGColor.B / 255.0, 0 };
                BGColors.Add(currentColor);
                BGshIndex.Add(ShapeIndex);
            }
            bt_FloodFill.Enabled = false;
            bt_Scanline.Enabled = false;
        }

        private void bt_Scanline_click(object sender, EventArgs e)
        {
            // Chưa cài đặt cho hình tròn và ellipse
            if(lShape[ShapeIndex].shShape == 1 || lShape[ShapeIndex].shShape == 3)
            {
                bt_FloodFill.Enabled = false;
                bt_Scanline.Enabled = false;
                return;
            }
            // Lấy danh sách các điểm màu và tính thời gian
            ScanLine newScanLine = new ScanLine(lShape[ShapeIndex].getLineOfShape());
            colorPoint.Add(newScanLine.getPointColor());
            double[] currentColor = { colorBGColor.R / 255.0, colorBGColor.G / 255.0, colorBGColor.B / 255.0, 0 };
            BGColors.Add(currentColor);
            BGshIndex.Add(ShapeIndex);
            bt_FloodFill.Enabled = false;
            bt_Scanline.Enabled = false;
        }

        private void bt_Move_Click(object sender, EventArgs e)
        {
            shShape = -2;
        }

        private void bt_Rotate_Click(object sender, EventArgs e)
        {
            shShape = -3;
        }

        private void bt_Resize_Click(object sender, EventArgs e)
        {
            shShape = -4;
        }
    }
}
