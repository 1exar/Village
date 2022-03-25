using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Items;

public class ResourceManager : MonoBehaviour
{

    private Dictionary<string, Resource> myResource = new Dictionary<string, Resource>();
    public static ResourceManager I;

    private List<ResourceHeader> tabResource = new List<ResourceHeader>();

    public GameObject resourceHeaderPrefab;
    public Transform spawnParent;
    
    private void Awake()
    {
        I = this;
        Init();
    }

    private void Init()
    {
        foreach (var item in GameManager.I.items.getAllItemsType())
        {
            Resource newItem = new Resource();
            newItem.type = item;
            myResource.Add(item.Name, newItem);
            ResourceHeader newTabView = Instantiate(resourceHeaderPrefab, spawnParent).GetComponent<ResourceHeader>();
            newTabView.Init(item.Sprite, 0);
            tabResource.Add(newTabView);
        }
    }

    private void ClearResourceCount()
    {
        foreach (var resource in myResource)
        {
            resource.Value.TakeResource(resource.Value.GetCount());
        }
    }
    
    public void CheckMyResources()
    {
        ClearResourceCount();
        foreach (var building in BuildingManager.I.buildings.Where(b => b.haveStorage))
        {
            foreach (var resource in building.storage.GetAllItemsInStorage())
            {
                myResource[resource.type.Name].AddResource(resource.GetCount());
            }
        }

        int id = 0;
        foreach (var resource in myResource)
        {
            tabResource[id].UpdateView(resource.Value.GetCount());
            id++;
        }
    }
    
}

namespace Items
{
    public class Resource
    {

        public Item type;
        private int count;

        public void AddResource(int count)
        {
            this.count += count;
        }

        public int GetCount()
        {
            return count;
        }

        public bool TakeResource(int count)
        {
            if (count >= GetCount())
            {
                this.count -= count;
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }
}
