﻿using Platformer2D.Assets.AnimationScripts;
using Platformer2D.Assets.LevelScripts;
using Platformer2D.Assets.PathFinder;
using Platformer2D.Assets.Settings;

namespace Platformer2D.Assets.Enemy.Bat
{
    internal sealed class BatController
    {
        private const float RANDOM_ANIMATION_SPEED_DEVIATION = 10f;
        private BatData data;
        private AnimationController animationController;
        private BatTargetMove batTargetMove;
        private BatMoveController batMoveController;
        private BatPatrolController batPatrolController;
        private PathFinderManager pathFinderManager;
        private LevelData levelData;

        public BatController(BatData data, PathFinderManager pathFinderManager, LevelData levelData)
        {
            this.data = data;
            this.pathFinderManager = pathFinderManager;
            this.levelData = levelData;

            animationController = new AnimationController(data.view.spriteRenderer);
            animationController.Play(new AnimationFactory().CreateRandomSpeedDeviation(ResourcesPathes.BAT_FLY_ANNIMATION_TRACK, RANDOM_ANIMATION_SPEED_DEVIATION));
            batTargetMove = new BatTargetMove(data.view.transformView.position);
            batMoveController = new BatMoveController(data, batTargetMove);
            batPatrolController = new BatPatrolController(data, batTargetMove, pathFinderManager, levelData);
        }

        public void Update(float deltaTime)
        {
            animationController.Update(deltaTime);
            batPatrolController.Update();
            batMoveController.Update(deltaTime);
        }
    }
}
