using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Buildings;

public class BuildingCard : MonoBehaviour
{

    public Image icon;
    public List<ResourceToBuild> resourcesToBuild = new List<ResourceToBuild>();
    public string name;
    public Text nameText;

    public GameObject resourceToBuildPrefab;
    public Transform parent;

    public GameObject prefab;
    
    public void Choise()
    {
        UIManager.I.CloseUI();
        BuildingManager.I.VisualaizeBuild(prefab);
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
