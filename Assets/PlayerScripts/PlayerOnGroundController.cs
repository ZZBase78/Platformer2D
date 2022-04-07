using Platformer2D.Assets.CollidersScripts;
using Platformer2D.Assets.Settings;
using UnityEngine;

namespace Platformer2D.Assets.PlayerScripts
{
    internal sealed class PlayerOnGroundController
    {
        private const int maxContacts = 8;
        private const float minNormalValue = 0.8f;
        private Player player;
        private ContactPoint2D[] contacts;

        public PlayerOnGroundController(Player player)
        {
            this.player = player;
            contacts = new ContactPoint2D[maxContacts];
        }

        public void Update()
        {
            player.isOnGround = false;

            int contactsCount = player.view.rigidbodyView.GetContacts(contacts);

            for (int i = 0; i < contactsCount; i++)
            {
                if (contacts[i].normal.y > minNormalValue)
                {
                    player.isOnGround = true;
                    return;
                }
            }
        }
    }
}
