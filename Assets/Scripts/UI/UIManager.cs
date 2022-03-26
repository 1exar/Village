using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Jobs;

public class UIManager : MonoBehaviour
{

    public List<GameObject> panels;
    public GameObject lumberJobPane, applyButton, applyBuilding;
    
    public static UIManager I;

    public List<GameObject> uiToHide;

    [SerializeField] private Transform taskListParent;
    [SerializeField] private GameObject taskLitsObjectPrefab, headerPanel, downPanel;

    private void Awake()
    {
        I = this;
    }

    public void AddNewTaskToTaskList(Task job)
    {
        TaskListObject newTask = Instantiate(taskLitsObjectPrefab, taskListParent).GetComponent<TaskListObject>();
        newTask.Init(job);
    }
    
    public void CloseAllPanels()
    {
        foreach (var panel in panels)
        {
            panel.SetActive(false);
        }
    }

    public void OnLumberJobChoise()
    {
        lumberJobPane.SetActive(true);
    }

    public void CloseUI()
    {
        foreach (var ui in uiToHide)
        {
            ui.SetActive(false);
        }
    }

    public void ShowUI()
    {
        foreach (var ui in uiToHide)
        {
            ui.SetActive(true);
        }
        applyButton.SetActive(false);
    }

}
