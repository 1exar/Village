using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceBuilderPanel : MonoBehaviour
{

    public void AddNewPoint()
    {
        FenceBuilder.I.SpawnPoint();
    }

    public void RemoveNewPoint()
    {
        FenceBuilder.I.RemovePoint();
    }

    public void ShowFence()
    {
        FenceBuilder.I.SpawnFench();
    }

    public void BuildFence()
    {
        
    }

    public void RemoveFench()
    {
        FenceBuilder.I.DestroyObjects();
    }

    public void CancleBuildFence()
    {
        FenceBuilder.I.RemoveAll();
        Destroy(FenceBuilder.I.gameObject);
    }
    
}