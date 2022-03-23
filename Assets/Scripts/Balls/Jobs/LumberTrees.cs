using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Jobs
{
    public class LumberTrees
    {
        public LumberTrees(List<GameObject> tree, int balls)
        {
            MineTrees job = new MineTrees();
            job.progressMax = tree.Count;
            job.name = "Лесохуй";
            List<BallAi> avaibleBalls = GameManager.I.balls.Where(b => !b.haveTask).ToList();
            List<BallAi> usedBalls = new List<BallAi>();

            GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree").Where(t => !t.GetComponent<Tree>().ocuped).ToArray();
            int c = 0;
            for (int i = 0; i < tree.Count; i++)
            {
                Tree t = trees[i].GetComponent<Tree>();
                t.jobWithMe = job;
                job.trees.Add(t);
            }


            for (int i = 0; i < balls; i++)
            {
                avaibleBalls[i].GetComponent<BallLumberJob>().LumberTrees(job);
                usedBalls.Add(avaibleBalls[i]);
            }

            job.balls = usedBalls;
            JobsManager.I.mineTreeTasks.Add(job);
        }
    }
    
    [Serializable]
    public class MineTrees : Task
    {
        public List<Tree> trees = new List<Tree>();

        public void ChopOneTree(int woodDrop)
        {
            progressCurrent += woodDrop;
            if (progressCurrent >= progressMax)
            {
                if (balls.Count() != 0)
                {
                    foreach (var ball in balls.ToList())
                    {
                        ball.EndJob();
                        balls.Remove(ball);
                    }
                }
            }
        }
    }
}