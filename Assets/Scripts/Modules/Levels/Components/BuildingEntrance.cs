using System;
using Common;
using Common.Def;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Components
{
    public class BuildingEntrance : MonoBehaviour
    {
        public int levelId;

        private void OnTriggerEnter2D(Collider2D other)
        {
            GameManager.EventCenter.PostEvent(CallbackFuncName.ShowLevelDesEvent, levelId);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            GameManager.EventCenter.PostEvent(CallbackFuncName.HideLevelDesEvent);
        }
    }
}