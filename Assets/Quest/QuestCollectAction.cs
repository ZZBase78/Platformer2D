namespace Platformer2D.Assets.Quest
{
    internal sealed class QuestCollectAction : IQuest
    {
        private IEventQuestCollect collectController;
        private IQuestAction actionController;
        private bool isComlete;

        public QuestCollectAction(IEventQuestCollect collectController, IQuestAction actionController)
        {
            this.collectController = collectController;
            this.actionController = actionController;

            collectController.actionQuestCollect += QuestCollect;

            isComlete = false;
        }

        public bool IsComblete()
        {
            return isComlete;
        }

        public void QuestCollect()
        {
            collectController.actionQuestCollect -= QuestCollect;
            isComlete = true;
            actionController.QuestAction();
        }
    }
}
