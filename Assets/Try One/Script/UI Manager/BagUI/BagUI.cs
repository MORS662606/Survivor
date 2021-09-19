using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UserNamespace;
using UserNamespace.CustomData;

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
    [Header("UI Information")] public TextMeshProUGUI uiName;
    public TextMeshProUGUI uiWeight;
    public TextMeshProUGUI uiNumber;
    public TextMeshProUGUI uiDescription;
    public RawImage uiPicture;
    public Button[] items;
     public  List<ItemInfo>  ItemInfoLocal = new List<ItemInfo>();

    

    private void Awake()
    {
        //获取显示组件
        var item = GameObject.Find("UI/Bag UI/Item");
        uiName = item.transform.Find("Name").gameObject.GetComponent<TextMeshProUGUI>();
        uiWeight = item.transform.Find("Weight").gameObject.GetComponent<TextMeshProUGUI>();
        uiNumber = item.transform.Find("Number").gameObject.GetComponent<TextMeshProUGUI>();
        uiDescription = item.transform.Find("Description").gameObject.GetComponent<TextMeshProUGUI>();
        uiPicture = item.transform.Find("Picture").gameObject.GetComponent<RawImage>();
        items = GameObject.Find("UI/Bag UI/ShowItem").GetComponentsInChildren<Button>();

        
    }

    private void Start()
    {
        ItemInfoLocal = GameData.ItemInfo;

    }

    internal void LoadToUI(string id)
    {
        var itemID = UserFunction.ToInt(id);
        var info = GameData.ItemInfo.Find(var => var.ID == itemID);
        //var info =GameData.ItemInfo[itemID];
        uiDescription.text = info.Description;
        uiName.text = info.Name;
        var bagItem = GameData.ItemList.Find(var => var.itemId == itemID);
        uiWeight.text = (info.Weight * bagItem.itemNum).ToString();
    }
}