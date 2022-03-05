using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace WinFormsApp1
{
    class Line
    {
        // Điểm thứ nhất của đoạn thẳng
        Point start;
        // Điểm thứ hai của đoạn thẳng
        Point end;
        // Kích thước nét vẽ
        short size;

        public Line(Point start, Point end, short size)
        {
            // Khởi tạo các thuộc tính
            this.start = start;
            this.end = end;
            this.size = size;
        }
        //Hàm tìm danh sách các điểm của đoạn thẳng
        public List<Point> Draw()
        {
            List<Point> pointOfLines = new List<Point>();
            //Tính dx, dy
            int dx = end.X - start.X;
            int dy = end.Y - start.Y;
            // stepx, stepy là giá trị độ thay đổi của điểm sau so với điểm trước, có giá trị 1 hoặc -1
            int stepx, stepy;
            if (dx < 0)
            {
                dx = -dx;
                stepx = -1;
            }
            else
            {
                stepx = 1;
            }
            if (dy < 0)
            {
                dy = -dy;
                stepy = -1;
            }
            else
            {
                stepy = 1;
            }
            Point currentPoint = start;
            // Xử lý với trường hợp trị tuyệt đối hệ số góc bé hơn 1
            if (dx > dy)
            {
                int p = 2*dy - dx;
                // Vẽ thêm các điểm ở trên và dưới để tăng độ dày nét vẽ
                for (int i = -size / 2; i <= size / 2; i++)
                {
                    Point pointPart = new Point(currentPoint.X, currentPoint.Y + i);
                    pointOfLines.Add(pointPart);
                }
                // Lặp cho dến khi vẽ đến điểm đích
                while (currentPoint.X != end.X)
                {
                    if (p >= 0)
                    {
                        currentPoint.Y += stepy;
                        p += 2 * dy - 2 * dx;
                    }
                    else
                    {
                        p += 2 * dy;
                    }
                    currentPoint.X += stepx;
                    for (int i = -size / 2; i <= size / 2; i++)
                    {
                        Point pointPart = new Point(currentPoint.X, currentPoint.Y + i);
                        pointOfLines.Add(pointPart);
                    }
                }
            }
            // Xử lý với trường hợp trị tuyệt đối hệ số góc lớn hơn hoặc bằng 1
            else
            {
                int p = 2 * dx - dy;
                // Vẽ thêm các điểm ở trái và phải để tăng độ dày nét vẽ
                for (int i = -size / 2; i <= size / 2; i++)
                {
                    Point pointPart = new Point(currentPoint.X + i, currentPoint.Y);
                    pointOfLines.Add(pointPart);
                }
                // Lặp cho đến khi vẽ được điểm đích
                while (currentPoint.Y != end.Y)
                {
                    if (p >= 0)
                    {
                        currentPoint.X += stepx;
                        p += 2 * dx - 2 * dy;
                    }
                    else
                    {
                        p += 2 * dx;
                    }
                    currentPoint.Y += stepy;
                    for (int i = -size / 2; i <= size / 2; i++)
                    {
                        Point pointPart = new Point(currentPoint.X + i, currentPoint.Y);
                        pointOfLines.Add(pointPart);
                    }
                }
            }
            return pointOfLines;
        }
        public Point getLowestP()
        {
            if (start.Y < end.Y)
                return start;
            else
                return end;
        }
        public Point getHighestP()
        {
            if (start.Y < end.Y)
                return end;
            else
                return start;
        }
    }
}
