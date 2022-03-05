using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SharpGL;
using System.Diagnostics;
using System.Linq;


namespace WinFormsApp1
{
    //AffineTransform là class sử lý các phép biến đổi affine lên các điểm điều khiển
    class AffineTransform
    {
        // Tập hợp các điểm điều kiển
        List<Point> controlPoint = new List<Point>();
        // Ma trận biến đổi Affine
        double[,] affineMatrix = new double[3, 3];
        // Điểm bắt đầu và kết thúc kéo thả
        Point start, end;
        public AffineTransform(List<Point> ControlPoint, Point Start, Point End)
        {
            this.controlPoint = this.controlPoint.Concat(ControlPoint).ToList();
            this.start = Start;
            this.end = End;
        }
        void doAffine()
        {
            // Thực hiện biến đổi affine trên các điểm neo và hình sẽ được vẽ lại dựa vào điểm neo mới
            for (int i = 0; i < controlPoint.Count; i++)
            {
                double newX = (double) controlPoint[i].X * affineMatrix[0, 0] + controlPoint[i].Y * affineMatrix[0, 1] + affineMatrix[0, 2];
                double newY = (double) controlPoint[i].X * affineMatrix[1, 0] + controlPoint[i].Y * affineMatrix[1, 1] + affineMatrix[1, 2];
                controlPoint[i] = new Point((int)newX, (int)newY);
            }
        }
        public void Move()
        {
            // Hàm tịnh tiến lấy vector tịnh tiến và vector từ điểm nhấp chuột đến điểm thả chuột
            double tx = (double)end.X - start.X;
            double ty = (double)end.Y - start.Y;
            affineMatrix[0, 0] = 1;
            affineMatrix[0, 1] = 0;
            affineMatrix[0, 2] = tx;
            affineMatrix[1, 0] = 0;
            affineMatrix[1, 1] = 1;
            affineMatrix[1, 2] = ty;
            doAffine();
        }
        public void Rotate()
        {
            // Hàm xoay quanh điểm đầu tiên của đa giác và tâm của tất cả thực thể hình học khác
            // Tính góc bằng tan của tỉ lệ chênh lệnh tung độ click chuột và chênh lệch hoành độ với tâm quay
            double Alpha = Math.Atan((double)(end.Y - start.Y) / (end.X - controlPoint[0].X));
            // Nếu góc bẹt thì hiệu chỉnh lại Alpha
            if (end.X < controlPoint[0].X)
                Alpha = Math.PI + Alpha;
            double cosA = Math.Cos(Alpha);
            double sinA = Math.Sin(Alpha);
            double tx = controlPoint[0].X;
            double ty = controlPoint[0].Y;
            // Ma trận affine là kết quả sau khi thực hiện tịnh tiến về góc tọa độ, xoay, cuối cùng tịnh tiến ngược lại
            affineMatrix[0, 0] = cosA;
            affineMatrix[0, 1] = -sinA;
            affineMatrix[0, 2] = -tx*cosA +ty*sinA + tx;
            affineMatrix[1, 0] = sinA;
            affineMatrix[1, 1] = cosA;
            affineMatrix[1, 2] = -tx*sinA-ty*cosA + ty;
            doAffine();
        }
        public void Resize()
        {
            // Hàm co giãn quanh điểm đầu tiên của đa giác và tâm của tất cả thực thể hình học khác
            // Tính tỉ lệ scale là tỉ lệ độ chênh lệch khoảng cách từ tâm co đến điểm thả chuột và khoảng cách từ tâm đến điểm nhấp chuột
            double sx = (double)(end.X - controlPoint[0].X) / (start.X - controlPoint[0].X);
            double sy = (double)(end.Y - controlPoint[0].Y) / (start.Y - controlPoint[0].Y);
            double tx = controlPoint[0].X;
            double ty = controlPoint[0].Y;
            
            // Ma trận affine là kết quả sau khi thực hiện phép tịnh tiến về tâm, scale, và tịnh tiến ngược lại
            affineMatrix[0, 0] = sx;
            affineMatrix[0, 1] = 0;
            affineMatrix[0, 2] = tx - sx * tx;
            affineMatrix[1, 0] = 0;
            affineMatrix[1, 1] = sy;
            affineMatrix[1, 2] = ty - sy * ty;
            doAffine();
        }
        public List<Point> getPoints()
        {
            return controlPoint;
        }
    }
}
