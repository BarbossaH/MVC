using System;
using Common;
using Unity.VisualScripting;
using UnityEngine;

namespace Components
{
    public class BuildingEntrance : MonoBehaviour
    {
        public int LevelId;

        private void OnTriggerEnter2D(Collider2D other)
        {
            GameManager.EventCenter.PostEvent(Defines.ShowLevelDesEvent, LevelId);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            GameManager.EventCenter.PostEvent(Defines.HideLevelDesEvent);
        }
    }
}