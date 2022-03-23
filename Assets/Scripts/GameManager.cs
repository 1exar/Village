using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager I;
    public ItemDatabase items;
    public List<BallAi> balls;

    public GameObject dropItemPrefab;
    
    private void Awake()
    {
        I = this;
    }
}