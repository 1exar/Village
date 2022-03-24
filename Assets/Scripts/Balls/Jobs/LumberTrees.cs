using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Jobs
{
    public class LumberTrees
    {
        public LumberTrees(List<GameObject> tree, List<Ball> balls)
        {
            LumberTreesTask job = new LumberTreesTask();
            job.progressMax = tree.Count;
            job.name = "Лесохуй";

            // GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree").Where(t => !t.GetComponent<Tree>().ocuped).ToArray();
            int c = 0;
            for (int i = 0; i < tree.Count; i++)
            {
                Tree t = tree[i].GetComponent<Tree>();
                t.jobWithMe = job;
                job.trees.Add(t);
            }

            foreach (var ball in balls)
            {
                ball.GetComponent<BallLumberJob>().LumberTrees(job);
            }

            job.balls = balls;
            JobsManager.I.mineTreeTasks.Add(job);
            UIManager.I.AddNewTaskToTaskList(job);
        }
    }
    
    [Serializable]
    public class LumberTreesTask : Task
    {
        public List<Tree> trees = new List<Tree>();

        public void ChopOneTree()
        {
            progressCurrent++;
            if (progressCurrent >= progressMax)
            {

                List<DropedItem> itemsToCollect = new List<DropedItem>();

                foreach (var item in WorldResourceManager.I.dropedItems)
                {
                    if (item.Name == GameManager.I.items.wood.Name || item.Name == GameManager.I.items.BrushWood.Name)
                    {
                        itemsToCollect.Add(item);
                    }
                }

                CollectDrop nextJob = new CollectDrop(balls, itemsToCollect);
                JobsManager.I.mineTreeTasks.Remove(this);
            }
        }
    }
}