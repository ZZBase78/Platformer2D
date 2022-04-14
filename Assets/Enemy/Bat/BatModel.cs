using UnityEngine;

namespace Platformer2D.Assets.Enemy.Bat
{
    [CreateAssetMenu(fileName = "BatModel", menuName = "ScriptableObjects/Bat/BatModel", order = 1)]
    internal sealed class BatModel : ScriptableObject
    {
        public float normalSpeed;
        public float chaseSpeed;
    }
}
