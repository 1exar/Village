using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FencePart : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer sp;
    [SerializeField]
    private Material red, def;

    private int startLayerId;

    private void Start()
    {
        startLayerId = sp.sortingOrder;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Perforating>())
        {
            sp.material = red;
            if (transform.position.y > col.transform.position.y - col.GetComponent<Perforating>().yOffset)
            {
                sp.sortingOrder = 2;
                sp.sortingLayerID = 0;
            }

            if (transform.position.y < col.transform.position.y - col.GetComponent<Perforating>().yOffset)
            {
                sp.sortingOrder = startLayerId;
                sp.sortingLayerID = 0;
            }
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Perforating>())
        {
            sp.material = red;
            if (transform.position.y > other.transform.position.y - other.GetComponent<Perforating>().yOffset)
            {
                sp.sortingOrder = 2;
                sp.sortingLayerID = 0;
            }

            if (transform.position.y < other.transform.position.y - other.GetComponent<Perforating>().yOffset)
            {
                sp.sortingOrder = 4;
                sp.sortingLayerID = 0;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Perforating>())
        {
            sp.material = def;
            sp.sortingOrder = startLayerId;
            sp.sortingLayerName = "UI";
        }
    }
}
