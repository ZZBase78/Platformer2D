using Platformer2D.Assets.Extention;
using Platformer2D.Assets.LevelScripts;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D.Assets.Enemy.Cannon
{
    internal sealed class CannonLevelPlacer
    {

        internal sealed class CannonPosition
        {
            public Vector2 position;
            public Vector2 direction;

            public CannonPosition(Vector2 position, Vector2 direction)
            {
                this.position = position;
                this.direction = direction;
            }
        }

        private bool[] templateUp = { false, false, false, false, false, false, true, true, true };
        private bool[] templateDown = { true, true, true, false, false, false, false, false, false };
        private bool[] templateLeft = { false, false, true, false, false, true, false, false, true };
        private bool[] templateRight = { true, false, false, true, false, false, true, false, false };

        private Vector2 placeOffSet = new Vector2(0.5f, 0.5f);

        private LevelData levelData;
        private LevelWallChecker levelWallChecker;
        private CannonFactory cannonFactory;
        private int countToPlace;
        private float cannonMinDistantion;

        public CannonLevelPlacer(LevelData levelData)
        {
            this.levelData = levelData;
            levelWallChecker = new LevelWallChecker(levelData);
            countToPlace = levelData.levelConfig.levelConfigSettings.cannonCount;
            cannonMinDistantion = levelData.levelConfig.levelConfigSettings.cannonMinDistantion;
            cannonFactory = new CannonFactory();
        }

        public List<CannonData> PlaceCannons()
        {
            List<CannonPosition> cannonPositions = new List<CannonPosition>();

            List<CannonPosition> possiblePositions = FindPossiblePositions();

            while (countToPlace > 0 && possiblePositions.Count > 0)
            {
                CannonPosition randomPosition = possiblePositions.GetRandom();
                
                possiblePositions.Remove(randomPosition);

                if (CheckDistantion(randomPosition, cannonPositions))
                {
                    cannonPositions.Add(randomPosition);
                    countToPlace--;
                }
            }

            List<CannonData> cannons = DisplayCannons(cannonPositions);

            return cannons;
        }

        private List<CannonData> DisplayCannons(List<CannonPosition> cannonPositions)
        {
            List<CannonData> cannonsData = new List<CannonData>();
            foreach(CannonPosition cannonPosition in cannonPositions)
            {
                cannonsData.Add(DisplayCannon(cannonPosition));
            }

            return cannonsData;
        }

        private CannonData DisplayCannon(CannonPosition cannonPosition)
        {
            CannonData cannonData = cannonFactory.GetCannon();
            cannonData.view.transform.position = cannonPosition.position + placeOffSet;
            cannonData.view.transform.up = cannonPosition.direction;

            return cannonData;
        }

        private bool CheckDistantion(CannonPosition currentPosition, List<CannonPosition> cannonPositions)
        {
            foreach (CannonPosition present in cannonPositions)
            {
                float distantion = (present.position - currentPosition.position).magnitude;
                if (distantion < cannonMinDistantion) return false;
            }

            return true;
        }

        private List<CannonPosition> FindPossiblePositions()
        {
            List<CannonPosition> positions = new List<CannonPosition>();

            for(int x = 1; x < levelData.width - 1; x++)
            {
                for (int y = 1; y < levelData.height - 1; y++)
                {
                    if (CanPlace(x, y, templateUp)) positions.Add(new CannonPosition(new Vector2(x, y), Vector2.up));
                    if (CanPlace(x, y, templateDown)) positions.Add(new CannonPosition(new Vector2(x, y), Vector2.down));
                    if (CanPlace(x, y, templateLeft)) positions.Add(new CannonPosition(new Vector2(x, y), Vector2.left));
                    if (CanPlace(x, y, templateRight)) positions.Add(new CannonPosition(new Vector2(x, y), Vector2.right));
                }
            }

            return positions;
        }

        private bool CanPlace(int x, int y, bool[] template)
        {
            return
                levelWallChecker.IsWall(x - 1, y + 1) == template[0] &&
                levelWallChecker.IsWall(x, y + 1) == template[1] &&
                levelWallChecker.IsWall(x + 1, y + 1) == template[2] &&
                levelWallChecker.IsWall(x - 1, y) == template[3] &&
                levelWallChecker.IsWall(x, y) == template[4] &&
                levelWallChecker.IsWall(x + 1, y) == template[5] &&
                levelWallChecker.IsWall(x - 1, y - 1) == template[6] &&
                levelWallChecker.IsWall(x, y - 1) == template[7] &&
                levelWallChecker.IsWall(x + 1, y - 1) == template[8];
        }
    }
}
