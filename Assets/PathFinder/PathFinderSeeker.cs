using Platformer2D.Assets.Extention;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D.Assets.PathFinder
{
    internal sealed class PathFinderSeeker
    {
        private PathFinderTask task;
        private List<PathFinderGraph> graphToCalculate;
        public bool isComplete;
        public bool isFound;
        private float minWeight;

        private List<PathFinderGraph> graphList;
        private PathFinderGraph startGraph;
        private PathFinderGraph finishGraph;

        public List<PathFinderGraph> resultPath;

        public PathFinderSeeker(PathFinderTask task, List<PathFinderGraph> graphList, PathFinderGraph startGraph, PathFinderGraph finishGraph)
        {
            this.task = task;
            this.graphList = graphList;
            this.startGraph = startGraph;
            this.finishGraph = finishGraph;
            graphToCalculate = new List<PathFinderGraph>();
            graphToCalculate.Add(finishGraph);
            isComplete = false;
            isFound = false;
            minWeight = 0;
            resultPath = new List<PathFinderGraph>();
        }

        public void Update()
        {
            if (isComplete) return;

            if (graphToCalculate.Count == 0)
            {
                isComplete = true;
                CreatePath();
                return;
            }

            CalculateGraph(graphToCalculate[0]);
            graphToCalculate.RemoveAt(0);
        }

        private void CreatePath()
        {
            resultPath.Clear();
            resultPath.Add(startGraph);
            PathFinderGraph current = startGraph;
            while (current != finishGraph)
            {
                PathFinderGraph nextGraph = GetNextPathGraph(current);
                resultPath.Add(nextGraph);
                current = nextGraph;
            }
        }

        private PathFinderGraph GetNextPathGraph(PathFinderGraph graph)
        {
            List<float> nearVelues = new List<float>();

            float valueUp = GetPathWeight(graph.upGraph);
            if (valueUp != 0) nearVelues.Add(valueUp);
            
            float valueDown = GetPathWeight(graph.downGraph);
            if (valueDown != 0) nearVelues.Add(valueDown);
            
            float valueLeft = GetPathWeight(graph.leftGraph);
            if (valueLeft != 0) nearVelues.Add(valueLeft);
            
            float valueRight = GetPathWeight(graph.rightGraph);
            if (valueRight != 0) nearVelues.Add(valueRight);
            
            float minNearValue = ListMinValue(nearVelues);

            List<PathFinderGraph> nextGraphs = new List<PathFinderGraph>();
            if (Mathf.Approximately(minNearValue, valueUp)) nextGraphs.Add(graph.upGraph);
            if (Mathf.Approximately(minNearValue, valueDown)) nextGraphs.Add(graph.downGraph);
            if (Mathf.Approximately(minNearValue, valueLeft)) nextGraphs.Add(graph.leftGraph);
            if (Mathf.Approximately(minNearValue, valueRight)) nextGraphs.Add(graph.rightGraph);

            return nextGraphs.GetRandom();
        }

        private void CalculateGraph(PathFinderGraph graph)
        {
            List<float> nearVelues = new List<float>();
            float value = 0;
            value = GetPathWeight(graph.upGraph);
            if (value != 0) nearVelues.Add(value);
            value = GetPathWeight(graph.downGraph);
            if (value != 0) nearVelues.Add(value);
            value = GetPathWeight(graph.leftGraph);
            if (value != 0) nearVelues.Add(value);
            value = GetPathWeight(graph.rightGraph);
            if (value != 0) nearVelues.Add(value);

            float minNearValue = ListMinValue(nearVelues);

            float currentWeight = 0;

            if (graph.weight.ContainsKey(task))
            {
                currentWeight = graph.weight[task];
            }
            else
            {
                currentWeight = graph.baseWeight;
            }
            

            float calculatedPathWeight = currentWeight + minNearValue;

            if (isFound && calculatedPathWeight > minWeight) return;

            bool needNearUpdate = true;

            if (graph.calculated.Contains(task))
            {
                float currentPathWeight = GetPathWeight(graph);
                if (calculatedPathWeight < currentPathWeight)
                {
                    graph.pathWeight[task] = calculatedPathWeight;
                }
                else
                {
                    needNearUpdate = false;
                }
            }
            else
            {
                graph.calculated.Add(task);
                graph.pathWeight.Add(task, calculatedPathWeight);
            }

            if (needNearUpdate && graph != startGraph) AddNearToCalculate(graph);

            if (graph == startGraph)
            {
                isFound = true;
                minWeight = calculatedPathWeight;
            }
        }

        private void AddNearToCalculate(PathFinderGraph graph)
        {
            AddToCalculate(graph.upGraph);
            AddToCalculate(graph.downGraph);
            AddToCalculate(graph.leftGraph);
            AddToCalculate(graph.rightGraph);
        }

        private void AddToCalculate(PathFinderGraph graph)
        {
            if (graph == null) return;
            if (!graph.canUse) return;
            if (graphToCalculate.Contains(graph)) return;
            if (!graphList.Contains(graph)) return;
            graphToCalculate.Add(graph);
        }

        private float ListMinValue(List<float> list)
        {
            if (list.Count == 0) return 0;

            float min = list[0];

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] < min) min = list[i];
            }
            return min;
        }

        private float GetPathWeight(PathFinderGraph graph)
        {
            if (graph == null) return 0;
            if (!graph.canUse) return 0;
            if (!graph.calculated.Contains(task)) return 0;
            return graph.pathWeight[task];
        }
    }
}
