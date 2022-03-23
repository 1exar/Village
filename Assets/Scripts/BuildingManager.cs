using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{

    public static BuildingManager I;
    
    [SerializeField]
    public List<Building> buildings = new List<Building>();

    private void Awake()
    {
        I = this;
    }
}