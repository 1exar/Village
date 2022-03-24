using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMover : MonoBehaviour
{

    private bool move;
    
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
}
