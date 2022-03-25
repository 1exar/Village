using System;
using System.Collections.Generic;
using Items;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Database", fileName = "ItemsDatabase")]
public class ItemDatabase : ScriptableObject
{
    [SerializeField] public WoodItem wood;
    [SerializeField] public BrushWoodItem BrushWood;
    [SerializeField] public Stone stone;

    private List<Item> allItems = new List<Item>();

    public void ClearAllItemsList()
    {
        allItems.Clear();
    }
    
    public List<Item> getAllItemsType()
    {
        if (allItems.Count != 0) return allItems;
        else
        {
            allItems.Add(wood);
            allItems.Add(BrushWood);
            //allItems.Add(stone);

            return allItems;
        }
    }

    [Serializable]
    public class WoodItem : Item
    {
        
    }
    [Serializable]
    public class BrushWoodItem : Item
    {
        
    }

    [Serializable]
    public class Stone : Item
    {
        
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
    }
}