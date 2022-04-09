using Platformer2D.Assets.Interfaces;
using UnityEngine;

namespace Platformer2D.Assets.Portal
{
    internal sealed class PortalView : MonoBehaviour, IExit
    {
        public Transform transformView;
        public SpriteRenderer spriteRenderer;
        public PortalController portalController;

        public bool IsExit()
        {
            return portalController.IsExit();
        }
    }
}
