using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D.Assets.AnimationScripts
{
    [CreateAssetMenu(fileName = "AnimationTrack", menuName = "ScriptableObjects/Animation/AnimationTrack", order = 1)]
    internal sealed class AnimationTrack : ScriptableObject
    {
        public float speed;
        public bool loop;
        public List<Sprite> list;
    }
}
