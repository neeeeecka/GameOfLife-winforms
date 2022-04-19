using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public static class RenderFunctions
    {
        public static void DrawLine(Vector2 point0, Vector2 point1, Color color, Bitmap screen)
        {
            var dist = (point1 - point0).Length();

            if (dist < 1)
                return;

            Vector2 middlePoint = point0 + (point1 - point0) / 2;

            DrawPoint(middlePoint.X, middlePoint.Y, color, screen);
            DrawLine(point0, middlePoint, color, screen);
            DrawLine(middlePoint, point1, color, screen);
        }

        public static void DrawRectangle(Vector2 center, float width, float height, Color color, Bitmap screen)
        {
            for(int i=0; i<width; i++)
            {
                for(int j=0; j<height; j++)
                {
                    DrawPoint(i + center.X, j + center.Y, color, screen);
                }
            }
        }

        public static void DrawPoint(double x, double y, Color color, Bitmap screen)
        {

            if (x > screen.Width - 1 || x < 0)
                return;
            if (y > screen.Height - 1 || y < 0)
                return;

            screen.SetPixel((int)x, (int)y, color);
        }
    }
}
