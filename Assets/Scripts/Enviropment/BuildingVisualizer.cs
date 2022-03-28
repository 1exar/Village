using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildingVisualizer : MonoBehaviour
{
    private bool move;
    private SpriteRenderer sp;

    private int startLayerId;

    [SerializeField]
    private Material red, def;
    
    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        startLayerId = sp.sortingOrder;
        red = Resources.Load("Materials/red", typeof(Material)) as Material;
        def = Resources.Load("Materials/def", typeof(Material)) as Material;
    }

    public void OnMouseDown()
    {
        CameraController.I.canDrag = false;
        GetComponent<NavMeshObstacle>().enabled = false;
        move = true;
    }

    public void OnMouseUp()
    {
        CameraController.I.canDrag = true;
        GetComponent<NavMeshObstacle>().enabled = true;
        move = false;
    }

    public void Update()
    {
        if(!move) return;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        
       /* foreach (var cast in Physics2D.CircleCastAll(transform.position, 2, Vector2.up))
        {
            if (cast.transform.GetComponent<Perforating>())
            {
                if (cast.transform.position.y - cast.transform.GetComponent<Perforating>().yOffset < transform.position.y)
                {
                    sp.material = def;
                    sp.sortingOrder = startLayerId;
                }
                else
                {
                    sp.sortingOrder = 2;
                    sp.material = red;
                }
            }
        }*/
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Perforating>())
        {
            sp.material = red;
            if (transform.position.y > col.transform.position.y - col.GetComponent<Perforating>().yOffset) sp.sortingOrder = 2;
            if (transform.position.y < col.transform.position.y - col.GetComponent<Perforating>().yOffset) sp.sortingOrder = startLayerId;
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Perforating>())
        {
            sp.material = red;
            if (transform.position.y > other.transform.position.y - other.GetComponent<Perforating>().yOffset) sp.sortingOrder = 2;
            if (transform.position.y < other.transform.position.y - other.GetComponent<Perforating>().yOffset) sp.sortingOrder = startLayerId;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Perforating>())
        {
            sp.material = def;
        }
    }
}
