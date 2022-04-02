namespace Platformer2D.Assets.LevelScripts
{
    internal sealed class LevelConfig
    {
        public int width;
        public int height;
        public LevelCellConfig[,] levelCellConfigs;

        public LevelConfig(int width, int height)
        {
            this.width = width;
            this.height = height;
            levelCellConfigs = new LevelCellConfig[width, height];
            StartConfiguration();
        }

        private CellDirectionSet GetStartForbiddenDirectionSet(int x, int y)
        {
            bool down = y == 0;
            bool up = y == height - 1;
            bool left = x == 0;
            bool right = x == width - 1;
            return new CellDirectionSet(up, down, left, right);
        }

        private CellDirectionSet GetStartPermittedDirectionSet()
        {
            return new CellDirectionSet(false, false, false, false);
        }

        private LevelCellConfig GetStartLevelCellConfig(int x, int y)
        {
            CellDirectionSet forbiddenDirectionSet = GetStartForbiddenDirectionSet(x, y);
            CellDirectionSet permittedDirectionSet = GetStartPermittedDirectionSet();
            return new LevelCellConfig(x, y, false, forbiddenDirectionSet, permittedDirectionSet);
        }

        private void StartConfiguration()
        {
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    levelCellConfigs[x, y] = GetStartLevelCellConfig(x, y);
                }
            }
        }
    }
}
