using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D.Assets.PathFinder
{
    internal sealed class PathFinderResult
    {
        public List<Vector2> resultPath;
        public bool isFound;

        public PathFinderResult()
        {
            resultPath = new List<Vector2>();
            isFound = false;
        }
    }
}
