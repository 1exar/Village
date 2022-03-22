using System;
using System.Collections.Generic;
using System.Linq;
using Jobs;
using UnityEngine;

public class JobsManager : MonoBehaviour
{

    public static JobsManager I;

    [SerializeField]
    public List<Task> tasks = new List<Task>();

    private void Awake()
    {
        I = this;
    }

    public void LumberTrees(int count, int balls)
    {
        MineTrees job = new MineTrees();
        job.progressMax = count;
        List<BallAi> avaibleBalls = GameManeger.I.balls.Where(b => !b.haveTask).ToList();
        List<BallAi> usedBalls = new List<BallAi>();
        for (int i = 0; i < balls; i++)
        {
            avaibleBalls[i].MakeJob(job);
            usedBalls.Add(avaibleBalls[i]);
        }

        job.balls = usedBalls;
        tasks.Add(job);
    }
    
    private void CheckTasks()
    {
        
    }
}

namespace Jobs
{
    [Serializable]
    public class MineTrees : Task
    {
        
    }
    [Serializable]
    public class CollectAllDrops : Task
    {
        
    }
    [Serializable]
    public class Task
    {
        public string name;
        public int progressMax, progressCurrent;
        public List<BallAi> balls;
    }
}