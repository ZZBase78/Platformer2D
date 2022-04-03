using Platformer2D.Assets.AnimationScripts;
using System.Collections.Generic;

namespace Platformer2D.Assets.PlayerScripts
{
    internal sealed class PlayerStateAnimation
    {
        private AnimationData defaultAnimation;
        private Dictionary<PlayerState, AnimationData> dictionary;
        public PlayerStateAnimation()
        {
            AnimationFactory animationFactory = new AnimationFactory();

            defaultAnimation = animationFactory.Create(ResourcesAnimationPathes.PLAYER_IDLE);
            
            AnimationData moveAnimation = animationFactory.Create(ResourcesAnimationPathes.PLAYER_MOVE);

            dictionary = new Dictionary<PlayerState, AnimationData>();
            dictionary.Add(PlayerState.Stand, defaultAnimation);
            dictionary.Add(PlayerState.MoveLeft, moveAnimation);
            dictionary.Add(PlayerState.MoveRight, moveAnimation);
        }

        public AnimationData GetAnimationData(PlayerState playerState)
        {
            if (dictionary.TryGetValue(playerState, out AnimationData animationData))
            {
                return animationData;
            }
            else
            {
                return defaultAnimation;
            }
        }
    }
}
