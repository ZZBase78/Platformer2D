
using Platformer2D.Assets.LevelScripts;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D.Assets.PathFinder
{
    internal sealed class PathFinderTask
    {
        private const float RANDOM_WEIGHT = 100f;
        public bool isComplete;

        private Vector2 startPosition;
        private Vector2 finishPosition;
        private List<PathFinderGraph> bigGraphsList;
        private PathFinderGraph[,] bigGraphs;
        private PathFinderGraph[,] smallGraphs;
        private Action<PathFinderResult> actionResult;
        private bool smallFind;
        private LevelData levelData;
        private PathFinderSeeker seeker;
        private LevelCoordinator levelCoordinator;

        public PathFinderTask(Vector2 startPosition, Vector2 finishPosition, LevelData levelData, List<PathFinderGraph> bigGraphsList, PathFinderGraph[,] bigGraphs, PathFinderGraph[,] smallGraphs, Action<PathFinderResult> actionResult)
        {
            this.startPosition = startPosition;
            this.finishPosition = finishPosition;
            this.bigGraphsList = bigGraphsList;
            this.bigGraphs = bigGraphs;
            this.smallGraphs = smallGraphs;
            this.actionResult = actionResult;
            this.levelData = levelData;
            this.levelCoordinator = new LevelCoordinator(levelData);

            smallFind = false;

            Vector2Int startCell = levelCoordinator.GetCellFromWorld(startPosition);
            Vector2Int finishCell = levelCoordinator.GetCellFromWorld(finishPosition);

            seeker = new PathFinderSeeker(this, bigGraphsList, bigGraphs[startCell.x, startCell.y], bigGraphs[finishCell.x, finishCell.y]);

            isComplete = false;
        }

        public void Update()
        {
            if (isComplete) return;
            seeker.Update();

            CheckComplete();
        }

        private void CheckComplete()
        {
            if (!seeker.isComplete) return;

            if (!smallFind)
            {
                //start small find
                smallFind = true;
                if (!seeker.isFound)
                {
                    isComplete = true;
                    PathFinderResult result = new PathFinderResult(this, false);
                    actionResult?.Invoke(result);
                    return;
                }
                StartSmallSeeker();
            }
            else
            {
                isComplete = true;
                PathFinderResult result = CreateResult();
                actionResult?.Invoke(result);
            }
        }

        private void StartSmallSeeker()
        {
            Vector2Int startLevel = levelCoordinator.GetLevelFromWorld(startPosition);
            Vector2Int finishLevel = levelCoordinator.GetLevelFromWorld(finishPosition);

            PathFinderGraph startGraph = null;
            PathFinderGraph finishGraph = null;
            int startMinDistance = 0; //INT because sqr Magnitude from Vector2Int
            int finishMinDistance = 0; //INT because sqr Magnitude from Vector2Int

            List<PathFinderGraph> smallGraphsList = new List<PathFinderGraph>();
            for(int bigIndex = 0; bigIndex < seeker.resultPath.Count; bigIndex++)
            {
                
                int minX;
                int maxX;
                int minY;
                int maxY;

                if (bigIndex == seeker.resultPath.Count - 1)
                {
                    PathFinderGraph firstGraph = seeker.resultPath[bigIndex];
                    minX = levelCoordinator.GetLevelLeftXFromCellX(firstGraph.x);
                    maxX = levelCoordinator.GetLevelRightXFromCellX(firstGraph.x);
                    minY = levelCoordinator.GetLevelDownYFromCellY(firstGraph.y);
                    maxY = levelCoordinator.GetLevelUpYFromCellY(firstGraph.y);
                }
                else
                {
                    PathFinderGraph firstGraph = seeker.resultPath[bigIndex];
                    PathFinderGraph secondGraph = seeker.resultPath[bigIndex + 1];
                    minX = levelCoordinator.GetLevelLeftXFromCellX(Mathf.Min(firstGraph.x, secondGraph.x));
                    maxX = levelCoordinator.GetLevelRightXFromCellX(Mathf.Max(firstGraph.x, secondGraph.x));
                    minY = levelCoordinator.GetLevelDownYFromCellY(Mathf.Min(firstGraph.y, secondGraph.y));
                    maxY = levelCoordinator.GetLevelUpYFromCellY(Mathf.Max(firstGraph.y, secondGraph.y));
                }

                for (int x = minX; x <= maxX; x++)
                {
                    for (int y = minY; y <= maxY; y++)
                    {
                        PathFinderGraph smallGraph = smallGraphs[x, y];
                        if (!smallGraph.weight.ContainsKey(this))
                        {
                            smallGraph.weight.Add(this, UnityEngine.Random.Range(1f, RANDOM_WEIGHT));
                        }
                        smallGraphsList.Add(smallGraph);

                        Vector2Int currentVector2Int = new Vector2Int(smallGraph.x, smallGraph.y);
                        int currentStartDistance = (currentVector2Int - startLevel).sqrMagnitude;
                        int currentFinishDistance = (currentVector2Int - finishLevel).sqrMagnitude;

                        if (startGraph == null)
                        {
                            startGraph = smallGraph;
                            startMinDistance = currentStartDistance;
                        }
                        else if (currentStartDistance < startMinDistance)
                        {
                            startGraph = smallGraph;
                            startMinDistance = currentStartDistance;
                        }

                        if (finishGraph == null)
                        {
                            finishGraph = smallGraph;
                            finishMinDistance = currentFinishDistance;
                        }
                        else if (currentFinishDistance < finishMinDistance)
                        {
                            finishGraph = smallGraph;
                            finishMinDistance = currentFinishDistance;
                        }
                    }
                }
            }

            seeker = new PathFinderSeeker(this, smallGraphsList, startGraph, finishGraph);
        }

        private PathFinderResult CreateResult()
        {
            PathFinderResult result = new PathFinderResult(this, seeker.isFound);
            foreach(PathFinderGraph graph in seeker.resultPath)
            {
                result.resultPath.Add(new Vector2(graph.x, graph.y) + levelCoordinator.worldOffSet);
            }
            return result;
        }
    }
}
