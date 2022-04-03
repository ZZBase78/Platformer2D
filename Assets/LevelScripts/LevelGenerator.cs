namespace Platformer2D.Assets.LevelScripts
{
    internal sealed class LevelGenerator
    {
        public LevelData Generate()
        {
            LevelConfig levelConfig = new LevelConfigGenerator().Generate();
            LevelData levelData = new LevelData(levelConfig);
            new LevelBoolWallPlacer(levelData).PlaceBoolWall();
            new LevelWallTileCalulator(levelData).Calulate();

            return levelData;
        }
    }
}
