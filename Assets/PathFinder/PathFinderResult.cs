using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D.Assets.PathFinder
{
    internal sealed class PathFinderResult
    {
        public PathFinderTask task;
        public List<Vector2> resultPath;
        public bool isFound;

        public PathFinderResult(PathFinderTask task, bool isFound)
        {
            this.task = task;
            this.isFound = isFound;
            resultPath = new List<Vector2>();
        }
    }
}
