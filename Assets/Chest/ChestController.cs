namespace Platformer2D.Assets.Chest
{
    internal sealed class ChestController
    {
        private ChestAnimationController chestAnimationController;

        public ChestController(ChestData data)
        {
            chestAnimationController = new ChestAnimationController(data.view.chestRigidbody2D, data.view.spriteRenderer);
        }

        public void Update(float deltaTime)
        {
            chestAnimationController.Update(deltaTime);
        }
    }
}
