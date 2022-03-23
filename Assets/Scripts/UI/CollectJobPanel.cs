using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Jobs;

public class CollectJobPanel : MonoBehaviour
{

    public Slider ballsSlider;
    public Text ballsText;
    public Dropdown avaibleItemsDropdown;
    public Toggle collectAllDropsToggle;
    [SerializeField]
    private List<string> avaibleItemsList = new List<string>();
    [SerializeField]
    private List<DropedItem> _dropedItemsList = new List<DropedItem>();

    public void Open()
    {
        ballsSlider.maxValue = GameManager.I.balls.Count(b => !b.haveTask);
        RefreshSliders();
    }

    public void RefreshSliders()
    {
        ballsText.text = ballsSlider.value + "";
    }

    public void ChoiseJob()
    {
        CollectDrop job = new CollectDrop((int) ballsSlider.value, _dropedItemsList);
       // JobsManager.I.CollectItems();
        UIManager.I.CloseAllPanels();
    }

    public void ChangeDropDownState(bool collectAll)
    {
        if (collectAll)
        {
            _dropedItemsList.Clear();
            avaibleItemsDropdown.gameObject.SetActive(false);
        }
        else
        {
            RefreshAvaibleItemsDropdown();
            avaibleItemsDropdown.gameObject.SetActive(true);
            OnDropdownSelect(avaibleItemsDropdown.value);
        }
    }

    private void RefreshAvaibleItemsDropdown()
    {
        avaibleItemsDropdown.ClearOptions();

        avaibleItemsList.Clear();
        
        foreach (var item in WorldResourceManager.I.dropedItems)
        {
            avaibleItemsList.Add(item.Name);
        }

        avaibleItemsList = avaibleItemsList.Distinct().ToList();
        avaibleItemsDropdown.AddOptions(avaibleItemsList);
    }

    public void OnDropdownSelect(int itemId)
    {
        _dropedItemsList.Clear();
        foreach (var item in WorldResourceManager.I.dropedItems)
        {
            if (item.Name == avaibleItemsList[itemId])
            {
                _dropedItemsList.Add(item);
                break;
            }
        }
    }
    
}
