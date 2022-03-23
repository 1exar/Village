using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Items;
using Jobs;
using DG.Tweening;
using Random = UnityEngine.Random;

public class Tree : MonoBehaviour, IMined
{

    internal int woodDrop = 60;
    public List<ItemsToDrop> _itemsToDrop;
    [SerializeField]
    private int hp = 30;
    [SerializeField]
    private Slider hpSlider;
    public bool ocuped;
    public MineTrees jobWithMe;
    public GameObject nameText;
    private void Start()
    {
        hpSlider.maxValue = hp;
        hpSlider.value = hp;
        _itemsToDrop.Add(new ItemsToDrop(GameManager.I.items.wood, woodDrop, 100));
        DropItems();
    }

    public void OnMouseOver()
    {
        nameText.SetActive(true);
    }

    public void OnMouseExit()
    {
        nameText.SetActive(false);
    }

    public void DropItems()
    {
        foreach (var drop in _itemsToDrop)
        {
            if (drop.chance == 100)
            {
                DropedItem droped = Instantiate(GameManager.I.dropItemPrefab, transform.position, new Quaternion())
                    .GetComponent<DropedItem>();
                droped.gameObject.transform.parent = null;
                droped.Init(drop.itemsToDrop, drop.count);
                WorldResourceManager.I.dropedItems.Add(droped);
            }
            else
            {
                int chance = Random.Range(0, 100);
                if (chance >= drop.chance)
                {
                    DropedItem droped = Instantiate(GameManager.I.dropItemPrefab, transform.position, new Quaternion())
                        .GetComponent<DropedItem>();
                    droped.gameObject.transform.parent = null;
                    droped.Init(drop.itemsToDrop, drop.count);
                    WorldResourceManager.I.dropedItems.Add(droped);
                }
                else
                {
                    continue;
                }
            }
        }
    }

    public void TakeHurt(int dmg)
    {
        if(!hpSlider.gameObject.activeSelf) hpSlider.gameObject.SetActive(true);
        hp -= dmg;
        hpSlider.value = hp;
        if (hp <= 0)
        {
            Choop();
        }
    }

    public void Choop()
    {
        jobWithMe.trees.Remove(this);
        jobWithMe.ChopOneTree(woodDrop);
        StartCoroutine(FallTree());
    }

    private IEnumerator FallTree()
    {
        transform.DOLocalRotate(new Vector3(90, 90), 1, RotateMode.FastBeyond360);
        yield return new WaitForSeconds(1);
        DropItems();
        Destroy(gameObject);
    }
}
