using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CollectJobPanel : MonoBehaviour
{

    public Slider ballsSlider;
    public Text ballsText;
    public Dropdown avaibleItemsDropdown;
    public Toggle collectAllDropsToggle;
    [SerializeField]
    private List<string> avaibleItemsList = new List<string>();
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
        UIManager.I.CloseAllPanels();
    }

    public void ChangeDropDownState(bool collectAll)
    {
        if (collectAll)
        {
            avaibleItemsDropdown.gameObject.SetActive(false);
        }
        else
        {
            RefreshAvaibleItemsDropdown();
            avaibleItemsDropdown.gameObject.SetActive(true);
        }
    }

    private void RefreshAvaibleItemsDropdown()
    {
        avaibleItemsDropdown.ClearOptions();

        foreach (var item in WorldResourceManager.I.dropedItems)
        {
            avaibleItemsList.Add(item.Name);
        }

        _dropedItemsList = WorldResourceManager.I.dropedItems;
        avaibleItemsList = avaibleItemsList.Distinct().ToList();
        avaibleItemsDropdown.AddOptions(avaibleItemsList);
    }
    
}
