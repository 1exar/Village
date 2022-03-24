using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelector : MonoBehaviour
{

    public Vector3 TL, BL, TR, BR;

    public GameObject p1, p2;
    
    [SerializeField]
    private LineRenderer ln;

    public static ObjectSelector I;

    public enum FindType
    {
        Tree, Drop
    }

    public FindType current;
    
    public List<GameObject> SearchInArea(FindType options)
    {
        List<GameObject> result = new List<GameObject>();

        switch (options)
        {
            case FindType.Tree:
                foreach (var tree in GameObject.FindGameObjectsWithTag("Tree"))
                {
                    if(IsWithinPolygon(tree.transform.position)) result.Add(tree);
                }
                break;
            case FindType.Drop:
                break;
        }
        
        return result;
    }

    private void Awake()
    {
        I = this;
    }

    public void ShowSelectors()
    {
        p1.SetActive(true);
        p2.SetActive(true);
        p1.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2 , 5));
        p2.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 5));
        ln.enabled = true;
    }

    public void OnApplyButton()
    {
        ln.enabled = false;
        p2.transform.position = new Vector3(p2.transform.position.x, p2.transform.position.y, 10);
        CalculatePointPositions();
        SearchInArea(current);
        p1.SetActive(false);
        p2.SetActive(false);
    }
    
    private bool IsWithinPolygon(Vector3 unitPos)
    {
        bool isWithinPolygon = false;

        //The polygon forms 2 triangles, so we need to check if a point is within any of the triangles
        //Triangle 1: TL - BL - TR
        if (IsWithinTriangle(unitPos, TL, BL, TR))
        {
            return true;
        }

        //Triangle 2: TR - BL - BR
        if (IsWithinTriangle(unitPos, TR, BL, BR))
        {
            return true;
        }

        return isWithinPolygon;
    }
    
    private bool IsWithinTriangle(Vector3 p, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        bool isWithinTriangle = false;

        //Need to set z -> y because of other coordinate system
        float denominator = ((p2.z - p3.z) * (p1.x - p3.x) + (p3.x - p2.x) * (p1.z - p3.z));

        float a = ((p2.z - p3.z) * (p.x - p3.x) + (p3.x - p2.x) * (p.z - p3.z)) / denominator;
        float b = ((p3.z - p1.z) * (p.x - p3.x) + (p1.x - p3.x) * (p.z - p3.z)) / denominator;
        float c = 1 - a - b;

        //The point is within the triangle if 0 <= a <= 1 and 0 <= b <= 1 and 0 <= c <= 1
        if (a >= 0f && a <= 1f && b >= 0f && b <= 1f && c >= 0f && c <= 1f)
        {
            isWithinTriangle = true;
        }

        return isWithinTriangle;
    }

    private void FixedUpdate()
    {
        CalculatePointPositions();
        DrawBox();
    }

    private void CalculatePointPositions()
    {
        TL = p1.transform.position;
        BR = p2.transform.position;
        TR = new Vector3(BR.x, TL.y, TL.z);
        BL = new Vector3(TL.x, BR.y, BR.z);
    }
    
    private void DrawBox()
    {
        ln.positionCount = 5;
        List<Vector3> points = new List<Vector3>();
        points.Add(TL);
        points.Add(TR);
        points.Add(BR);
        points.Add(BL);
        points.Add(TL);
        ln.SetPositions(points.ToArray());
    }
    
}
