namespace Platformer2D.Assets.LevelScripts
{
    internal sealed class LevelWallChecker
    {
        private LevelData levelData;
        private int width;
        private int height;

        public LevelWallChecker(LevelData levelData)
        {
            this.levelData = levelData;
            this.width = levelData.width;
            this.height = levelData.height;
        }

        public bool IsWall(int x, int y)
        {
            if (x < 0 || x >= width || y < 0 || y >= height) return false;

            return levelData.levelElements[x, y].isWall;
        }
    }
}
