using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Jobs;

public class LumberJobPanel : MonoBehaviour
{

    public Slider avaibleBalls;
    public Text avaibleSlider;

    public Text treesCount;
    
    [SerializeField]
    private List<GameObject> trees = new List<GameObject>();

    public void Open()
    {
        avaibleBalls.maxValue = GameManager.I.balls.Count(b => !b.GetComponent<Ball>().haveTask);
        treesCount.text = 0 + "";
        RefreshSliders();
    }

    public void OnApplyButton()
    {
        trees = ObjectSelector.I.SearchInArea(ObjectSelector.FindType.Tree);
        treesCount.text = trees.Count + "";
    }
    
    public void RefreshSliders()
    {
        avaibleSlider.text = avaibleBalls.value + "";
    }

    public void ChoiseJob()
    {
        LumberTrees lumber = new LumberTrees(trees, GameManager.I.avaibleBalls((int)avaibleBalls.value));
       // JobsManager.I.LumberTrees((int)count.value,(int)avaibleBalls.value);
       UIManager.I.CloseAllPanels();
       UIManager.I.applyButton.SetActive(false);
    }

    public void SelectRegion()
    {
        UIManager.I.CloseUI();
        UIManager.I.applyButton.SetActive(true);
        ObjectSelector.I.current = ObjectSelector.FindType.Tree;
        ObjectSelector.I.ShowSelectors();
    }

}
