using Platformer2D.Assets.LevelScripts;

namespace Platformer2D.Assets.PathFinder
{
    internal sealed class PathFinderGraphBuilder
    {
        private const int BIG_GRAPH_BASE_WEIGHT = 1;
        private const int SMALL_GRAPH_BASE_WEIGHT = 100;

        public LevelData levelData;
        public PathFinderGraphBuilder(LevelData _levelData)
        {
            levelData = _levelData;
        }

        public PathFinderGraph[,] BuildBigGraphs()
        {
            int width = levelData.levelConfig.width;
            int height = levelData.levelConfig.height;

            PathFinderGraph[,] bigGraphs = new PathFinderGraph[width, height];

            //Build
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    bigGraphs[x, y] = new PathFinderGraph(x, y, true, BIG_GRAPH_BASE_WEIGHT);
                }
            }

            //Connect
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    LevelCellConfig cellConfig = levelData.levelConfig.levelCellConfigs[x, y];
                    if (cellConfig.permittedDirectionSet.left) ConnectGraph(bigGraphs[x, y], bigGraphs[x - 1, y]);
                    if (cellConfig.permittedDirectionSet.right) ConnectGraph(bigGraphs[x, y], bigGraphs[x + 1, y]);
                    if (cellConfig.permittedDirectionSet.up) ConnectGraph(bigGraphs[x, y], bigGraphs[x, y + 1]);
                    if (cellConfig.permittedDirectionSet.down) ConnectGraph(bigGraphs[x, y], bigGraphs[x, y - 1]);
                }
            }

            return bigGraphs;
        }

        public PathFinderGraph[,] BuildSmallGraphs()
        {
            int width = levelData.width;
            int height = levelData.height;

            PathFinderGraph[,] smallGraphs = new PathFinderGraph[width, height];

            //Build
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    smallGraphs[x, y] = new PathFinderGraph(x, y, !levelData.levelElements[x, y].isWall, SMALL_GRAPH_BASE_WEIGHT);
                }
            }

            //Connect
            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
                {
                    if (smallGraphs[x, y].canUse)
                    {
                        if (smallGraphs[x - 1, y].canUse) ConnectGraph(smallGraphs[x, y], smallGraphs[x - 1, y]);
                        if (smallGraphs[x + 1, y].canUse) ConnectGraph(smallGraphs[x, y], smallGraphs[x + 1, y]);
                        if (smallGraphs[x, y - 1].canUse) ConnectGraph(smallGraphs[x, y], smallGraphs[x, y - 1]);
                        if (smallGraphs[x, y + 1].canUse) ConnectGraph(smallGraphs[x, y], smallGraphs[x, y + 1]);
                    }
                }
            }

            return smallGraphs;
        }

        private void ConnectGraph(PathFinderGraph graph1, PathFinderGraph graph2)
        {
            if ((graph1.x == graph2.x) && (graph1.y == graph2.y + 1))
            {
                graph1.downGraph = graph2;
                graph2.upGraph = graph1;
            }
            else if ((graph1.x == graph2.x) && (graph1.y == graph2.y - 1))
            {
                graph2.downGraph = graph1;
                graph1.upGraph = graph2;
            }
            else if ((graph1.x == graph2.x + 1) && (graph1.y == graph2.y))
            {
                graph1.leftGraph = graph2;
                graph2.rightGraph = graph1;
            }
            else if ((graph1.x == graph2.x - 1) && (graph1.y == graph2.y))
            {
                graph2.leftGraph = graph1;
                graph1.rightGraph = graph2;
            }
        }
    }
}
