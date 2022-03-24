using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{

    public static GameManager I;
    public ItemDatabase items;
    public List<Ball> balls;

    public GameObject dropItemPrefab;
    
    public List<Ball> avaibleBalls(int count)
    {
        List<Ball> freeBals = balls.Where(b => !b.haveTask).ToList();
        return freeBals.GetRange(0, count);
    }

    private void Awake()
    {
        I = this;
    }
    
    
    
}