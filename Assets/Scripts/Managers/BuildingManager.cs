using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;
using Buildings;

public class BuildingManager : MonoBehaviour
{

    public static BuildingManager I;

    public Storage mainStorage;
    
    [SerializeField]
    public List<Building> buildings = new List<Building>();

    [SerializeField]
    public List<BuildingCardInfo> buildingsList = new List<BuildingCardInfo>();

    private void Awake()
    {
        I = this;
    }

    private void Init()
    {
        BuildingCardInfo info = new BuildingCardInfo();
        Resource wood = new Resource();
        wood.type = GameManager.I.items[0];

    }
    
}

namespace Buildings
{
    [Serializable]
    public class BuildingCardInfo
    {
        [SerializeField]
        public List<ResourceToBuild> resourcesToBuild = new List<ResourceToBuild>();
    }

    [Serializable]
    public class ResourceToBuild
    {
        public Item type;
        public int count;
    }
}

