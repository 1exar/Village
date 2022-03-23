using System;
using System.Collections.Generic;
using System.Linq;
using Items;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldResourceManager : MonoBehaviour
{

    public static WorldResourceManager I;
    public List<GameObject> _trees = new List<GameObject>();
    public List<DropedItem> dropedItems = new List<DropedItem>();

    public int AvaibleWood()
    {
        _trees.Clear();
        _trees = GameObject.FindGameObjectsWithTag("Tree").Where(t => !t.GetComponent<Tree>().ocuped).ToList();

        return _trees.Count;
    }

    public void SpawnDropItems(List<ItemsToDrop> itemsToDrop, Vector3 pos)
    {
        foreach (var drop in itemsToDrop)
        {
            Vector3 spawnPos = new Vector3(pos.x + Random.Range(-1, 1), pos.y + Random.Range(-1, 1), pos.z);
            if (drop.chance == 100)
            {
                DropedItem droped = Instantiate(GameManager.I.dropItemPrefab, spawnPos, new Quaternion())
                    .GetComponent<DropedItem>();
                droped.gameObject.transform.parent = null;
                droped.Init(drop.itemsToDrop, drop.count);
                dropedItems.Add(droped);
            }
            else
            {
                int chance = Random.Range(0, 100);
                if (chance >= drop.chance)
                {
                    DropedItem droped = Instantiate(GameManager.I.dropItemPrefab, spawnPos, new Quaternion())
                        .GetComponent<DropedItem>();
                    droped.gameObject.transform.parent = null;
                    droped.Init(drop.itemsToDrop, drop.count);
                    dropedItems.Add(droped);
                }
                else
                {
                    continue;
                }
            }
        }
    }
    
    private void Awake()
    {
        I = this;
    }

}