using System;
using System.Collections.Generic;
using System.Linq;
using Items;
using Jobs;
using UnityEditor;
using UnityEngine;

public class JobsManager : MonoBehaviour
{

    public static JobsManager I;

    [SerializeField] public List<MineTrees> mineTreeTasks = new List<MineTrees>();
    [SerializeField] public List<CollectDrops> collectAllDropTasks = new List<CollectDrops>();

    private void Awake()
    {
        I = this;
    }

    public void LumberTrees(int count, int balls)
    {
        MineTrees job = new MineTrees();
        job.progressMax = count;
        job.name = "Лесохуй";
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

    public void CollectItems(int balls, List<DropedItem> type)
    {
        CollectDrops job = new CollectDrops();
        job.progressMax = type.Count;
        job.name = "Искать мусор";
        List<BallAi> avaibleBalls = GameManager.I.balls.Where(b => !b.haveTask).ToList();
        List<BallAi> usedBalls = new List<BallAi>();

        if (type.Count == 0)
        {
            job.itemsToCollect.Clear();
            job.itemsToCollect = WorldResourceManager.I.dropedItems;
        }
        else
        {
            job.itemsToCollect.Clear();
            foreach (var dropedItem in WorldResourceManager.I.dropedItems)
            {
                foreach (var item in type)
                {
                    if(dropedItem.Item.Name == item.Name) job.itemsToCollect.Add(dropedItem);
                }
            }
        }

        
        for (int i = 0; i < balls; i++)
        {
            avaibleBalls[i].CollectAllItems(job);
            usedBalls.Add(avaibleBalls[i]);
        }

        job.balls = usedBalls;
        collectAllDropTasks.Add(job);

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