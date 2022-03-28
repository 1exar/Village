using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Buildings;
using Items;

public class BuildingCard : MonoBehaviour
{

    public Image icon;
    public List<ResourceToBuild> resourcesToBuild = new List<ResourceToBuild>();
    public string name;
    public Text nameText;

    public GameObject resourceToBuildPrefab;
    public Transform parent;

    public GameObject prefab;

    public enum buildingType
    {
        building, fence, road
    }

    public buildingType currentType;
    
    public void Choise()
    {
        switch (currentType)
        {
            case buildingType.building:
                if (CheckAvaibleResources())
                {
                    UIManager.I.CloseUI();
                    BuildingManager.I.VisualaizeBuild(prefab);
                }
                else
                {
                    print("no resources");
                }
                break;
            case buildingType.fence:
                Instantiate(BuildingManager.I.fenceBuilder);
                UIManager.I.FenceBuildMode();
                break;
        }

    }

    private bool CheckAvaibleResources()
    {
        foreach (var res in resourcesToBuild)
        {
            if (ResourceManager.I.GetResourceCount(res.id) >= res.count)
            {
                continue;
            }
            else
            {
                return false;
            }
        }

        return true;
    }
    
    public void Init()
    {
        foreach (var resource in resourcesToBuild)
        {
            ResourceToBuildCard res = Instantiate(resourceToBuildPrefab, parent).GetComponent<ResourceToBuildCard>();
            res.count.text = resource.count + "";
            res.icon.sprite = GameManager.I.items[resource.id].Sprite;
        }
    }
    
}
