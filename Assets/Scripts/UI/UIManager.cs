using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public List<GameObject> panels;
    public GameObject lumberJobPane, collectChoiseItemsPanel, applyButton;

    public HeaderPanel header;
    
    public static UIManager I;

    public List<GameObject> uiToHide;

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

    public void CloseUI()
    {
        foreach (var ui in uiToHide)
        {
            ui.SetActive(false);
        }
        applyButton.SetActive(true);
    }

    public void ShowUI()
    {
        foreach (var ui in uiToHide)
        {
            ui.SetActive(true);
        }
        applyButton.SetActive(false);
    }
    
    public void OnCollectJobChoise()
    {
        collectChoiseItemsPanel.SetActive(true);
    }

}
