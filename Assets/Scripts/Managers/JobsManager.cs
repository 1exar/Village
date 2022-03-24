using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Items;
using Jobs;
using UnityEditor;
using UnityEngine;

public class JobsManager : MonoBehaviour
{

    public static JobsManager I;

    [SerializeField] public List<LumberTreesTask> mineTreeTasks = new List<LumberTreesTask>();
    [SerializeField] public List<CollectDropTask> collectAllDropTasks = new List<CollectDropTask>();

    private void Awake()
    {
        I = this;
    }

}

namespace Jobs
{
    [Serializable]
    public class Task
    {
        public string name;
        public int progressMax, progressCurrent;
        public List<Ball> balls;

        public void CancelTask()
        {
            progressCurrent = progressMax;
            foreach (var ball in balls)
            {
                ball.CancelJob();
            }
        }
    }
}