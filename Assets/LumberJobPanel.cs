using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LumberJobPanel : MonoBehaviour
{

    public Slider count, avaibleBalls;
    public Text avaibleSlider, countSlider;
    
    
    public void Open()
    {
        count.maxValue = WorldResourceManager.I.AvaibleWood();
        avaibleBalls.maxValue = GameManager.I.balls.Count(b => !b.haveTask);
        RefreshSliders();
    }

    public void RefreshSliders()
    {
        avaibleSlider.text = avaibleBalls.value + "";
        countSlider.text = count.value + "";
    }

    public void ChoiseJob()
    {
        JobsManager.I.LumberTrees((int)count.value,(int)avaibleBalls.value);
        UIManager.I.CloseAllPanels();
    }
    
}
