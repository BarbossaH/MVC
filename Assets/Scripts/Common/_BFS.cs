using System.Collections.Generic;
using System.Linq;
using BattleMgr;

namespace Common
{
    /// <summary>
    /// 广度优先算法
    /// </summary>
    public class _BFS
    {
        public class Point
        {
            public int X; // 行坐标
            public int Y;//列坐标
            public Point Father;
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
            public Point(int x, int y, Point father)
            {
                X = x;
                Y = y;
                Father = father;
            }
        }
        
        public readonly int RowCount;
        public readonly int ColumnCount;
        //store the points found, the key is the concatenated string of row and column index values
        private readonly Dictionary<string, Point> _pointsFound = new ();
        
        public _BFS(int rowCount, int colCount)
        {
            RowCount = rowCount;
            ColumnCount = colCount;
        }
        
        public List<Point> Seach(int startRow, int startCol, int steps)
        {
            List<Point> searchs = new List<Point>();
            Point startPoint = new Point(startRow, startCol);
            searchs.Add(startPoint);
            _pointsFound.Add($"{startRow}_{startCol}", startPoint);

            for (int i = 0; i < steps; i++)
            {
                //a temp list to store the points that match the condition
                List<Point> temps = new List<Point>();
                for (int j = 0; j< searchs.Count; j++)
                {
                    Point currentPoint = searchs[j];
                    //查出当前四周的点
                    FindAroundPoints(currentPoint,temps);
                }
                if(temps.Count ==0) break;
                
                searchs.Clear();
                searchs.AddRange(temps);
            }
            return _pointsFound.Values.ToList();
        }

        private void FindAroundPoints(Point currentPoint, List<Point> points)
        {
            if (currentPoint.X - 1 >= 0)
            {
                AddFinds(currentPoint.X - 1, currentPoint.Y, currentPoint, points);
            }

            if (currentPoint.X + 1 < RowCount)
            {
                AddFinds(currentPoint.X+1, currentPoint.Y, currentPoint, points);
            }

            if (currentPoint.Y-1>=0)
            {
                AddFinds(currentPoint.X, currentPoint.Y-1, currentPoint, points);
            }

            if (currentPoint.Y + 1 < ColumnCount)
            {
                AddFinds(currentPoint.X, currentPoint.Y+1, currentPoint, points);
            }
        }

        private void AddFinds(int row, int col, Point father, List<Point> points)
        {
            if (!_pointsFound.ContainsKey($"{row}_{col}") && GameManager.MapManager.GetGridType(row, col) != GridType.Obstacle)
            {
                Point p= new Point(row, col, father);
                _pointsFound.Add($"{row}_{col}", p);
                points.Add(p);
            }
        }
    }
}