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
            AnimationTrack animationTrack = Resources.Load<AnimationTrack>(resourcePathString);

            if (animationTrack == null)
            {
                throw new Exception(string.Join(ErrorMessages.ERROR_SEPARATOR, ErrorMessages.ANIMATION_NOT_FOUND, resourcePathString));
            }

            return Create(animationTrack);
        }

        public AnimationData Create(AnimationTrack animationTrack)
        {
            AnimationData animationData = new AnimationData();
            animationData.track = animationTrack.list;
            animationData.speed = animationTrack.speed;
            animationData.counter = 0;
            animationData.loop = animationTrack.loop;

            return animationData;
        }
    }
}
