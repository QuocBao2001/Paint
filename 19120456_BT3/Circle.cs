using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;

namespace WinFormsApp1
{
    class Circle
{
        // Tọa độ tâm của hình tròn
        Point center;
        // Bán kính của hình tròn
        int R;
        // Kích thước nét vẽ
        short size;
        public Circle(Point center, int R, short size)
        {
            // Khởi tạo các thuộc tính
            this.center = center;
            this.R = R;
            this.size = size;
        }
        public List<Point> Draw()
        {
            List<Point> pointsOfCircle = new List<Point>();
            // Tính toán với trường hợp hình tròn ở gốc tọa độ, sau đó tịnh tiến các điểm theo vector tọa độ tâm
            Point Current = new Point(0, R);
            // Tính các tham số ban đầu
            int twoX = 0;
            int twoY = 2 * R;
            int P = 1 - R;
            // Vẽ các điểm kề nhau để đảm bảo độ dày nét vẽ
            // ở diểm có độ lớn hệ số góc tiếp tuyến bé hơn 1 thì vẽ thêm các điểm trên dưới
            // ở điểm có độ lớn hệ số góc tiếp tuyến lớn hơn 1 thì vẽ thêm các điểm trái phải
            for (int i = -size / 2; i <= size / 2; i++)
            {
                // Vẽ 8 điểm đầu tiên là các điểm trên Ox, Oy, y = x, y = -x
                List<Point> pointsPart = new List<Point>();
                pointsPart.Add(new Point(Current.X + center.X, Current.Y + i + center.Y));
                pointsPart.Add(new Point(Current.X + center.X, -Current.Y + i + center.Y));
                pointsPart.Add(new Point(-Current.X + center.X, Current.Y + i + center.Y));
                pointsPart.Add(new Point(-Current.X + center.X, -Current.Y + i + center.Y));
                pointsPart.Add(new Point(Current.Y + i + center.X, Current.X  + center.Y));
                pointsPart.Add(new Point(Current.Y + i + center.X, -Current.X + center.Y));
                pointsPart.Add(new Point(-Current.Y + i + center.X, Current.X + center.Y));
                pointsPart.Add(new Point(-Current.Y + i + center.X, -Current.X + center.Y));
                pointsOfCircle = pointsOfCircle.Concat(pointsPart).ToList();
            }
            // Lặp cho đến khi X > Y
            while (twoX <= twoY)
            {
                if (P < 0)
                {
                    Current.X += 1;
                    twoX += 2;
                    P += twoX + 1;
                }
                else
                {
                    Current.X += 1;
                    Current.Y -= 1;
                    twoX += 2;
                    twoY -= 2;
                    P += twoX - twoY + 1;
                }
                // Thực hiện vẽ điểm vừa tính
                for (int i = -size / 2; i <= size / 2; i++)
                {
                    List<Point> pointsPart = new List<Point>();
                    pointsPart.Add(new Point(Current.X + center.X, Current.Y + i + center.Y));
                    pointsPart.Add(new Point(Current.X + center.X, -Current.Y + i + center.Y));
                    pointsPart.Add(new Point(-Current.X + center.X, Current.Y + i + center.Y));
                    pointsPart.Add(new Point(-Current.X + center.X, -Current.Y + i + center.Y));
                    pointsPart.Add(new Point(Current.Y + i + center.X, Current.X + center.Y));
                    pointsPart.Add(new Point(Current.Y + i + center.X, -Current.X + center.Y));
                    pointsPart.Add(new Point(-Current.Y + i + center.X, Current.X + center.Y));
                    pointsPart.Add(new Point(-Current.Y + i + center.X, -Current.X + center.Y));
                    pointsOfCircle = pointsOfCircle.Concat(pointsPart).ToList();
                }
            }
            return pointsOfCircle;
        }
}
}
