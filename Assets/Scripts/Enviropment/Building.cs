using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Items;
using UnityEngine;

public class Building : MonoBehaviour, IBuilding
{
    public string buildingName;
    public bool haveStorage;
    [SerializeField]
    public Storage storage;

    public Vector3 fronPosOffset;
    
    public void OnMouseOver()
    {
      //  throw new NotImplementedException();
    }

    public void OnMouseExit()
    {
    //    throw new NotImplementedException();
    }

    public void OnMouseDown()
    {
        if (haveStorage && storage)
        {
            BuildingStorageInfoPanel.I.Init(storage.GetAllItemsInStorage(), "pososi");
        }
    }

    public Storage GetStorage()
    {
        if (haveStorage) return storage;
        else return null;
    }
    
    public void Upgrade()
    {
        throw new NotImplementedException();
    }
}
