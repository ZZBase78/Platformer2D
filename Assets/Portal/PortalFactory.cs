using Platformer2D.Assets.Settings;
using System;
using UnityEngine;

namespace Platformer2D.Assets.Portal
{
    internal sealed class PortalFactory
    {

        private GameObject prefab;
        private PortalModel model;

        public PortalFactory()
        {
            prefab = Resources.Load<GameObject>(ResourcesPathes.PORTAL_PREFAB);
            model = Resources.Load<PortalModel>(ResourcesPathes.PORTAL_MODEL);
        }

        public PortalData Create()
        {
            PortalData portalData = new PortalData();

            GameObject gameObject = GameObject.Instantiate(prefab);
            PortalView view = gameObject.GetComponent<PortalView>();
            if (view == null) throw new Exception(ErrorMessages.PORTALVIEW_VIEW_NOT_FOUND);
            portalData.view = view;

            portalData.model = model;

            portalData.IsExit = false;

            return portalData;

        }

        public void Destroy(PortalData portalData)
        {
            GameObject.Destroy(portalData.view.gameObject);
            portalData = null;
        }
    }
}
