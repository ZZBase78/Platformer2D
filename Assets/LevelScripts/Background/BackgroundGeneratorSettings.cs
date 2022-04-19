using UnityEngine;

namespace Platformer2D.Assets.LevelScripts.Background
{
    [CreateAssetMenu(fileName = "BackgroundGeneratorSettings", menuName = "ScriptableObjects/Level/BackgroundGeneratorSettings", order = 1)]
    internal sealed class BackgroundGeneratorSettings : ScriptableObject
    {
        [Range(0f, 100f)] public float fillPercent;
        [Range(0f, 8f)] public int wallNeighbour;
        [Range(0f, 8f)] public int emptyNeighbour;
        public int smoothCount;
    }
}
