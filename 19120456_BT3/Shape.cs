using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SharpGL;
using System.Diagnostics;
using System.Linq;

namespace WinFormsApp1
{
    class Shape
    {
        // Loại hình học
        public short shShape;
        // Điểm ấn chuột
        Point start;
        // Điểm thả chuột
        Point end;
        // Tập hợp các điểm điều kiển
        List<Point> controlPoint = new List<Point>();
        // Tập hợp các cạnh của hình
        List<Line> lineOfShape = new List<Line>();
        //Kích thước nét vẽ
        short size;
        OpenGL gl;
        //Danh sách các điểm của hình học
        List<Point> pointOfShape = new List<Point>();
        //Danh sách các điểm điều kiển thực vẽ (gồm điểm điều kiển và lân cận)
        List<Point> controlPointDrawed = new List<Point>();

        List<Point> DrawControlPoint(Point p)
        {
            List<Point> result = new List<Point>();
            for(int i = -2*size; i <=2*size; i++)
            {
                for(int j = -2*size; j <= 2*size; j++)
                {
                    Point newPoint = new Point(p.X + i, p.Y + j);
                    result.Add(newPoint);
                }
            }
            return result;
        }
        public Shape(short shShape, Point start, Point end, short size, OpenGL gl, List<Point> ControlPoint = null)
        {
            //Khởi tạo các thuộc tính của class
            this.shShape = shShape;
            this.start = start;
            this.end = end;
            this.size = size;
            this.gl = gl;
            // Nếu có danh sách controlPoint thì vẽ hình dựa trên các controlPoint
            if (ControlPoint != null)
            {
                this.controlPoint = this.controlPoint.Concat(ControlPoint).ToList();
                switch (shShape)
                {
                    // Đoạn thẳng
                    case 0:
                        Line newLine = new Line(controlPoint[1], controlPoint[2], size);
                        pointOfShape = newLine.Draw();
                        // Lưu vị trí các điểm điều khiển
                        controlPoint.Add(controlPoint[0]);
                        controlPoint.Add(controlPoint[1]);
                        controlPoint.Add(controlPoint[2]);
                        controlPointDrawed = DrawControlPoint(controlPoint[0]);
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(controlPoint[1])).ToList();
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(controlPoint[2])).ToList();
                        break;
                    // Hình tròn
                    case 1:
                        // controlPoint1 là tâm, controlPoint2 là 1 điểm trên đường tròn
                        Point center = controlPoint[0];
                        Point onCircle = controlPoint[1];
                        // Bán kính là khoản cách từ điểm nhấp chuột đến điểm thả chuột
                        int R = (int)Math.Sqrt((center.X - onCircle.X) * (center.X - onCircle.X) + (center.Y - onCircle.Y) * (center.Y - onCircle.Y));
                        Circle newCircle = new Circle(center, R, size);
                        pointOfShape = newCircle.Draw();
                        // Lưu vị trí các điểm điều khiển ở 4 hướng
                        Point Right = new Point(center.X + R, center.Y);
                        Point Left = new Point(center.X - R, center.Y);
                        Point Up = new Point(center.X, center.Y + R);
                        Point Down = new Point(center.X, center.Y - R);
                        controlPoint.Add(center);
                        controlPoint.Add(Right);
                        controlPoint.Add(Left);
                        controlPoint.Add(Up);
                        controlPoint.Add(Down);
                        controlPointDrawed = DrawControlPoint(center);
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(Right)).ToList();
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(Left)).ToList();
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(Up)).ToList();
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(Down)).ToList();
                        break;
                    // Hình Ellipse
                    case 3:
                        // Tâm Ellipse là controlPoint1
                        Point Ecenter = controlPoint[0];
                        // Các trục lớn, trục nhỏ là sự chênh lệch tọa độ giữa điểm ấn chuột và điểm thả chuột
                        int Rx = Math.Abs(controlPoint[1].X - Ecenter.X);
                        int Ry = Math.Abs(controlPoint[3].Y - Ecenter.Y);
                        Ellipse newEllipse = new Ellipse(Ecenter, Rx, Ry, size);
                        pointOfShape = newEllipse.Draw();
                        // Lưu vị trí các điểm điều khiển
                        Point ERight = new Point(Ecenter.X + Rx, Ecenter.Y);
                        Point ELeft = new Point(Ecenter.X - Rx, Ecenter.Y);
                        Point EUp = new Point(Ecenter.X, Ecenter.Y + Ry);
                        Point EDown = new Point(Ecenter.X, Ecenter.Y - Ry);
                        controlPoint.Add(Ecenter);
                        controlPoint.Add(ERight);
                        controlPoint.Add(ELeft);
                        controlPoint.Add(EUp);
                        controlPoint.Add(EDown);
                        controlPointDrawed = DrawControlPoint(Ecenter);
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(ERight)).ToList();
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(ELeft)).ToList();
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(EUp)).ToList();
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(EDown)).ToList();
                        break;
                    // Hình chữ nhật
                    case 2:
                    // Hình tam giác đều
                    case 4:
                    // Vẽ ngũ giác đều
                    case 5:
                    // Vẽ lục giác đều
                    case 6:
                        // Vẽ các cạnh dựa trên các đỉnh, lưu danh sách điểm của đa giác vào pointOfShape
                        Line lastLine = new Line(controlPoint[controlPoint.Count - 1], controlPoint[1], size);
                        pointOfShape = lastLine.Draw();
                        for (int i = 1; i < controlPoint.Count - 1; i++)
                        {
                            Line currentLine = new Line(controlPoint[i], controlPoint[i + 1], size);
                            pointOfShape = pointOfShape.Concat(currentLine.Draw()).ToList();
                            // Lưu danh sách các cạnh
                            lineOfShape.Add(currentLine);
                        }
                        lineOfShape.Add(lastLine);
                        // Lưu vị trí các điểm điều khiển
                        controlPointDrawed = DrawControlPoint(controlPoint[0]);
                        for (int i = 1; i < controlPoint.Count; i++)
                        {
                            controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(controlPoint[i])).ToList();
                        }
                        break;
                    // Vẽ đa giác
                    case 7:
                        // Vẽ các cạnh dựa trên các đỉnh, lưu danh sách điểm của đa giác vào pointOfShape
                        Line lastLineP = new Line(controlPoint[controlPoint.Count - 1], controlPoint[0], size);
                        pointOfShape = lastLineP.Draw();
                        for (int i = 0; i < controlPoint.Count - 1; i++)
                        {
                            Line currentLine = new Line(controlPoint[i], controlPoint[i + 1], size);
                            pointOfShape = pointOfShape.Concat(currentLine.Draw()).ToList();
                            // Lưu danh sách các cạnh
                            lineOfShape.Add(currentLine);
                        }
                        lineOfShape.Add(lastLineP);
                        // Lưu vị trí các điểm điều khiển
                        controlPointDrawed = DrawControlPoint(controlPoint[0]);
                        for (int i = 1; i < controlPoint.Count; i++)
                        {
                            controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(controlPoint[i])).ToList();
                        }
                        break;
                }
            }
            // Nếu không có controlPoint thì vẽ hình theo các điểm start end
            else
            {
                this.controlPoint = new List<Point>();
                switch (shShape)
                {
                    // Đoạn thẳng
                    case 0:
                        Line newLine = new Line(start, end, size);
                        pointOfShape = newLine.Draw();
                        // Lưu vị trí điểm giữa
                        int centerLX = Math.Abs(start.X + end.X)/2;
                        int centerLY = Math.Abs(start.Y + end.Y)/2;
                        // Lưu vị trí các điểm điều khiển
                        Point centerL = new Point(centerLX, centerLY);
                        controlPoint.Add(centerL);
                        controlPoint.Add(start);
                        controlPoint.Add(end);
                        controlPointDrawed = DrawControlPoint(centerL);
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(end)).ToList();
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(end)).ToList();
                        break;
                    // Hình tròn
                    case 1:
                        //Điểm nhấp chuột đầu tiên là tâm
                        Point center = new Point(start.X, start.Y);
                        // Bán kính là khoản cách từ điểm nhấp chuột đến điểm thả chuột
                        int R = (int)Math.Sqrt((start.X - end.X) * (start.X - end.X) + (start.Y - end.Y) * (start.Y - end.Y));
                        Circle newCircle = new Circle(center, R, size);
                        pointOfShape = newCircle.Draw();
                        // Lưu vị trí các điểm điều khiển
                        Point Right = new Point(center.X + R, center.Y);
                        Point Left = new Point(center.X - R, center.Y);
                        Point Up = new Point(center.X, center.Y + R);
                        Point Down = new Point(center.X, center.Y - R);
                        controlPoint.Add(center);
                        controlPoint.Add(Right);
                        controlPoint.Add(Left);
                        controlPoint.Add(Up);
                        controlPoint.Add(Down);
                        controlPointDrawed = DrawControlPoint(center);
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(Right)).ToList();
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(Left)).ToList();
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(Up)).ToList();
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(Down)).ToList();
                        break;
                    // Hình chữ nhật
                    case 2:
                        // Xác định các đỉnh của hình chữ nhật
                        Point Vertex1 = new Point(start.X, start.Y);
                        Point Vertex2 = new Point(end.X, start.Y);
                        Point Vertex3 = new Point(end.X, end.Y);
                        Point Vertex4 = new Point(start.X, end.Y);
                        // Vẽ các cạnh dựa trên các đỉnh
                        Line firstLine = new Line(Vertex1, Vertex2, size);
                        pointOfShape = firstLine.Draw();
                        Line secondLine = new Line(Vertex2, Vertex3, size);
                        pointOfShape = pointOfShape.Concat(secondLine.Draw()).ToList();
                        Line thirdLine = new Line(Vertex3, Vertex4, size);
                        pointOfShape = pointOfShape.Concat(thirdLine.Draw()).ToList();
                        Line fourthLine = new Line(Vertex4, Vertex1, size);
                        pointOfShape = pointOfShape.Concat(fourthLine.Draw()).ToList();
                        //Lưu danh sách các cạnh
                        lineOfShape.Add(firstLine);
                        lineOfShape.Add(secondLine);
                        lineOfShape.Add(thirdLine);
                        lineOfShape.Add(fourthLine);
                        // Lưu tâm hình chữ nhật
                        int CenterRX = Math.Abs(Vertex1.X + Vertex3.X)/2;
                        int CenterRY = Math.Abs(Vertex1.Y + Vertex3.Y)/2;
                        Point CenterR = new Point(CenterRX, CenterRY);
                        // Lưu vị trí các điểm điều khiển
                        controlPoint.Add(CenterR);
                        controlPoint.Add(Vertex1);
                        controlPoint.Add(Vertex2);
                        controlPoint.Add(Vertex3);
                        controlPoint.Add(Vertex4);
                        controlPointDrawed = DrawControlPoint(CenterR);
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(Vertex1)).ToList();
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(Vertex2)).ToList();
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(Vertex3)).ToList();
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(Vertex4)).ToList();
                        break;
                    // Hình Ellipse
                    case 3:
                        // Tâm Ellipse là điểm ấn chuột
                        Point Ecenter = new Point(start.X, start.Y);
                        // Các trục lớn, trục nhỏ là sự chênh lệch tọa độ giữa điểm ấn chuột và điểm thả chuột
                        int Rx = Math.Abs(end.X - start.X);
                        int Ry = Math.Abs(end.Y - start.Y);
                        Ellipse newEllipse = new Ellipse(Ecenter, Rx, Ry, size);
                        pointOfShape = newEllipse.Draw();
                        // Lưu vị trí các điểm điều khiển
                        Point ERight = new Point(Ecenter.X + Rx, Ecenter.Y);
                        Point ELeft = new Point(Ecenter.X - Rx, Ecenter.Y);
                        Point EUp = new Point(Ecenter.X, Ecenter.Y + Ry);
                        Point EDown = new Point(Ecenter.X, Ecenter.Y - Ry);
                        controlPoint.Add(Ecenter);
                        controlPoint.Add(ERight);
                        controlPoint.Add(ELeft);
                        controlPoint.Add(EUp);
                        controlPoint.Add(EDown);
                        controlPointDrawed = DrawControlPoint(Ecenter);
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(ERight)).ToList();
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(ELeft)).ToList();
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(EUp)).ToList();
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(EDown)).ToList();
                        break;
                    // Hình tam giác đều
                    case 4:
                        // Các đỉnh của hình tam giác được tính dựa vào điểm ấn thả chuột
                        // Chương trình vẽ 1 cạnh của tam giác song song với trục ngang, không quan tâm đến tung độ điểm thả chuột
                        Point triVertex1 = new Point(start.X, start.Y);
                        double high = Math.Abs(end.X - start.X) * Math.Sqrt(3) / 2;
                        Point triVertex2 = new Point((int)(1.0 / 2 * (start.X + end.X)), (int)(start.Y - high));
                        Point triVertex3 = new Point(end.X, start.Y);
                        // Vẽ các cạnh dựa vào tọa độ các đỉnh
                        Line firstTriL = new Line(triVertex1, triVertex2, size);
                        pointOfShape = firstTriL.Draw();
                        Line secondTriL = new Line(triVertex2, triVertex3, size);
                        pointOfShape = pointOfShape.Concat(secondTriL.Draw()).ToList();
                        Line thirdTriL = new Line(triVertex3, triVertex1, size);
                        pointOfShape = pointOfShape.Concat(thirdTriL.Draw()).ToList();
                        // Lưu danh sách các cạnh
                        lineOfShape.Add(firstTriL);
                        lineOfShape.Add(secondTriL);
                        lineOfShape.Add(thirdTriL);
                        // Lưu tâm hình tam giác
                        int CenterTX = Math.Abs(triVertex1.X + triVertex2.X + triVertex3.X) / 3;
                        int CenterTY = Math.Abs(triVertex1.Y+ triVertex2.Y + triVertex3.Y) / 3;
                        Point CenterT = new Point(CenterTX, CenterTY);
                        // Lưu vị trí các điểm điều khiển
                        controlPoint.Add(CenterT);
                        controlPoint.Add(triVertex1);
                        controlPoint.Add(triVertex2);
                        controlPoint.Add(triVertex3);
                        controlPointDrawed = DrawControlPoint(CenterT);
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(triVertex1)).ToList();
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(triVertex2)).ToList();
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(triVertex3)).ToList();
                        break;
                    // Vẽ ngũ giác đều
                    case 5:
                        // Các đỉnh của ngũ giác đều được tính dựa vào điểm ấn và thả chuột
                        // Chương trình vẽ 1 cạnh của ngũ giác đều song song với trục ngang, không quan tâm đến tung độ điểm thả chuột
                        Point pentaVertex1 = new Point(start.X, start.Y);
                        Point pentaVertex2 = new Point(end.X, start.Y);
                        double length = end.X - start.X;
                        double Dx = length * Math.Cos(2 * Math.PI / 5);
                        double Dy = length * Math.Sin(2 * Math.PI / 5);
                        Point pentaVertex3 = new Point((int)(end.X + Dx), (int)(start.Y - Dy));
                        Point pentaVertex4 = new Point((int)(1.0 / 2 * (end.X + start.X)), (int)(start.Y - Dy - Dy * Math.Cos(Math.PI / 5)));
                        Point pentaVertex5 = new Point((int)(start.X - Dx), (int)(start.Y - Dy));
                        // Vẽ các cạnh dựa vào danh sách các đỉnh
                        Line pentaL1 = new Line(pentaVertex1, pentaVertex2, size);
                        pointOfShape = pentaL1.Draw();
                        Line pentaL2 = new Line(pentaVertex2, pentaVertex3, size);
                        pointOfShape = pointOfShape.Concat(pentaL2.Draw()).ToList();
                        Line pentaL3 = new Line(pentaVertex3, pentaVertex4, size);
                        pointOfShape = pointOfShape.Concat(pentaL3.Draw()).ToList();
                        Line pentaL4 = new Line(pentaVertex4, pentaVertex5, size);
                        pointOfShape = pointOfShape.Concat(pentaL4.Draw()).ToList();
                        Line pentaL5 = new Line(pentaVertex5, pentaVertex1, size);
                        pointOfShape = pointOfShape.Concat(pentaL5.Draw()).ToList();
                        // Lưu danh sách các cạnh
                        lineOfShape.Add(pentaL1);
                        lineOfShape.Add(pentaL2);
                        lineOfShape.Add(pentaL3);
                        lineOfShape.Add(pentaL4);
                        lineOfShape.Add(pentaL5);
                        // Lưu tâm ngũ giác đều
                        int CenterPX = Math.Abs(pentaVertex1.X + pentaVertex2.X + pentaVertex3.X + pentaVertex4.X + pentaVertex5.X) / 5;
                        int CenterPY = Math.Abs(pentaVertex1.Y + pentaVertex2.Y + pentaVertex3.Y + pentaVertex4.Y + pentaVertex5.Y) / 5;
                        Point CenterP = new Point(CenterPX, CenterPY);
                        // Lưu vị trí các điểm điều khiển
                        controlPoint.Add(CenterP);
                        controlPoint.Add(pentaVertex1);
                        controlPoint.Add(pentaVertex2);
                        controlPoint.Add(pentaVertex3);
                        controlPoint.Add(pentaVertex4);
                        controlPoint.Add(pentaVertex5);
                        controlPointDrawed = DrawControlPoint(CenterP);
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(pentaVertex1)).ToList();
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(pentaVertex2)).ToList();
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(pentaVertex3)).ToList();
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(pentaVertex4)).ToList();
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(pentaVertex5)).ToList();
                        break;
                    // Vẽ lục giác đều
                    case 6:
                        // Lục giác đều được vẽ có các cạnh đáy song song với trục ngang, không quan tâm tung độ điểm thả chuột
                        int D = (end.X - start.X) / 2;
                        int H = (int)(D * Math.Sqrt(3) / 2);
                        Point hexaVertex1 = new Point(start.X, start.Y);
                        Point hexaVertex2 = new Point(start.X + D / 2, start.Y - H);
                        Point hexaVertex3 = new Point(start.X + 3 * D / 2, start.Y - H);
                        Point hexaVertex4 = new Point(end.X, start.Y);
                        Point hexaVertex5 = new Point(start.X + 3 * D / 2, start.Y + H);
                        Point hexaVertex6 = new Point(start.X + D / 2, start.Y + H);
                        // Vẽ các cạnh dựa trên các đỉnh
                        Line hexaL1 = new Line(hexaVertex1, hexaVertex2, size);
                        pointOfShape = hexaL1.Draw();
                        Line hexaL2 = new Line(hexaVertex2, hexaVertex3, size);
                        pointOfShape = pointOfShape.Concat(hexaL2.Draw()).ToList();
                        Line hexaL3 = new Line(hexaVertex3, hexaVertex4, size);
                        pointOfShape = pointOfShape.Concat(hexaL3.Draw()).ToList();
                        Line hexaL4 = new Line(hexaVertex4, hexaVertex5, size);
                        pointOfShape = pointOfShape.Concat(hexaL4.Draw()).ToList();
                        Line hexaL5 = new Line(hexaVertex5, hexaVertex6, size);
                        pointOfShape = pointOfShape.Concat(hexaL5.Draw()).ToList();
                        Line hexaL6 = new Line(hexaVertex6, hexaVertex1, size);
                        pointOfShape = pointOfShape.Concat(hexaL6.Draw()).ToList();
                        // Lưu danh sách các cạnh
                        lineOfShape.Add(hexaL1);
                        lineOfShape.Add(hexaL2);
                        lineOfShape.Add(hexaL3);
                        lineOfShape.Add(hexaL4);
                        lineOfShape.Add(hexaL5);
                        lineOfShape.Add(hexaL6);
                        // Lưu tâm lục giác đều
                        int CenterHX = Math.Abs(hexaVertex1.X  + hexaVertex4.X) / 2;
                        int CenterHY = Math.Abs(hexaVertex1.Y + hexaVertex4.Y) / 2;
                        Point CenterH = new Point(CenterHX, CenterHY);
                        // Lưu vị trí các điểm điều khiển
                        controlPoint.Add(CenterH);
                        controlPoint.Add(hexaVertex1);
                        controlPoint.Add(hexaVertex2);
                        controlPoint.Add(hexaVertex3);
                        controlPoint.Add(hexaVertex4);
                        controlPoint.Add(hexaVertex5);
                        controlPoint.Add(hexaVertex6);
                        controlPointDrawed = DrawControlPoint(CenterH);
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(hexaVertex1)).ToList();
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(hexaVertex2)).ToList();
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(hexaVertex3)).ToList();
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(hexaVertex4)).ToList();
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(hexaVertex5)).ToList();
                        controlPointDrawed = controlPointDrawed.Concat(DrawControlPoint(hexaVertex6)).ToList();
                        break;
                }
            }
        }
        //Hàm trả về danh sách các điểm đã xác định
        public List<Point> getPointOfShape()
        {
            return pointOfShape;
        }
        public List<Point> getControlPointDrawed(Point clickPoint)
        {
            for(int i = 0; i < controlPoint.Count; i++)
            {
                float DistanceX = (clickPoint.X - controlPoint[i].X) * (clickPoint.X - controlPoint[i].X);
                float DistanceY = (clickPoint.Y - controlPoint[i].Y) * (clickPoint.Y - controlPoint[i].Y);
                float Distance = DistanceX + DistanceY;
                if (Distance < 2000)
                    return controlPointDrawed;
            }
            return new List<Point>();
        }
        public List<Point> getControlPoint()
        {
            return controlPoint;
        }
        public List<Line> getLineOfShape()
        {
            return lineOfShape;
        }
        public short getShShape()
        {
            return shShape;
        }
        public short getSize()
        {
            return size;
        }
    }
}
