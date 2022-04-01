using Platformer2D.Assets.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platformer2D.Assets.AnimationScripts
{
    internal sealed class AnimationFactory
    {
        public AnimationData Create(string resourcePathString)
        {
            AnimationData animationData = new AnimationData();
            AnimationTrack animationTrack = Resources.Load<AnimationTrack>(resourcePathString);
            if (animationTrack != null)
            {
                animationData.track = animationTrack.list;
            }
            else
            {
                throw new Exception(string.Join(ErrorMessages.ERROR_SEPARATOR, ErrorMessages.ANIMATION_NOT_FOUND, resourcePathString));
            }
            animationData.speed = animationTrack.speed;
            animationData.counter = 0;
            animationData.loop = animationTrack.loop;

            return animationData;
        }
    }
}
