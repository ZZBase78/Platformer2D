namespace Platformer2D.Assets.LevelScripts
{
    internal sealed class LevelBoolWallPlacer
    {
        private LevelData levelData;
        private int cellWidthCount;
        private int cellHeightCount;
        private int cellWidth;
        private int cellHeight;
        private int wallWidth;
        private int wallHeight;

        private LevelCoordinator levelCoordinator;

        public LevelBoolWallPlacer(LevelData levelData)
        {
            this.levelData = levelData;

            levelCoordinator = new LevelCoordinator(levelData);

            cellWidth = levelData.levelConfig.levelConfigSettings.cellWidth;
            cellHeight = levelData.levelConfig.levelConfigSettings.cellHeight;
            wallWidth = levelData.levelConfig.levelConfigSettings.wallWidth;
            wallHeight = levelData.levelConfig.levelConfigSettings.wallHeight;

            cellWidthCount = levelData.levelConfig.width;
            cellHeightCount = levelData.levelConfig.height;


        }

        public void PlaceBoolWall()
        {

            for(int cellX = 0; cellX < cellWidthCount; cellX++)
            {
                for (int cellY = 0; cellY < cellHeightCount; cellY++)
                {
                    int levelX = levelCoordinator.GetLevelLeftXFromCellX(cellX);
                    int levelY = levelCoordinator.GetLevelDownYFromCellY(cellY);

                    //Заполняем углы ячейки, они всегда заполнены стенами
                    SetRectWall(levelX - wallWidth, levelY - wallHeight, wallWidth, wallHeight);
                    SetRectWall(levelX - wallWidth, levelY + cellHeight, wallWidth, wallHeight);
                    SetRectWall(levelX + cellWidth, levelY - wallHeight, wallWidth, wallHeight);
                    SetRectWall(levelX + cellWidth, levelY + cellHeight, wallWidth, wallHeight);

                    //Заполняем стены в проходах если нужно
                    LevelCellConfig levelCellConfig = levelData.levelConfig.levelCellConfigs[cellX, cellY];
                    if (!levelCellConfig.permittedDirectionSet.up) SetRectWall(levelX, levelY + cellHeight, cellWidth, wallHeight);
                    if (!levelCellConfig.permittedDirectionSet.down) SetRectWall(levelX, levelY - wallHeight, cellWidth, wallHeight);
                    if (!levelCellConfig.permittedDirectionSet.left) SetRectWall(levelX - wallWidth, levelY, wallWidth, cellHeight);
                    if (!levelCellConfig.permittedDirectionSet.right) SetRectWall(levelX + cellWidth, levelY, wallWidth, cellHeight);
                }
            }
        }

        private void SetRectWall(int startX, int startY, int width, int height)
        {
            int endX = startX + width;
            int endY = startY + height;
            for (int x = startX; x < endX; x++)
            {
                for (int y = startY; y < endY; y++)
                {
                    levelData.levelElements[x, y].isWall = true;
                }
            }
        }
    }
}
