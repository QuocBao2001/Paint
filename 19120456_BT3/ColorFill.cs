using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SharpGL;
using System.Diagnostics;
using System.Linq;

namespace WinFormsApp1
{
    class ColorFill
{
        // Điểm bắt đầu cho tô loang
        Point start;
        // Danh sách các điểm trên biên
        List<Point> pointOfShape = new List<Point>();
        //Danh sách các điểm được tô
        List<Point> pointOfColorFill = new List<Point>();
        public ColorFill(Point start, List<Point> pointOfShape)
        {
            this.start = start;
            this.pointOfShape = pointOfShape;
        }
        bool InSet(Point pointCheck, List<Point> listCheck)
        {
            for(int i = 0; i < listCheck.Count(); i++)
            {
                if (pointCheck == listCheck[i])
                    return true;
            }
            return false;
        }
        void BoundaryFill(int x, int y)
        {
            Point currentPoint = new Point(x, y);
            if(!InSet(currentPoint,pointOfShape) && !InSet(currentPoint, pointOfColorFill))
            {
                pointOfColorFill.Add(currentPoint);
                BoundaryFill(x + 1, y);
                BoundaryFill(x - 1, y);
                BoundaryFill(x, y + 1);
                BoundaryFill(x, y - 1);
            }
        }
        public List<Point> Fill()
        {
            BoundaryFill(start.X, start.Y);
            return pointOfColorFill;
        }
    }
}
