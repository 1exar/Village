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
        storage.GetAllItems();
    }

    public void Upgrade()
    {
        throw new NotImplementedException();
    }
}
