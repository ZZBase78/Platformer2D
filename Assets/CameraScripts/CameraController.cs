using Platformer2D.Assets.Settings;
using UnityEngine;

namespace Platformer2D.Assets.CameraScripts
{
    internal sealed class CameraController
    {
        private Camera camera;
        private Transform cameraTransform;
        private CameraData cameraData;

        public CameraController()
        {
            camera = Camera.main;
            cameraTransform = camera.transform;
            InitCameraData();
        }

        public void UnSetTarget()
        {
            cameraData.target = null;
            cameraData.isTargetSet = false;
        }

        private void InitCameraData()
        {
            cameraData = new CameraData();

            CameraModel cameraModel = Resources.Load<CameraModel>(ResourcesPathes.CAMERA_MODEL);

            cameraData.moveSpeed = cameraModel.moveSpeed;
            cameraData.zoomSpeed = cameraModel.zoomSpeed;

            cameraData.targetZoom = camera.orthographicSize;

            cameraData.target = null;
            cameraData.isTargetSet = false;
        }

        private void Move(float deltaTime)
        {
            if (cameraData.isTargetSet)
            {
                Vector3 newPosition = Vector3.Lerp(cameraTransform.position, cameraData.target.position, deltaTime * cameraData.moveSpeed);
                newPosition.z = cameraTransform.position.z;

                cameraTransform.position = newPosition;
            }
        }

        private void Zoom(float deltaTime)
        {
            float newZoom = Mathf.Lerp(camera.orthographicSize, cameraData.targetZoom, deltaTime * cameraData.zoomSpeed);
            camera.orthographicSize = newZoom;
        }

        public void Update(float deltaTime)
        {
            Move(deltaTime);
            Zoom(deltaTime);
        }

        public void SetTarget(Transform transform)
        {
            cameraData.target = transform;
            cameraData.isTargetSet = true;
        }
    }
}
