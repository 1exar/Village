using System;
using System.Collections.Generic;
using System.Linq;
using Items;
using Jobs;
using UnityEngine;

public class JobsManager : MonoBehaviour
{

    public static JobsManager I;

    [SerializeField]
    public List<MineTrees> mineTreeTasks = new List<MineTrees>();

    private void Awake()
    {
        I = this;
    }

    public void LumberTrees(int count, int balls)
    {
        MineTrees job = new MineTrees();
        job.progressMax = count;
        List<BallAi> avaibleBalls = GameManager.I.balls.Where(b => !b.haveTask).ToList();
        List<BallAi> usedBalls = new List<BallAi>();

        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree").Where(t => !t.GetComponent<Tree>().ocuped).ToArray();
        int c = 0;
        for (int i = 0; c < count; i++)
        {
            Tree t = trees[i].GetComponent<Tree>();
            t.jobWithMe = job;
            c += t.woodDrop;
            job.trees.Add(t);
        }

        for (int i = 0; i < balls; i++)
        {
            avaibleBalls[i].LumberTrees(job);
            usedBalls.Add(avaibleBalls[i]);
        }

        job.balls = usedBalls;
        mineTreeTasks.Add(job);
    }

    public void CollectItems(int count, int balls, Item[] type = null)
    {
        
    }

}

namespace Jobs
{
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
    [Serializable]
    public class CollectDrops : Task
    {

        public List<DropedItem> itemsToCollect = new List<DropedItem>();

    }
    [Serializable]
    public class Task
    {
        public string name;
        public int progressMax, progressCurrent;
        public List<BallAi> balls;
    }
}