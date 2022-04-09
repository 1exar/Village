using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FenceBuilder : MonoBehaviour
{

    public GameObject spawnPrefab;

    public GameObject pointPrefab;

    public float spacing = .6f;

    public LineRenderer ln;

    private List<GameObject> points = new List<GameObject>();

    private List<GameObject> spikes = new List<GameObject>();

    public static FenceBuilder I;
    
    private void Start()
    {
        I = this;
    }

    public void SpawnPoint()
    {
        points.Add(Instantiate(pointPrefab, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 5)),
            quaternion.identity));
    }

    public void RemovePoint()
    {
        Destroy(points[points.Count - 1]);
        points.RemoveAt(points.Count - 1);
    }
    
    public void SpawnFench()
    {
        for (int i = 0; i < points.Count - 1 ; i++)
        {
            SpawnObjects(points[i].transform.position, points[i+1].transform.position);
        }
    }

    enum dir
    {
        up, down, right, left, upleft, upright, rightdown, leftDown
    }
    private void SpawnObjects(Vector3 a, Vector3 b)
    {
        Vector3 direction = (b - a).normalized;
        int howMany = (int) (Vector3.Distance(a, b) / spacing);

        float newSpacing = 16;

        int order = 2;

        dir current = default;

        GameObject[] spawned = new GameObject[howMany + 1];

        if (direction.x > 0)// right
        {
            if (direction.y > 0.4f)//rightUp
            {
                order = spawned.Length + 5;
            }
            else if (direction.y < -0.4f)//rightDown
            {
            }
        }
        else//left
        {
            if (direction.y > 0.4f)//leftUp
            {
                order = spawned.Length + 5;
            }
            else if (direction.y < -0.4f)//leftDown
            {
            }
        }
        
        for (int i = 0; i < howMany + 1; i++)
        {
            spawned[i] = Instantiate(spawnPrefab);
            
            if(i == 0)
            {
                newSpacing = 0;
            }else{
                newSpacing += spacing;
            }

            if (direction.x > 0)// right
            {
                order++;
                spawned[i].name = "right";
                if (direction.y > 0.4f)//rightUp
                {
                    spawned[i].name = "rightUp";
                    order--;
                }
                else if (direction.y < -0.4f)//rightDown
                {
                    spawned[i].name = "rightDown";
                }
            }
            else//left
            {
                order++;
                spawned[i].name = "left";
                if (direction.y > 0.4f)//leftUp
                {
                    spawned[i].name = "leftUp";
                    order--;
                }
                else if (direction.y < -0.4f)//leftDown
                {
                    spawned[i].name = "leftDown";
                    order++;
                }
            }
            
            spawned[i].GetComponent<SpriteRenderer>().sortingOrder = order;
            spawned[i].transform.position = a + (direction * newSpacing) + Vector3.up * Random.Range(0,.5f); 
            spikes.Add(spawned[i]);
        }
    }

    public void DestroyObjects()
    {
        foreach (var spike in spikes)
        {
            Destroy(spike);
        }
        spikes.Clear();
    }
    
    private void FixedUpdate()
    {
        if (points.Count > 0)
        {
            ln.positionCount = points.Count;
            Vector3[] p = new Vector3[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                p[i] = new Vector3(points[i].transform.position.x, points[i].transform.position.y, -6);
            }
            ln.SetPositions(p);
        }
    }

    private void HidePoints()
    {
        foreach (var point in points)
        {
            point.SetActive(false);
        }
    }

    private void ShowPoints()
    {
        foreach (var point in points)
        {
            point.SetActive(true);
        }
    }

    public void RemoveAll()
    {
        foreach (var p in points)
        {
         Destroy(p);
         DestroyObjects();
        }
    }
}
