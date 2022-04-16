using Platformer2D.Assets.LevelScripts;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D.Assets.PathFinder
{
    internal sealed class PathFinderManager
    {

        private LevelData levelData;
        private PathFinderGraph[,] bigGraphs;
        private List<PathFinderGraph> bigGraphsList;
        private PathFinderGraph[,] smallGraphs;
        private List<PathFinderTaskRemover> removeTasks;
        private List<PathFinderTask> tasks;

        public PathFinderManager(LevelData _levelData)
        {
            levelData = _levelData;

            PathFinderGraphBuilder graphBuilder = new PathFinderGraphBuilder(levelData);
            bigGraphs = graphBuilder.BuildBigGraphs();
            smallGraphs = graphBuilder.BuildSmallGraphs();

            removeTasks = new List<PathFinderTaskRemover>();
            tasks = new List<PathFinderTask>();

            bigGraphsList = GetBigGraphList();
        }

        private List<PathFinderGraph> GetBigGraphList()
        {
            List<PathFinderGraph> list = new List<PathFinderGraph>();
            for (int x = 0; x < levelData.levelConfig.width; x++)
            {
                for (int y = 0; y < levelData.levelConfig.height; y++)
                {
                    list.Add(bigGraphs[x, y]);
                }
            }
            return list;
        }


        public void Update()
        {
            CalculateTasks();
            CheckTasksComplete();
            RemoveTasks();
        }

        private void CheckTasksComplete()
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].isComplete)
                {
                    tasks.RemoveAt(i);
                }
            }
        }

        private void CalculateTasks()
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                tasks[i].Update();
                if (tasks[i].isComplete)
                {
                    removeTasks.Add(new PathFinderTaskRemover(bigGraphs, levelData.levelConfig.width, levelData.levelConfig.height, tasks[i]));
                    removeTasks.Add(new PathFinderTaskRemover(smallGraphs, levelData.width, levelData.height, tasks[i]));
                }
            }
        }

        private void RemoveTasks()
        {
            for (int i = 0; i < removeTasks.Count; i++)
            {
                removeTasks[i].Update();
                if (removeTasks[i].isFinished) removeTasks.RemoveAt(i);
            }
        }

        public PathFinderTask GetPathResult(Vector2 startPosition, Vector2 finishPosition, Action<PathFinderResult> actionResult)
        {
            PathFinderTask task = new PathFinderTask(startPosition, finishPosition, levelData, bigGraphsList, bigGraphs, smallGraphs, actionResult);
            tasks.Add(task);
            return task;
        }

        public void StopFinding(PathFinderTask task)
        {
            task.isComplete = true;
        }
    }
}
