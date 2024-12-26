
using Common.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    /// <summary>
    /// 拖拽出来的图标界面
    /// </summary>
    public class DragHeroView:BaseView
    {
        private Camera _mainCamera;
        private Image _image; 
        protected override void OnAwake()
        {
            base.OnAwake();
            _mainCamera = Camera.main;
            _image = GetComponent<Image>();
        }

        private void Update()
        {
            //拖拽中跟随鼠标移动
            if (CanvasComponent.enabled)
            {
                if (_mainCamera is not null)
                {
                    Vector2 worldPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    transform.position = worldPos;
                }
            }
        }

        public override void ShowView(params object[] args)
        {
            base.ShowView(args);
            // Debug.Log(args[0]);
            _image.SetIcon((string)args[0]);
        }
    }
}