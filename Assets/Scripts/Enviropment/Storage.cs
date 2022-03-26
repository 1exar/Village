using UnityEngine;
using System;
using System.Collections.Generic;
using Items;

public class Storage : MonoBehaviour
{

   private Dictionary<string, Resource> itemsInStorage = new Dictionary<string, Resource>();

   private void Start()
   {
      Init();
   }

   private void Init()
   {
      foreach (var item in GameManager.I.items.allItems)
      {
         Resource newItem = new Resource();
         newItem.type = item;
         itemsInStorage.Add(item.Name, newItem);
      }
   }

   public List<Resource> GetAllItemsInStorage()
   {
      List<Resource> result = new List<Resource>();
      foreach (var item in itemsInStorage)
      {
         result.Add(item.Value);
      }

      return result;
   }
   
   public void AddItemToStorage(Item type, int count)
   {
      if (itemsInStorage.ContainsKey(type.Name))
      {
         itemsInStorage[type.Name].AddResource(count);
      }
      else
      {
         Debug.LogError("Item no exist in ItemDataBase");
      }
   }
   
}