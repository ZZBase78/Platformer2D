﻿using UnityEngine;

namespace Platformer2D.Assets.LevelScripts
{
    [CreateAssetMenu(fileName = "LevelConfigSettings", menuName = "ScriptableObjects/Level/LevelConfigSettings", order = 1)]
    internal sealed class LevelConfigSettings : ScriptableObject
    {
        public int width;
        public int height;
        public int cellWidth;
        public int cellHeight;
        public int wallWidth;
        public int wallHeight;
    }
}
