using Platformer2D.Assets.Extention;
using Platformer2D.Assets.Settings;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D.Assets.LevelScripts
{
    internal sealed class LevelConfigGenerator
    {
        public LevelConfig Generate()
        {

            LevelConfigSettings levelConfigSettings = Resources.Load<LevelConfigSettings>(ResourcesPathes.LEVEL_CONFIG_SETTINGS);

            LevelConfig levelConfig = new LevelConfig(levelConfigSettings);

            List<LevelCellConfig> unreachableCells = GetUnReachableCells(levelConfig);

            //Start cell
            SetReachedCell(0, 0, levelConfig, unreachableCells);

            //Правая верхняя ячейка (выход) не может быть без нижней стены
            LevelCellConfig exitCellConfig = levelConfig.levelCellConfigs[levelConfig.width - 1, levelConfig.height - 1];
            exitCellConfig.forbiddenDirectionSet.down = true;

            CalculateReachableCells(levelConfig, unreachableCells);

            return levelConfig;
        }

        private void AddCellIfReachable(List<LevelCellConfig> list, LevelCellConfig cell)
        {
            if (cell.isReached) list.Add(cell);
        }

        private LevelCellConfig GetLeft(LevelConfig levelConfig, LevelCellConfig cell)
        {
            return levelConfig.levelCellConfigs[cell.x - 1, cell.y];
        }

        private LevelCellConfig GetRight(LevelConfig levelConfig, LevelCellConfig cell)
        {
            return levelConfig.levelCellConfigs[cell.x + 1, cell.y];
        }
        
        private LevelCellConfig GetDown(LevelConfig levelConfig, LevelCellConfig cell)
        {
            return levelConfig.levelCellConfigs[cell.x, cell.y - 1];
        }

        private LevelCellConfig GetUp(LevelConfig levelConfig, LevelCellConfig cell)
        {
            return levelConfig.levelCellConfigs[cell.x, cell.y + 1];
        }

        private List<LevelCellConfig> GetNearReachableCells(LevelConfig levelConfig, LevelCellConfig levelCellConfig)
        {
            List<LevelCellConfig> list = new List<LevelCellConfig>();
            if (!levelCellConfig.forbiddenDirectionSet.up)
            {
                LevelCellConfig upCell = GetUp(levelConfig, levelCellConfig);
                if (!upCell.forbiddenDirectionSet.down) AddCellIfReachable(list, upCell);
            }
            if (!levelCellConfig.forbiddenDirectionSet.down)
            {
                LevelCellConfig downCell = GetDown(levelConfig, levelCellConfig);
                if (!downCell.forbiddenDirectionSet.up) AddCellIfReachable(list, downCell);
            }
            if (!levelCellConfig.forbiddenDirectionSet.left)
            {
                LevelCellConfig leftCell = GetLeft(levelConfig, levelCellConfig);
                if (!leftCell.forbiddenDirectionSet.right) AddCellIfReachable(list, leftCell);
            }
            if (!levelCellConfig.forbiddenDirectionSet.right)
            {
                LevelCellConfig rightCell = GetRight(levelConfig, levelCellConfig);
                if (!rightCell.forbiddenDirectionSet.left) AddCellIfReachable(list, rightCell);
            }

            return list;
        }

        private void SetPerimittedDirection(LevelCellConfig cell1, LevelCellConfig cell2)
        {
            if ((cell1.x == cell2.x - 1) && (cell1.y == cell2.y))
            {
                cell1.permittedDirectionSet.right = true;
                cell2.permittedDirectionSet.left = true;
            }
            if ((cell1.x == cell2.x + 1) && (cell1.y == cell2.y))
            {
                cell1.permittedDirectionSet.left = true;
                cell2.permittedDirectionSet.right = true;
            }
            if ((cell1.y == cell2.y - 1) && (cell1.x == cell2.x))
            {
                cell1.permittedDirectionSet.up = true;
                cell1.forbiddenDirectionSet.down = true;

                cell2.permittedDirectionSet.down = true;
                cell2.forbiddenDirectionSet.up = true;
            }
            if ((cell1.y == cell2.y + 1) && (cell1.x == cell2.x))
            {
                cell1.permittedDirectionSet.down = true;
                cell1.forbiddenDirectionSet.up = true;

                cell2.permittedDirectionSet.up = true;
                cell2.forbiddenDirectionSet.down = true;
            }
        }

        private void CalculateReachableCells(LevelConfig levelConfig, List<LevelCellConfig> unreachableCells)
        {
            while(unreachableCells.Count > 0)
            {
                LevelCellConfig levelCellConfig = unreachableCells.GetRandom();
                List<LevelCellConfig> nearCells = GetNearReachableCells(levelConfig, levelCellConfig);
                if (nearCells.Count > 0)
                {
                    LevelCellConfig nearCell = nearCells.GetRandom();
                    SetPerimittedDirection(levelCellConfig, nearCell);
                    SetReachedCell(levelCellConfig, unreachableCells);
                }
            }
        }

        private void SetReachedCell(int x, int y, LevelConfig levelConfig, List<LevelCellConfig> unreachableCells)
        {
            LevelCellConfig cellConfig = levelConfig.levelCellConfigs[x, y];
            SetReachedCell(cellConfig, unreachableCells);
        }

        private void SetReachedCell(LevelCellConfig cell, List<LevelCellConfig> unreachableCells)
        {
            cell.isReached = true;
            unreachableCells.Remove(cell);
        }

        private List<LevelCellConfig> GetUnReachableCells(LevelConfig levelConfig)
        {
            List<LevelCellConfig> list = new List<LevelCellConfig>();

            for (int x = 0; x < levelConfig.width; x++)
            {
                for(int y = 0; y < levelConfig.height; y++)
                {
                    LevelCellConfig cellConfig = levelConfig.levelCellConfigs[x, y];
                    if (!cellConfig.isReached) list.Add(cellConfig);
                }
            }

            return list;
        }
    }
}
