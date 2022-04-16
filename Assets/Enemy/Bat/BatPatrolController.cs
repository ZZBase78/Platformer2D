using Platformer2D.Assets.LevelScripts;
using Platformer2D.Assets.PathFinder;
using UnityEngine;

namespace Platformer2D.Assets.Enemy.Bat
{
    internal sealed class BatPatrolController
    {
        private const float MIN_DISTANCE = 0.05f;

        private LevelData levelData;
        private LevelCoordinator levelCoordinator;
        private BatData batData;
        private BatTargetMove batTargetMove;
        private PathFinderManager pathFinderManager;
        private PathFinderResult pathFinderResult;
        private PathFinderTask currentPathFinderTask;
        private bool isPathFindingInProgress;
        private BatState batState;

        public BatPatrolController(BatData batData, BatTargetMove batTargetMove, PathFinderManager pathFinderManager, LevelData levelData)
        {
            this.batData = batData;
            this.batTargetMove = batTargetMove;
            this.pathFinderManager = pathFinderManager;
            this.levelData = levelData;
            levelCoordinator = new LevelCoordinator(levelData);
            pathFinderResult = null;
            isPathFindingInProgress = false;
            batState = batData.state;
            currentPathFinderTask = null;
        }

        private void CheckBatStateChange()
        {
            if (batState == batData.state) return;
            batState = batData.state;
            if (batData.state != BatState.Patrol)
            {
                if (currentPathFinderTask != null)
                {
                    pathFinderManager.StopFinding(currentPathFinderTask);
                    isPathFindingInProgress = false;
                    pathFinderManager.StopFinding(currentPathFinderTask);
                    currentPathFinderTask = null;
                    pathFinderResult = null;
                }
            }
        }

        private bool CheckDistance()
        {
            float currentDistance = (batTargetMove.smoothTarget - batTargetMove.target).magnitude;
            return currentDistance <= MIN_DISTANCE;
        }

        private bool SetNextPathTarget()
        {
            if (pathFinderResult == null) return false;
            if (pathFinderResult.resultPath.Count == 0) return false;
            batTargetMove.target = pathFinderResult.resultPath[0];
            pathFinderResult.resultPath.RemoveAt(0);
            return true;
        }

        private void NewPathFind(PathFinderResult pathFinderResult)
        {
            if (pathFinderResult.task != currentPathFinderTask) return;
            isPathFindingInProgress = false;
            if (pathFinderResult.isFound)
            {
                this.pathFinderResult = pathFinderResult;
            }
            
        }

        private void StartNewFinding()
        {
            float x = Random.Range(0, levelData.width);
            float y = Random.Range(0, levelData.height);
            Vector3 target = new Vector3(x, y, 0f) + (Vector3)levelCoordinator.worldOffSet;

            pathFinderResult = null;
            currentPathFinderTask = pathFinderManager.GetPathResult(batData.view.transformView.position, target, NewPathFind);
            isPathFindingInProgress = true;
        }

        public void Update()
        {
            CheckBatStateChange();
            if (batData.state != BatState.Patrol) return;
            if (isPathFindingInProgress) return;
            if (!CheckDistance()) return;
            if (SetNextPathTarget()) return;

            StartNewFinding();
        }
    }
}
