using System.Collections.Generic;

namespace Platformer2D.Assets.PathFinder
{
    internal sealed class PathFinderGraph
    {
        public int x;
        public int y;
        public bool canUse;
        public float baseWeight;
        public Dictionary<PathFinderTask,float> weight;
        public List<PathFinderTask> calculated;

        public PathFinderGraph leftGraph;
        public PathFinderGraph rightGraph;
        public PathFinderGraph upGraph;
        public PathFinderGraph downGraph;

        public Dictionary<PathFinderTask, float> pathWeight;

        public PathFinderGraph(int _x, int _y, bool _canUse, float _baseWeight)
        {
            x = _x;
            y = _y;
            canUse = _canUse;
            baseWeight = _baseWeight;

            pathWeight = new Dictionary<PathFinderTask, float>();
            weight = new Dictionary<PathFinderTask, float>();
            calculated = new List<PathFinderTask>();
        }
    }
}
