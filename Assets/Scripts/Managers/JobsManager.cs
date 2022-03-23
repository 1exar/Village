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

}

namespace Jobs
{
    [Serializable]
    public class Task
    {
        public string name;
        public int progressMax, progressCurrent;
        public List<BallAi> balls;
    }
}