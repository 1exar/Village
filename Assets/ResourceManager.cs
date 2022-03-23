using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class ResourceManager : MonoBehaviour
{

    public List<Resource> myRecources = new List<Resource>();
    public static ResourceManager I;

    private void Awake()
    {
        I = this;
    }

    public void CheckAllStorages()
    {
        
    }
    
}

namespace Items
{
    public class Resource
    {
        
    }
}
