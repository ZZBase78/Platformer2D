namespace Platformer2D.Assets.PlayerScripts
{
    internal sealed class Player
    {
        public PlayerView view;
        public float moveSpeed;
        public float jumpForce;
        public PlayerState playerState;
        public bool isOnGround;
        public float health;
    }
}
