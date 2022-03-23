using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public List<GameObject> panels;
    public GameObject lumberJobPane, collectChoiseItemsPanel;

    public static UIManager I;

    private void Awake()
    {
        I = this;
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

    public void OnCollectJobChoise()
    {
        collectChoiseItemsPanel.SetActive(true);
    }

}
