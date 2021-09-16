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

    private Dictionary<int, ItemData> informationDictionary = new Dictionary<int, ItemData>();
    private List<ItemData> itemInfoDic = new List<ItemData>();

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
        /*for (var i = 0; i < items.Length; i++)
        {
            if (i > _playerBag.bagItem.Count)
            {
                            items[i].image.gameObject.SetActive(false);
            }
        }*/
    }

    private void Start()
    {
        var tri = new ItemData();
        tri.ID = 3;
        tri.Name = "Van";
        tri.Description = "?";
        tri.Weight = 30;
        informationDictionary.Add(3, tri);
        itemInfoDic.Add(tri);
        UserFunction.SerializeXmlWrite(itemInfoDic, "D:/Dictionary.xml");
    }

    internal void LoadToUI(string id)
    {
        var itemID = UserFunction.ToInt(id);
        var info = informationDictionary[itemID];
        uiDescription.text = info.Description;
        uiName.text = info.Name;
        var bagItem = PlayerData.BackUp.Find(var => var.itemId == itemID);
        uiWeight.text = (info.Weight * bagItem.itemNum).ToString();
    }
}