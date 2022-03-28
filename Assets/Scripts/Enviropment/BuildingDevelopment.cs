using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDevelopment : MonoBehaviour
{

    [SerializeField]
    private Sprite[] buildingStage;

    [SerializeField]
    private int _currentStage = 0;
    [SerializeField]
    private float timeToBuild;

    private float startTimeToBuild;

    private SpriteRenderer sp;
    
    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    public void StartDevelopment()
    {
        _currentStage = buildingStage.Length;
        sp.sprite = buildingStage[_currentStage - 1];
        startTimeToBuild = timeToBuild;

        StartCoroutine(Procces());
    }
    
    private IEnumerator Procces()
    {
        timeToBuild--;
        int stage = (int)startTimeToBuild / buildingStage.Length;
        _currentStage = (int)timeToBuild / stage;
        if(_currentStage > buildingStage.Length - 1)
            sp.sprite = buildingStage[_currentStage - 1];
        else
            sp.sprite = buildingStage[_currentStage];
        if (timeToBuild > 0)
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(Procces());
        }
        else
        {
            CompleteDevelopment();
        }
    }

    private void CompleteDevelopment()
    {
        BuildingManager.I.buildings.Add(GetComponent<Building>());
        Destroy(this);
        print("здание построенно");
    }

}
