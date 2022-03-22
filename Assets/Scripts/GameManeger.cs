using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{

    public static GameManeger I;
    public ItemDatabase items;
    public List<BallAi> balls;

    private void Awake()
    {
        I = this;
    }
}