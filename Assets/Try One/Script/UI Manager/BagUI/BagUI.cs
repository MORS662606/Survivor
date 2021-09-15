using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

struct ItemInformation
{
    private Image itemPicture;
    private string itemName;
    private string itemWeight;
    private string itemNumber;
    private string itemDescription;
}

public class BagUI : MonoBehaviour
{
    [Header("UI Information")]
    public TextMeshProUGUI uiName;
    public TextMeshProUGUI uiWeight;
    public TextMeshProUGUI uiNumber;
    public TextMeshProUGUI uiDescription;
    public RawImage uiPicture;
    public Button[] items;
    private PlayerBag _playerBag;
    private void Start()
    {
        //获取显示组件
        var item = GameObject.Find("UI/Bag UI/Item");
        uiName = item.transform.Find("Name").gameObject.GetComponent<TextMeshProUGUI>();
        uiWeight = item.transform.Find("Weight").gameObject.GetComponent<TextMeshProUGUI>();
        uiNumber = item.transform.Find("Number").gameObject.GetComponent<TextMeshProUGUI>();
        uiDescription = item.transform.Find("Description").gameObject.GetComponent<TextMeshProUGUI>();
        uiPicture = item.transform.Find("Picture").gameObject.GetComponent<RawImage>();
        items = GameObject.Find("UI/Bag UI/ShowItem").GetComponentsInChildren<Button>();
        /*for (var i = 0; i < items.Length; i++)
        {
            if (i > _playerBag.bagItem.Count)
            {
                            items[i].image.gameObject.SetActive(false);
            }
        }*/
    }

    public void ClickButton()
    {
        Debug.Log(_playerBag.bagItem.Count);
   
    }
}
