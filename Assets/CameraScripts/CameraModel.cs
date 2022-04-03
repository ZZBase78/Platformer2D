using UnityEngine;

namespace Platformer2D.Assets.CameraScripts
{
    [CreateAssetMenu(fileName = "CameraModel", menuName = "ScriptableObjects/Camera/CameraModel", order = 1)]
    internal sealed class CameraModel : ScriptableObject
    {
        public float moveSpeed;
        public float zoomSpeed;
    }
}
