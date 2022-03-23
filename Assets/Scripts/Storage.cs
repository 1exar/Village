using UnityEngine;
using System;
using System.Collections.Generic;
using Items;

public class Storage : MonoBehaviour
{

    public Dictionary<Item, int> items = new Dictionary<Item, int>();

    public void AddItemToStorage(Item item, int count)
    {
        if (items.ContainsKey(item))
        {
            items[item] += count;
        }
    }

    public int GetCount(Item type)
    {
        return items[type];
    }
    
    public void GetAllItems()
    {
        foreach (var item in items)
        {
            Debug.Log(item.Key + " " + item.Value);
        }
    }

}
[Serializable]
public class StoredItem : Item
{
    public int count;
}