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
    private int count;
    [SerializeField]
    private SpriteRenderer sp;
    public TMP_Text countText;
    
    public void Init(Item item, int count)
    {
        currentItem = item;
        sp.sprite = item.Sprite;
        this.count = count;
        countText.text = item.Name + "\n" + count;
    }
}
