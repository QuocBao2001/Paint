using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SharpGL;
using System.Diagnostics;
using System.Linq;

namespace WinFormsApp1
{
    struct AEL
    {
        public int yUpper;
        public double xInt;
        public double reciSlope;
        public AEL(int y, double x, double r)
        {
            yUpper = y;
            xInt = x;
            reciSlope = r;
        }
    }
    class ScanLine
{
        int Min_Y;
        int Max_Y;
        List<AEL>[] ET = new List<AEL>[1000];
        List<Point> pointOfColor = new List<Point>();
        public ScanLine(List<Line> lineList)
        {
            // Khởi tạo ET
            for(int i = 0; i < 1000; i++)
            {
                ET[i] = new List<AEL>();
            }
            // Tìm tung độ Min, Max
            Min_Y = lineList[0].getLowestP().Y;
            Max_Y = lineList[0].getHighestP().Y;
            for (int i = 1; i < lineList.Count; i++)
            {
                if (lineList[i].getHighestP().Y > Max_Y)
                    Max_Y = lineList[i].getHighestP().Y;
                if (lineList[i].getLowestP().Y < Min_Y)
                    Min_Y = lineList[i].getLowestP().Y;
            }
            // Xét cạnh đầu tiên
            Point firstH = lineList[0].getHighestP();
            Point firstL = lineList[0].getLowestP();
            // Nếu cạnh không nằm ngang thì thêm vào ET
            if (firstH.Y - firstL.Y != 0)
            {
                double firstReciSlope = 1.0 * (firstH.X - firstL.X) / (firstH.Y - firstL.Y);
                // Tính tọa độ thấp, cao của cạnh kế
                Point nextFirstL = lineList[1].getLowestP();
                Point nextFirstH = lineList[1].getHighestP();
                // nếu cạnh kế không nằm ngang và tọa độ thấp cạnh kế bằng tọa độ cao cạnh hiện tại thì làm ngắn cạnh
                if ((nextFirstH.Y - nextFirstL.Y !=0) && (nextFirstL == firstH))
                    firstH.Y -= 1;
                // Tính tọa độ thấp, cao của cạnh trước
                Point prevFirstL = lineList[lineList.Count - 1].getLowestP();
                Point prevFirstH = lineList[lineList.Count - 1].getHighestP();
                // nếu cạnh trước không nằm ngang và tạo độ thấp cạnh trước bằng tọa độ cao cạnh hiện tại thì làm ngắn cạnh
                if ((prevFirstL.Y - prevFirstH.Y != 0) && (prevFirstL == firstH))
                    firstH.Y -= 1;
                // Thêm vào bảng
                AEL newEdge = new AEL(firstH.Y, (float)firstL.X, firstReciSlope);
                ET[firstL.Y - Min_Y].Add(newEdge);
            }
            // Thêm các cạnh cạnh vào bảng ET
            for (int i = 1; i < lineList.Count - 1; i++)
            {
                // Lấy điểm cao nhất và thấp nhất của cạnh
                Point H = lineList[i].getHighestP();
                Point L = lineList[i].getLowestP();
                // Nếu cạnh nằm ngang thì không thêm vào ET
                if (H.Y - L.Y == 0)
                    continue;
                double LReciSlope = 1.0 * (H.X - L.X)/(H.Y-L.Y);
                // Tính tọa độ thấp, cao của cạnh kế
                Point nextL = lineList[i + 1].getLowestP();
                Point nextH = lineList[i + 1].getHighestP();
                // nếu cạnh kế không nằm ngang và tọa độ thấp cạnh kế bằng tọa độ cao cạnh hiện tại thì làm ngắn cạnh
                if ((nextH.Y - nextL.Y != 0) && (nextL == H))
                    H.Y -= 1;
                //Tính tọa độ thấp, cao của cạnh trước
                Point prevL = lineList[i - 1].getLowestP();
                Point prevH = lineList[i - 1].getHighestP();
                // Nếu cạnh trước không nằm ngang tọa độ thấp cạnh trước bằng tọa độ cao cạnh hiện tại thì làm ngắn cạnh
                if ((prevH.Y - prevL.Y != 0) && (prevL == H))
                    H.Y -= 1;
                // Thêm cạnh vào bảng
                AEL newEdge = new AEL(H.Y, (float)L.X, LReciSlope);
                ET[L.Y - Min_Y].Add(newEdge);
            }
            // Xét cạnh cuối
            Point lastH = lineList[lineList.Count - 1].getHighestP();
            Point lastL = lineList[lineList.Count - 1].getLowestP();
            // Nếu cạnh không nằm ngang thì thêm vào ET
            if (lastH.Y - lastL.Y != 0)
            {
                double LastReciSlope = 1.0 * (lastH.X - lastL.X) / (lastH.Y - lastL.Y);
                // Tính tọa độ thấp, cao của cạnh kế
                Point nextLastL = lineList[0].getLowestP();
                Point nextLastH = lineList[0].getHighestP();
                // nếu cạnh kế không nằm ngang tọa độ thấp cạnh kế bằng tọa độ cao cạnh hiện tại thì làm ngắn cạnh
                if ((nextLastH.Y - nextLastL.Y != 0) && (nextLastL == lastH))
                    lastH.Y -= 1;
                // Tính tọa độ thấp, cao của cạnh trước
                Point prevLastL = lineList[lineList.Count - 2].getLowestP();
                Point prevLastH = lineList[lineList.Count - 2].getHighestP();
                // nếu tạo độ thấp cạnh trước bằng tọa độ cao cạnh hiện tại thì làm ngắn cạnh
                if ((prevLastH.Y - prevLastL.Y != 0) && (prevLastL == lastH))
                    lastH.Y -= 1;
                // Thêm vào bảng
                AEL newEdge = new AEL(lastH.Y, (float)lastL.X, LastReciSlope);
                ET[lastL.Y - Min_Y].Add(newEdge);
            }
            // Thực hiện scan
            Scan();
        }
        void SortByxInt(List<AEL> list)
        {
            for(int i = 0; i < list.Count - 1; i++)
            {
                for(int j =0; j < list.Count - i - 1; j++)
                {
                    if (list[j].xInt > list[j + 1].xInt)
                    {
                        AEL temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }
        }
        void Scan()
        {
            // BegList lưu danh sách các cạnh đang xét
            List<AEL> BegList = new List<AEL>();
            // Lặp với mỗi dòng quét từ Min_Y đến Max_Y
            for(int i = Min_Y; i <= Max_Y; i++)
            {
                //Nếu có cạnh bắt dầu từ i
                if(ET[i-Min_Y].Count != 0)
                {
                    // Đưa danh sách các cạnh vào beglist
                        BegList = BegList.Concat(ET[i - Min_Y]).ToList();
                }
                // Sắp xếp lại Beglist theo thứ tự tăng dần x_int
                SortByxInt(BegList);
                // Tô màu các điểm ở giữa các cặp giao điểm
                for (int j = 0; j < BegList.Count - 1; j += 2)
                {
                    for(int k = (int)BegList[j].xInt + 1; k < (int)BegList[j+1].xInt + 1; k++)
                    {
                        pointOfColor.Add(new Point(k, i));
                    }
                }
                // Loại bỏ các cạnh có y_upper bằng i
                for (int j = 0; j < BegList.Count; j++)
                {
                    if (BegList[j].yUpper == i)
                    {
                        BegList.RemoveAt(j);
                        j--;
                    }
                }
                // Cập nhật giá trị xInt bởi reciSlope
                for(int j = 0; j < BegList.Count; j++)
                {
                    BegList[j] = new AEL(BegList[j].yUpper, BegList[j].xInt + BegList[j].reciSlope, BegList[j].reciSlope);
                }
            }
        }
        public List<Point> getPointColor()
        {
            return pointOfColor;
        }
}
}
