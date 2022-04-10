using UnityEngine;

namespace Platformer2D.Assets.PlayerBullet
{
    internal sealed class PlayerBulletMoveController
    {
        public void Move(PlayerBulletData playerBulletData, float deltaTime)
        {
            Vector2 translate = playerBulletData.view.transformView.up * playerBulletData.speed * deltaTime;
            playerBulletData.view.transformView.Translate(translate, Space.World);
        }
    }
}
