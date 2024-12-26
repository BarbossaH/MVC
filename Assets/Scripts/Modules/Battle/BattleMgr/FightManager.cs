using System.Collections.Generic;
using Character;
using UnityEngine;

namespace BattleMgr
{
    public enum GameState
    {
        Idle,
        Enter,
    }
    /// <summary>
    /// 战斗管理器（用于管理战斗相关的实体--双方的角色，地图，格子等）
    /// </summary>
    public class FightManager
    {
        public GameState State = GameState.Idle;

        //当前所处的战斗单元
        private FightUnitBase _currentUnit;

        public FightUnitBase CurrentUnit => _currentUnit;

        private List<Hero> _heroes;
        private  List<Enemy> _enemies;
        private int _roundCounter = 0;
        public FightManager()
        {
            ChangeState(GameState.Idle);
        }
        public void OnUpdate(float dt)
        {
            if (_currentUnit != null && _currentUnit.Update(dt))
            {
                //todo 
            }
            else
            {
                _currentUnit = null;
            }
        }

        //切换战斗状态
        public void ChangeState(GameState newState)
        {
            FightUnitBase current = _currentUnit;
            State = newState;
            switch (State)
            {
                case GameState.Idle:
                    current = new FightIdle();
                    break;
                case GameState.Enter:
                    current = new FightEnter();
                    break;
            }
            current.Init();
        }

        public void CreateHero(BattleGrid grid, Dictionary<string, string> data)
        {
            Object heroPrefab = Resources.Load($"Model/{data["Model"]}");
            GameObject heroObj = Object.Instantiate(heroPrefab) as GameObject;
            if (heroObj != null)
            {
                heroObj.transform.position = new Vector3(grid.transform.position.x, grid.transform.position.y, -1);
                Hero hero = heroObj.AddComponent<Hero>();
                hero.Init(data,grid.RowIndex,grid.ColumnIndex);
                grid.Type = GridType.Obstacle;
                _heroes.Add(hero);
            }
        }

        public void InitHeroAndEnemy()
        {
            _roundCounter = 1;
            _heroes = new List<Hero>();
            _enemies = new List<Enemy>();
            
            //将场景中的敌人脚本进行存储
            
            GameObject []objs = GameObject.FindGameObjectsWithTag("Enemy");

            for (int i = 0; i < objs.Length; i++)
            {
                _enemies.Add(objs[i].GetComponent<Enemy>());
            }
        }
    }
}