using System;
using System.Collections.Generic;
using Items;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Database", fileName = "ItemsDatabase")]
public class ItemDatabase : ScriptableObject
{

    [SerializeField]private List<Item> items;

    [SerializeField]private Item current;

    [SerializeField]private int currentIndex;

    public Item this[int index]
    {
        get
        {
            if (items != null && index >= 0 && index < items.Count)
                return items[index];
            return null;
        }
        set
        {
            if (items == null)
                items = new List<Item>();

            if (index >= 0 && index < items.Count && value != null)
                items[index] = value;
            else Debug.LogError("out of range or value is null");
        }
    }

    public Item this[string id]
    {
        get
        {
            foreach (var item in items)
            {
                if (item.Id == id) return item;
            }

            return null;
        }
    }
    
    public void AddElement()
    {
        if (items == null)
            items = new List<Item>();
        current = new Item();
        items.Add(current);
        currentIndex = items.Count - 1;
    }

    public void RemoveCurrentElement()
    {
        if (currentIndex > 0)
        {
            current = items[--currentIndex];
            items.RemoveAt(++currentIndex);
            currentIndex--;
        }
        else
        {
            items.Clear();
            current = null;
        }
    }
    
    public Item NextItem()
    {
        if (currentIndex < items.Count - 1)
            currentIndex++;
        current = this[currentIndex];
        return current;
    }

    public Item PrevItem()
    {
        if (currentIndex > 0)
            currentIndex--;
        current = this[currentIndex];
        return current;
    }

    public List<Item> allItems
    {
        protected set
        {
            
        }
        get
        {
            return items;
        }
    }

}

namespace Items
{
    [Serializable]
    public class Item
    {
        [SerializeField] private string name;
        public string Name
        { get { return name;} set {}}

        [SerializeField] private Sprite sprite;
        public Sprite Sprite
        { get { return sprite; } set{} }

        [SerializeField] private string id;
        public string Id
        { get { return id;} set{}}
    }

}