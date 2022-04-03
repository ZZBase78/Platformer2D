namespace Platformer2D.Assets.LevelScripts
{
    internal sealed class LevelData
    {
        public int width;
        public int height;
        public LevelElement[,] levelElements;
        private LevelConfig levelConfig;

        public LevelData(LevelConfig levelConfig)
        {
            this.levelConfig = levelConfig;
        }
    }
}
