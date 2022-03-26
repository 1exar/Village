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

    private List<BuildingCard> buildingUIPanels = new List<BuildingCard>();
    [SerializeField]
    private GameObject buildingCardPrefab;
    [SerializeField] 
    private Transform parent, mapParent;
    [SerializeField]
    private GameObject currentBuilding;
    
    
    private void Awake()
    {
        I = this;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        foreach (var building in buildingsList)
        {
            BuildingCard card = Instantiate(buildingCardPrefab, parent).GetComponent<BuildingCard>();
            card.icon.sprite = building.icon;
            card.resourcesToBuild = building.resourcesToBuild;
            card.prefab = building.buildingPrefab;
            card.Init();
        }
    }

    public void VisualaizeBuild(GameObject building)
    {
        currentBuilding = Instantiate(building, mapParent);
        currentBuilding.AddComponent<BuildingVisualizer>();
        UIManager.I.applyBuilding.SetActive(true);
    }

    public void ApplyBuilding()
    {
        
    }

    public void RemoveVisualizing()
    {
        
    }
    
}

namespace Buildings
{
    [Serializable]
    public class BuildingCardInfo
    {
        [SerializeField]
        public List<ResourceToBuild> resourcesToBuild = new List<ResourceToBuild>();

        public Sprite icon;

        public GameObject buildingPrefab;
    }

    [Serializable]
    public class ResourceToBuild
    {
        public string id;
        public int count;
    }
}

