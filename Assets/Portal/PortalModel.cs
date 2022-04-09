using Platformer2D.Assets.AnimationScripts;
using UnityEngine;

namespace Platformer2D.Assets.Portal
{
    [CreateAssetMenu(fileName = "PortalModel", menuName = "ScriptableObjects/Portal/PortalModel", order = 1)]
    internal sealed class PortalModel : ScriptableObject
    {
        public AnimationTrack animation;
    }
}
