using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{

    public static GameManager I;
    public ItemDatabase items;
    public List<BallAi> balls;

    public GameObject dropItemPrefab;

    public NavMeshSurface2d nav;
    
    private void Awake()
    {
        I = this;
    }
}