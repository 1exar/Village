using System;
using Items;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Database", fileName = "ItemsDatabase")]
public class ItemDatabase : ScriptableObject
{
    [SerializeField] public WoodItem wood;
    [SerializeField] public BrushWoodItem BrushWood;
    [Serializable]
    public class WoodItem : Item
    {
        
    }
    [Serializable]
    public class BrushWoodItem : Item
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
        { get { return name;} protected set {}}

        [SerializeField] private Sprite sprite;
        public Sprite Sprite
        { get { return sprite; } protected set{} }
    }
}