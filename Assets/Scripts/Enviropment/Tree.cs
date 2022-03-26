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
    [SerializeField]
    private int woodDrop = 60;
    private List<ItemsToDrop> _itemsToDrop = new List<ItemsToDrop>();
    [SerializeField]
    private int hp = 30;
    [SerializeField]
    private Slider hpSlider;
    public bool ocuped;
    public LumberTreesTask jobWithMe;
    [SerializeField]
    private GameObject nameText;

    public Vector3 frontPosOffset;
    
    private void Start()
    {
        hpSlider.maxValue = hp;
        hpSlider.value = hp;
        _itemsToDrop.Add(new ItemsToDrop(GameManager.I.items["wood"], woodDrop, 100));
        _itemsToDrop.Add(new ItemsToDrop(GameManager.I.items["brushWood"], 100, 100));
    }

    public void OnMouseDown()
    {
        nameText.SetActive(true);
    }

    public void OnMouseExit()
    {
        nameText.SetActive(false);
    }

    public void DropItems()
    {
        WorldResourceManager.I.SpawnDropItems(_itemsToDrop, transform.position);
    }

    public bool TakeHurt(int dmg)
    {
        if(!hpSlider.gameObject.activeSelf) hpSlider.gameObject.SetActive(true);
        hp -= dmg;
        hpSlider.value = hp;
        if (hp <= 0)
        {
            Choop();
            return false;
        }

        return true;
    }

    public void Choop()
    {
        jobWithMe.trees.Remove(this);
        StartCoroutine(FallTree());
    }

    private IEnumerator FallTree()
    {
        transform.DOLocalRotate(new Vector3(90, 90), 1, RotateMode.FastBeyond360);
        DropItems();
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) DropItems();
    }
}
