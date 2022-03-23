using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldResourceManager : MonoBehaviour
{

    public static WorldResourceManager I;
    public List<GameObject> _trees = new List<GameObject>();
    public List<DropedItem> dropedItems = new List<DropedItem>();

    public int AvaibleWood()
    {
        int i = 0;
        _trees.Clear();
        _trees = GameObject.FindGameObjectsWithTag("Tree").Where(t => !t.GetComponent<Tree>().ocuped).ToList();
        foreach (var tree in _trees)
        {
            i += tree.GetComponent<Tree>().woodDrop;
        }

        return i;
    }   

    private void Awake()
    {
        I = this;
    }

}