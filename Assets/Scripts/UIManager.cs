using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public List<GameObject> panels;

    public Slider avaibleBalls;
    public Slider count;

    public Text avaibleSlider, countSlider;
    
    public GameObject countAndBallPanel;

    private enum JobVariant
    {
        LumberJob
    }

    private JobVariant _currentJobVariant;

    public void RefreshSliders()
    {
        avaibleSlider.text = avaibleBalls.value + "";
        countSlider.text = count.value + "";
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
        countAndBallPanel.SetActive(true);
        count.maxValue = WorldResourceManager.I.AvaibleWood();
        avaibleBalls.maxValue = GameManeger.I.balls.Count(b => !b.haveTask);
        _currentJobVariant = JobVariant.LumberJob;
        RefreshSliders();
    }

    public void MakeJob()
    {
        switch (_currentJobVariant)
        {
            case JobVariant.LumberJob:
                JobsManager.I.LumberTrees((int)count.value,(int)avaibleBalls.value);
                break;
        }
        CloseAllPanels();
        countAndBallPanel.SetActive(false);
    }
    
}
