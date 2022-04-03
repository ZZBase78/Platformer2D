using UnityEngine;

namespace Platformer2D.Assets.CameraScripts
{
    internal sealed class CameraData
    {
        public float moveSpeed;
        public float zoomSpeed;

        public float targetZoom;
        public Transform target;
        public bool isTargetSet;
    }
}
