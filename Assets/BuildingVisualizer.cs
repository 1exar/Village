using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingVisualizer : MonoBehaviour
{
    private bool move;
    private SpriteRenderer sp;

    private int startLayerId;
    
    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        startLayerId = sp.sortingOrder;
    }

    public void OnMouseDown()
    {
        CameraController.I.canDrag = false;
        move = true;
    }

    public void OnMouseUp()
    {
        CameraController.I.canDrag = true;
        move = false;
    }

    public void Update()
    {
        if(!move) return;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Perforating>())
        {
            sp.color = Color.red;
            if (transform.position.y > other.transform.position.y - other.GetComponent<Perforating>().yOffset) sp.sortingOrder = 2;
            if (transform.position.y < other.transform.position.y - other.GetComponent<Perforating>().yOffset) sp.sortingOrder = startLayerId;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Perforating>())
        {
            sp.color = Color.white;
        }
    }
}
