using Platformer2D.Assets.CannonBullet;
using Platformer2D.Assets.PlayerScripts;
using Platformer2D.Assets.Starter;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D.Assets.Enemy.Cannon
{
    internal sealed class CannonManager
    {
        private Dictionary<CannonData, CannonController> controllers;
        private Player player;
        private CannonBulletManager cannonBulletManager;

        public CannonManager(List<CannonData> cannons, GameData gameData, Player player)
        {
            cannonBulletManager = new CannonBulletManager();

            this.player = player;

            controllers = new Dictionary<CannonData, CannonController>();
            foreach(CannonData cannonData in cannons)
            {
                controllers.Add(cannonData, new CannonController(gameData, cannonData, player, cannonBulletManager));
            }

        }

        public void Update(float deltaTime)
        {
            foreach(var keyValuePair in controllers)
            {
                keyValuePair.Value.Update(deltaTime);
            }
            cannonBulletManager.Update(deltaTime);
        }
    }
}
