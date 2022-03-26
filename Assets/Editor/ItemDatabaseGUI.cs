using System;
using Items;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemDatabase))]
public class ItemDatabaseGUI : Editor
{

    private ItemDatabase db;

    private void OnEnable()
    {
        db = (ItemDatabase) target;
    }

    public override void OnInspectorGUI()
    {

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Remove"))
        {
            db.RemoveCurrentElement();
        }    
        if (GUILayout.Button("Add"))
        {
            db.AddElement();
        }   
        if (GUILayout.Button("Next"))
        {
            db.NextItem();
        }
        if (GUILayout.Button("Prev"))
        {
            db.PrevItem();
        }
        
        GUILayout.EndHorizontal();
        base.OnInspectorGUI();

    }
}