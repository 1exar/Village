using System;
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
        if(GUILayout.Button("Обновить список элементов", GUILayout.Height(30))) db.ClearAllItemsList();
        foreach (var item in db.getAllItemsType())
        {
            item.Name = EditorGUILayout.TextField("Название предмета", item.Name);
            item.Sprite = (Sprite)EditorGUILayout.ObjectField("Иконка", item.Sprite, typeof(Sprite), false);
        }
    }
}