namespace Platformer2D.Assets.LevelScripts
{
    internal sealed class CellDirectionSet
    {
        public bool up;
        public bool down;
        public bool left;
        public bool right;

        public CellDirectionSet(bool up, bool down, bool left, bool right)
        {
            this.up = up;
            this.down = down;
            this.left = left;
            this.right = right;
        }
    }
}
