using Platformer2D.Assets.Starter;

namespace Platformer2D.Assets.EndLevel
{
    internal sealed class EndLevelController
    {
        private GameData gameData;
        private EndLevelUIController endLevelUIController;

        public EndLevelController(GameData gameData)
        {
            this.gameData = gameData;
        }

        private void CheckNew()
        {
            if (endLevelUIController != null)
            {
                endLevelUIController.Destroy();
                endLevelUIController = null;
            }
        }

        public void LevelExitPortalReached()
        {
            CheckNew();
            gameData.gameState = GameState.Exit;

            endLevelUIController = new EndLevelUIController();
            endLevelUIController.ShowNextLevel();

            GameRestartLevel gameRestartLevel = new GameRestartLevel();
            endLevelUIController.actionButtonPressed += gameRestartLevel.Go;
        }

        public void PlayerDie()
        {
            CheckNew();
            gameData.gameState = GameState.PlayerDie;

            endLevelUIController = new EndLevelUIController();
            endLevelUIController.ShowRestart();

            GameRestartLevel gameRestartLevel = new GameRestartLevel();
            endLevelUIController.actionButtonPressed += gameRestartLevel.Go;
        }
    }
}
