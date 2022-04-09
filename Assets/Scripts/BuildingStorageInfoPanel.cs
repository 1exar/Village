using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;

public class BuildingStorageInfoPanel : MonoBehaviour
{

    public static BuildingStorageInfoPanel I;

    private List<GameObject> currentItems;
    [SerializeField]
    private GameObject itemPrefab;
    [SerializeField]
    private Transform parent;
    
    public void Init(List<Resource> items, string name)
    {
        gameObject.SetActive(true);
    }
    
}
