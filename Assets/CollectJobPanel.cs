using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CollectJobPanel : MonoBehaviour
{

    public Slider countSlider, ballsSlider;
    public Text ballsText, countText;
    
    
    public void Open()
    {
        countSlider.maxValue = WorldResourceManager.I.AvaibleWood();
        ballsSlider.maxValue = GameManager.I.balls.Count(b => !b.haveTask);
        RefreshSliders();
    }

    public void RefreshSliders()
    {
        ballsText.text = ballsSlider.value + "";
        countText.text = countSlider.value + "";
    }

    public void ChoiseJob()
    {
        JobsManager.I.LumberTrees((int)countSlider.value,(int)ballsSlider.value);
        UIManager.I.CloseAllPanels();
    }
    
}
