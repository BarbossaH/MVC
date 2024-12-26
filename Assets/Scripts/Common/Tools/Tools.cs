using UnityEngine;

namespace Common.Tools
{
    public static class Tools
    {
        public static void SetIcon(this UnityEngine.UI.Image img, string iconName)
        {
            img.sprite = Resources.Load<Sprite>($"Icon/{iconName}");
        }

        //check if there is 2d collider on mouse pointing position
        public static void ScreenPointToRay2D(Camera camera, Vector2 mousePos, System.Action<Collider2D> callback)
        {
            Vector3 worldPoint = camera.ScreenToWorldPoint(mousePos);
            // Collider2D collider = Physics2D.OverlapPoint(worldPoint);
            Collider2D collider = Physics2D.OverlapCircle(worldPoint,0.02f);
            callback?.Invoke(collider);
        }
    }
}