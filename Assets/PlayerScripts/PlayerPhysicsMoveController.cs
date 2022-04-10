using Platformer2D.Assets.CollidersScripts;
using Platformer2D.Assets.Settings;
using Platformer2D.Assets.Starter;
using UnityEngine;

namespace Platformer2D.Assets.PlayerScripts
{
    internal sealed class PlayerPhysicsMoveController
    {
        private const int MAX_COLLIDERS_CONTROL = 8;
        private Vector2 HORISONTAL_OFFSET = new Vector2(0.52f, 0f);
        private Vector2 COLLIDER_SIZE = new Vector2(0.05f, 0.9f);

        private Player player;
        private GameData gameData;
        private bool leftKey;
        private bool rightKey;

        private CollidersController collilersController;

        public PlayerPhysicsMoveController(GameData gameData, Player player)
        {
            this.player = player;
            this.gameData = gameData;
            collilersController = new CollidersController(MAX_COLLIDERS_CONTROL);
        }

        public void Update(float deltaTime)
        {
            PlayerState newPlayerState = PlayerState.Stand;
            leftKey = false;
            rightKey = false;
            if (gameData.gameState == GameState.Playing)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    leftKey = true;
                    newPlayerState = PlayerState.MoveLeft;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    rightKey = true;
                    newPlayerState = PlayerState.MoveRight;
                }
            }

            player.playerState = newPlayerState;
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            Vector3 direction = Vector3.zero;
            bool canMove = true;
            if (leftKey)
            {
                direction = Vector3.left;

                Vector2 point = (Vector2)player.view.transformView.position - HORISONTAL_OFFSET;
                Collider2D wallCollider = collilersController.CheckTag(GameTags.WALL, point, COLLIDER_SIZE, 0);
                canMove = wallCollider == null;
            }
            if (rightKey)
            {
                direction = Vector3.right;

                Vector2 point = (Vector2)player.view.transformView.position + HORISONTAL_OFFSET;
                Collider2D wallCollider = collilersController.CheckTag(GameTags.WALL, point, COLLIDER_SIZE, 0);
                canMove = wallCollider == null;
            }

            if (canMove && (leftKey || rightKey))
            {
                direction.x = direction.x * player.moveSpeed - player.view.rigidbodyView.velocity.x;
                player.view.rigidbodyView.AddForce(direction, ForceMode2D.Impulse);
            }
            else
            {
                Vector2 velocity = player.view.rigidbodyView.velocity;
                velocity.x = 0;
                player.view.rigidbodyView.velocity = velocity;
            }
        }
    }
}
