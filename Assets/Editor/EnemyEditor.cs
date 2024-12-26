using Character;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;


[CanEditMultipleObjects, CustomEditor((typeof(Enemy)))]    
public class EnemyEditor:Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("设置位置"))
        {
            Tilemap tilemap = GameObject.Find("Grid/ground").GetComponent<Tilemap>();

            var allPos = tilemap.cellBounds.allPositionsWithin;

            int _x = 0;
            int _y = 0;

            //使用MoveNext函数获取到原点的坐标
            if (allPos.MoveNext())
            {
                Vector3Int cell = allPos.Current;
                _x = cell.x;
                _y = cell.y;
            }
            //得到编辑器操作的对象
            Enemy enemy = target as Enemy;
            if (enemy != null)
            {
                //将该对象的位置转化为tilemap中的坐标，把tilemap的坐标的绝对值变成所在的行树；
                Vector3Int cellPos = tilemap.WorldToCell(enemy.transform.position);
                enemy.rowIndex = Mathf.Abs(_y - cellPos.y);
                enemy.colIndex = Mathf.Abs(_x - cellPos.x);
                //将在敌人在tilemap中的坐标转变成世界坐标
                enemy.transform.position = tilemap.CellToWorld(cellPos)+ new Vector3(0.5f,0.5f,-1f);
            }
        }
    }
}
