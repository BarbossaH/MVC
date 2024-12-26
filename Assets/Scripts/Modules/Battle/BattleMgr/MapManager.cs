using System.Collections.Generic;
using Character;
using Common;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BattleMgr
{
    /// <summary>
    /// 地图管理器，存储地图的网格信息
    /// </summary>
    public class MapManager
    {
        private Tilemap _tileMap;
        
        public BattleGrid[,] BattleGrids;

        public int RowCount;
        public int ColumnCount;

        public void InitTileMap()
        {
            _tileMap = GameObject.Find("Grid/ground").GetComponent<Tilemap>();
            
            // todo： 地图大小，可将这个信息写道配置表中进行设置
            RowCount = 12;
            ColumnCount = 20;
            BattleGrids = new BattleGrid[RowCount, ColumnCount];
            
            //临时记录瓦片地图每个格子的位置
            List<Vector3Int> cells = new List<Vector3Int>();

            foreach (var pos in _tileMap.cellBounds.allPositionsWithin)
            {
                if(_tileMap.HasTile(pos))
                {
                    cells.Add(pos);
                }
            }
            
            //将一维数组的位置信息转变成二维数组
            Object prefab = Resources.Load("Model/block");

            for (int i = 0; i< cells.Count; i++)
            {
                int row = i/ColumnCount;
                int col = i % (ColumnCount);
                BattleGrid grid = ((GameObject)Object.Instantiate(prefab)).AddComponent<BattleGrid>();
                grid.RowIndex = row;
                grid.ColumnIndex = col;
                // grid.transform.position = _tileMap.CellToWorld(cells[i])+new Vector3(0.5f,0.5f,0f);
                grid.transform.position = _tileMap.GetCellCenterWorld(cells[i]);
                // grid.transform.position = new Vector3(grid.transform.position.x, grid.transform.position.y, 0);
                BattleGrids[row,col] = grid;
            }
        }

        public GridType GetGridType(int row, int col)
        {
            return BattleGrids[row,col].Type;
        }

        public void ShowStepGrid(CharacterBase c, int steps)
        {
            _BFS bfs = new _BFS(RowCount, ColumnCount);
            List<_BFS.Point> points = bfs.Seach(c.rowIndex,c.colIndex,steps);
            for (int i = 0; i < points.Count; i++)
            {
                BattleGrids[points[i].X, points[i].Y].ShowGrid(Color.green);
            }
        }

        public void HideStepGrid(CharacterBase c, int steps)
        {
            _BFS bfs = new _BFS(RowCount, ColumnCount);
            List<_BFS.Point> points = bfs.Seach(c.rowIndex,c.colIndex,steps);
            for (int i = 0; i < points.Count; i++)
            {
                BattleGrids[points[i].X, points[i].Y].HideGrid();
            }
        }
    }
}