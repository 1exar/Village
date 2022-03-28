using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;
using Buildings;
using UnityEngine.AI;

public class BuildingManager : MonoBehaviour
{

    public static BuildingManager I;
    public Storage mainStorage;
    [SerializeField]
    public List<Building> buildings = new List<Building>();
    [SerializeField]
    private List<BuildingCardInfo> buildingsList = new List<BuildingCardInfo>();
    [SerializeField] 
    private List<BuildingCardInfo> enviropmentList = new List<BuildingCardInfo>();
    [SerializeField]
    private GameObject buildingCardPrefab;
    [SerializeField] 
    private Transform parent, mapParent, envParent;
    [SerializeField]
    private GameObject currentBuilding;

    public GameObject fenceBuilder;
    
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
            card.currentType = building.type;
            card.Init();
        }

        foreach (var enviropment in enviropmentList)
        {
            BuildingCard card = Instantiate(buildingCardPrefab, envParent).GetComponent<BuildingCard>();
            card.icon.sprite = enviropment.icon;
            card.resourcesToBuild = enviropment.resourcesToBuild;
            card.prefab = enviropment.buildingPrefab;
            card.currentType = enviropment.type;
            card.Init();
        }
    }

    public void VisualaizeBuild(GameObject building)
    {
        currentBuilding = Instantiate(building, mapParent);
        currentBuilding.AddComponent<BuildingVisualizer>();
        UIManager.I.applyBuilding.SetActive(true);
        UIManager.I.cancelBuildingButton.SetActive(true);
    }

    public void SetupBuilding()
    {
        Destroy(currentBuilding.GetComponent<BuildingVisualizer>());
        currentBuilding.GetComponent<BuildingDevelopment>().StartDevelopment();
        UIManager.I.ShowUI();
    }

    public void RemoveVisualizing()
    {
        Destroy(currentBuilding);
        UIManager.I.ShowUI();
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

        public BuildingCard.buildingType type;
    }

    [Serializable]
    public class ResourceToBuild
    {
        public string id;
        public int count;
    }
}

