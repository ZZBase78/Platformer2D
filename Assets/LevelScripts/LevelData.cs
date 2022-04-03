namespace Platformer2D.Assets.LevelScripts
{
    internal sealed class LevelData
    {
        public int width;
        public int height;
        public LevelElement[,] levelElements;
        public LevelConfig levelConfig;

        public LevelData(LevelConfig levelConfig)
        {
            this.levelConfig = levelConfig;

            width = levelConfig.width * levelConfig.levelConfigSettings.cellWidth + (levelConfig.width + 1) * levelConfig.levelConfigSettings.wallWidth;
            height = levelConfig.height * levelConfig.levelConfigSettings.cellHeight + (levelConfig.height + 1) * levelConfig.levelConfigSettings.wallHeight;

            InitLevelElements();
        }

        private void InitLevelElements()
        {
            levelElements = new LevelElement[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    levelElements[x, y] = new LevelElement();
                }
            }
        }
    }
}
