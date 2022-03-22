using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Items;
public class Tree : MonoBehaviour, IMined
{

    internal int woodDrop = 10;
    public List<ItemsToDrop> _itemsToDrop;
    [SerializeField]
    private int hp = 30;
    [SerializeField]
    private Slider hpSlider;
    
    private void Start()
    {
        hpSlider.maxValue = hp;
        hpSlider.value = hp;
        _itemsToDrop.Add(new ItemsToDrop(GameManeger.I.items.wood, woodDrop, 100));
    }

    public void OnMouseDown()
    {
        throw new System.NotImplementedException();
    }

    public void Mine()
    {
        throw new System.NotImplementedException();
    }

    public void DropItems()
    {
        throw new System.NotImplementedException();
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

    private void Choop()
    {
        Destroy(gameObject);
    }
}
