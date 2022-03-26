using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingCard : MonoBehaviour
{

    public Image icon;
    public List<ResourceToBuildCard> resourcesToBuild = new List<ResourceToBuildCard>();
    public string name;
    public Text nameText;

    public GameObject resourceToBuildPrefab;
    public Transform parent;
    
    public void Choise()
    {
        
    }
    
}
