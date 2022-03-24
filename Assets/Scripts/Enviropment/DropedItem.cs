using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using Jobs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropedItem : MonoBehaviour
{
    [SerializeField]
    private Item currentItem;
    public Item Item
    {
        get
        {
            return currentItem;
        }
        protected set{}
    }
    public string Name
    {
        get
        {
            return currentItem.Name;
        }
        protected set{}
    }
    private int count;
    public int Count
    {
        get
        {
            return count;
        }

        protected set{}
    }
    [SerializeField]
    private SpriteRenderer sp;
    public TMP_Text countText;

    public bool occuped = false, moveToBall = false, used = false;
    private Vector3 offSet;
    public Transform ballPos;

    public CollectDropTask jobWithMe;
    
    public void Init(Item item, int count)
    {
        currentItem = item;
        sp.sprite = item.Sprite;
        this.count = count;
        countText.text = item.Name + "\n" + count;
    }

    public void Update()
    {
        if(moveToBall)
            transform.position = ballPos.position - offSet;
    }

    public void MoveToStorage()
    {
        moveToBall = false;
        WorldResourceManager.I.dropedItems.Remove(this);
        jobWithMe.OnItemMovedToStorage();
        Destroy(gameObject);
    }
    
}
