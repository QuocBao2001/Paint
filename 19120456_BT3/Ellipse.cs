using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;

namespace WinFormsApp1
{
    class Ellipse
    {
        // Tọa dộ tâm ellipse
        Point center;
        // Độ lớn trục x
        int Rx;
        // Độ lớn trục y
        int Ry;
        // Kích thước nét vẽ
        short size;
        public Ellipse(Point center, int Rx, int Ry, short size)
        {
            // Khởi tạo các thuộc tính
            this.center = center;
            this.Rx = Rx;
            this.Ry = Ry;
            this.size = size;
        }
        public List<Point> Draw()
        {
            // Thực hiện xác định các điểm với ellipse có tâm là góc tọa độ, sao đó tịnh tiến theo vector tâm
            List<Point> pointsOfEllipse = new List<Point>();
            Point Current = new Point(0, Ry);
            // Thực hiện vẽ các điểm ở trên và bên dưới để tăng độ dày nét vẽ
            for (int i = -size / 2; i <= size / 2; i++)
            {
                // Vẽ 4 điểm đầu tiên là các điểm nằm trên trục Ox, Oy
                List<Point> pointsPart = new List<Point>();
                pointsPart.Add(new Point(Current.X + center.X, Current.Y + i + center.Y));
                pointsPart.Add(new Point(Current.X + center.X, -Current.Y + i + center.Y));
                pointsPart.Add(new Point(-Current.X + center.X, Current.Y + i + center.Y));
                pointsPart.Add(new Point(-Current.X + center.X, -Current.Y + i + center.Y));
                pointsOfEllipse = pointsOfEllipse.Concat(pointsPart).ToList();
            }
            // Tính các giá trị khởi đầu trong giai đoạn 1
            // Lưu lại kết quả của các phép nhân
            int twoRyRyX = 0;
            int twoRxRxY = 2 * Rx * Rx * Ry;
            int twoRyRy = 2 * Ry * Ry;
            int twoRxRx = 2 * Rx * Rx;
            int RyRy = Ry * Ry;
            int RxRx = Rx * Rx;
            int p1 = Ry * Ry - Rx * Rx * Ry + Rx * Rx / 4;
            // Giai đoạn 1, hệ số góc tiếp tuyến có độ lớn bé hơn 1
            while(twoRyRyX < twoRxRxY)
            {
                if (p1 < 0)
                {
                    Current.X += 1;
                    twoRyRyX += twoRyRy;
                    p1 += twoRyRyX + RyRy;
                }
                else
                {
                    Current.X += 1;
                    Current.Y -= 1;
                    twoRyRyX += twoRyRy;
                    twoRxRxY -= twoRxRx;
                    p1 += twoRyRyX - twoRxRxY + RyRy;
                }
                // Vẽ thêm các điểm trên, dưới để tăng độ dày nét vẽ
                for (int i = -size / 2; i <= size / 2; i++)
                {
                    // Vẽ 4 điểm ở từng góc phần tư
                    List<Point> pointsPart = new List<Point>();
                    pointsPart.Add(new Point(Current.X + center.X, Current.Y + i + center.Y));
                    pointsPart.Add(new Point(Current.X + center.X, -Current.Y + i + center.Y));
                    pointsPart.Add(new Point(-Current.X + center.X, Current.Y + i + center.Y));
                    pointsPart.Add(new Point(-Current.X + center.X, -Current.Y + i + center.Y));
                    pointsOfEllipse = pointsOfEllipse.Concat(pointsPart).ToList();
                }
            }
            // Giai đoạn 2, độ lớn hệ số góc lớn hơn 1
            int p2 = (int)(RyRy * (Current.X + 1.0/2) * (Current.X + 1.0/2) + RxRx * (Current.Y - 1) * (Current.Y - 1) - RxRx * RyRy);
            while(Current.Y != 0)
            {
                if (p2 >= 0)
                {
                    Current.Y -= 1;
                    twoRxRxY -= twoRxRx;
                    p2 += -twoRxRxY + RxRx;
                }
                else
                {
                    Current.X += 1;
                    Current.Y -= 1;
                    twoRyRyX += twoRyRy;
                    twoRxRxY -= twoRxRx;
                    p2 += twoRyRyX - twoRxRxY + RxRx;
                }
                // Vẽ thêm các điểm trái, phải để tăng độ dày nét vẽ
                for (int i = -size / 2; i <= size / 2; i++)
                {
                    List<Point> pointsPart = new List<Point>();
                    pointsPart.Add(new Point(Current.X + i + center.X, Current.Y + center.Y));
                    pointsPart.Add(new Point(Current.X + i + center.X, -Current.Y + center.Y));
                    pointsPart.Add(new Point(-Current.X + i + center.X, Current.Y + center.Y));
                    pointsPart.Add(new Point(-Current.X + i + center.X, -Current.Y + center.Y));
                    pointsOfEllipse = pointsOfEllipse.Concat(pointsPart).ToList();
                }
            }
            return pointsOfEllipse;
        }
    }
}
