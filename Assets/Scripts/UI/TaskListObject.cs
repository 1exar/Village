using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Jobs;

public class TaskListObject : MonoBehaviour
{

    [SerializeField] private Text min, max, taskName;
    [SerializeField] private Slider progress;

    [SerializeField] private Task current;

    public void Init(Task newTask)
    {
        taskName.text = newTask.name;
        progress.maxValue = newTask.progressMax;
        current = newTask;
        min.text = "0";
        max.text = newTask.progressMax + "";
    }
    
    public void CancelTask()
    {
        current.CancelTask();
    }
    
    private void Update()
    {
        if (current != null)
        {
            progress.value = current.progressCurrent;
            if (progress.value == progress.maxValue)
            {
                Destroy(gameObject);
            }
        }
    }
}
