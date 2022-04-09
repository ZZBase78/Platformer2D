using Platformer2D.Assets.Starter;

namespace Platformer2D.Assets.EndLevel
{
    internal sealed class EndLevelController
    {
        private GameData gameData;

        public EndLevelController(GameData gameData)
        {
            this.gameData = gameData;
        }

        public void LevelExitPortalReached()
        {
            gameData.gameState = GameState.Exit;

            EndLevelUIController endLevelUIController = new EndLevelUIController();
            endLevelUIController.ShowRestart();

            GameRestartLevel gameRestartLevel = new GameRestartLevel();
            endLevelUIController.actionButtonPressed += gameRestartLevel.Go;
        }
    }
}
