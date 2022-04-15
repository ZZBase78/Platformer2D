
using System.Collections.Generic;

namespace Platformer2D.Assets.PathFinder
{
    internal sealed class PathFinderTaskRemover
    {
        private PathFinderGraph[,] graphs;
        private int width;
        private int height;
        private PathFinderTask task;
        private int x;
        private int y;
        public bool isFinished;

        public PathFinderTaskRemover(PathFinderGraph[,] graphs, int width, int height, PathFinderTask task)
        {
            this.graphs = graphs;
            this.width = width;
            this.height = height;
            this.task = task;
            x = 0;
            y = 0;
            isFinished = false;
        }

        public void Update()
        {
            if (isFinished) return;
            graphs[x, y].weight.Remove(task);
            graphs[x, y].pathWeight.Remove(task);
            graphs[x, y].calculated.Remove(task);
            x++;
            if (x >= width)
            {
                y++;
                x = 0;
            }
            if (y >= height)
            {
                isFinished = true;
            }
        }
    }
}
