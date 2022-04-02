namespace Platformer2D.Assets.LevelScripts
{
    internal sealed class LevelCellConfig
    {
        public int x;
        public int y;
        public bool isReached;
        public CellDirectionSet forbiddenDirectionSet;
        public CellDirectionSet permittedDirectionSet;

        public LevelCellConfig(int x, int y, bool isReached, CellDirectionSet forbiddenDirectionSet, CellDirectionSet permittedDirectionSet)
        {
            this.x = x;
            this.y = y;
            this.isReached = isReached;
            this.forbiddenDirectionSet = forbiddenDirectionSet;
            this.permittedDirectionSet = permittedDirectionSet;
        }
    }
}
