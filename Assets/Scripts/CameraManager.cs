
    using UnityEngine;

    public class CameraManager
    {
        private readonly Transform _cameraTransform;
        private readonly Vector3 _prePos;

        public CameraManager()
        {
            if (Camera.main != null) _cameraTransform = Camera.main.transform;
            if (_cameraTransform != null) _prePos = _cameraTransform.position;
        }

        public void SetPos(Vector3 pos)
        {
            pos.z = _cameraTransform.position.z;
            _cameraTransform.position = pos;
        }

        public void ResetPos()
        {
            _cameraTransform.position = _prePos;
        }
    }
    
    
