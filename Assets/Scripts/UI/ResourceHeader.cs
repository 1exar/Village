using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceHeader : MonoBehaviour
{

    [SerializeField] private Image sprite;
    [SerializeField] private Text countText;
    [SerializeField] private int count;

    public void Init(Sprite icon, int count)
    {
        sprite.sprite = icon;
        UpdateView(count);
    }

    public void UpdateView(int count)
    {
        this.count = count;
        countText.text = count + "";
    }

}
