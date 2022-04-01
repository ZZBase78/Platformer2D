using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D.Assets.AnimationScripts
{
    internal sealed class AnimationData
    {
        public List<Sprite> track;
        public float speed;
        public float counter;
        public bool loop;
    }
}
