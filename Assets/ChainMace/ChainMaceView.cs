using UnityEngine;

namespace Platformer2D.Assets.ChainMace
{
    internal class ChainMaceView : MonoBehaviour
    {
        public Transform transformView;
        public HingeJoint2D hingeJoint2D;
        public ChainMaceData data;
        public ChainMaceController controller;
    }
}
