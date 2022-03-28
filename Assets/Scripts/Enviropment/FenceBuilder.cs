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

    private void SpawnObjects(Vector3 a, Vector3 b)
    {
        Vector3 direction = (b - a).normalized;
        int howMany = (int) (Vector3.Distance(a, b) / spacing);

        float newSpacing = 16;
        
        print(direction);

        GameObject[] spawned = new GameObject[howMany + 1];
        int order = 5;

        
        if (direction.y < -.35f)
        {
            order = 5;
        }
        else
        {
            order = spawned.Length + 5;
            spawned = spawned.Reverse().ToArray();
        }

        if (direction.x > -0.35f)
        {
            print("right");
        }

        for (int i = 0; i < howMany + 1; i++)
        {
            spawned[i] = Instantiate(spawnPrefab);
            if (direction.y < -.35f)
            {
                order++;
            }
            else if(direction.x > -0.35f)
            {
                spawned[i].name = "right";
                order--;
            }
            else
            {
                order--;
            }
            if(i == 0)
            {
                newSpacing = 0;
                spawned[i].GetComponent<SpriteRenderer>().sortingOrder = 5;
            }else{
                newSpacing += spacing;
                spawned[i].GetComponent<SpriteRenderer>().sortingOrder = order;
            }

            spawned[i].transform.position = a + (direction * newSpacing) + Vector3.up * Random.Range(0,.5f); 
            spikes.Add(spawned[i]);
            //spawned[i].name = i + "";
            
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
