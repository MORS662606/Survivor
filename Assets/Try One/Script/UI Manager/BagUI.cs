using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mors;
using TMPro;


public class BagUI : MonoBehaviour
{
    public string test;
    private GameObject var;
    [SerializeField] private TextMeshProUGUI item_ui_name;
    [SerializeField] private TextMeshProUGUI item_ui_weight;
    [SerializeField] private TextMeshProUGUI item_ui_number;
    [SerializeField] private TextMeshProUGUI item_ui_description;
    [SerializeField] private RawImage item_ui_picture;
    [SerializeField] private Button[] item_buttons = new Button[7];
    [SerializeField] private Dictionary<string, string> item_description = new Dictionary<string, string>();

    private void Item_Dictionary_Initialization()
    {
        string json = SimpleFunction.Json_Read("C:/Users/Mors/Desktop/Information.json");
        Serialize serialize_dictionary = new Serialize();
        serialize_dictionary = JsonUtility.FromJson<Serialize>(json);
        item_description = serialize_dictionary.Data_Back(true);
    }

    private void Get_Component()
    {
        var = GameObject.Find("UI/Bag UI/Item");
        item_ui_name = var.transform.Find("Name").gameObject.GetComponent<TextMeshProUGUI>();
        item_ui_weight = var.transform.Find("Weight").gameObject.GetComponent<TextMeshProUGUI>();
        item_ui_number = var.transform.Find("Number").gameObject.GetComponent<TextMeshProUGUI>();
        item_ui_description = var.transform.Find("Description").gameObject.GetComponent<TextMeshProUGUI>();
        item_ui_picture = var.transform.Find("Picture").gameObject.GetComponent<RawImage>();

        item_buttons = GameObject.Find("UI/Bag UI/ShowItem").GetComponentsInChildren<Button>();
    }

    private void Load_Item_Data()
    {
        int props_number = PlayerData.props_value.Count;
        for (int i = props_number; i < item_buttons.Length; i++)
        {
            item_buttons[i].gameObject.SetActive(false);
        }
    }

    private void Show_Props_Information(int item_index)
    {
        test = PlayerData.props_value[item_index].name;
        item_ui_name.text = PlayerData.props_value[item_index].name;
        item_ui_number.text = "x"+PlayerData.props_value[item_index].num.ToString();
        item_ui_description.text = item_description[PlayerData.props_value[item_index].name];
    }
    public void Click_Button()
    {
        Button var_button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        int button_index = Array.IndexOf(item_buttons, var_button);

        Show_Props_Information(button_index);
    }

    public void Click_Button_Add()
    {

    }
    private void Start()
    { 
        Item_Dictionary_Initialization();
        Get_Component();
        Load_Item_Data();
       

        /*测试区
        item_description.Add("农夫山泉", "有点甜");
        item_description.Add("旺旺牛奶", "比你聪明，比你强");
        Serialize dictionary_serialize = new Serialize(item_description);
        string json = JsonUtility.ToJson(dictionary_serialize);
        Debug.Log(json);
        Debug.Log(item_description);

        SimpleFunction.Json_Write(json, "C:/Users/Mors/Desktop/Information.json");
       */
    }
    void FixedUpdate()
    {
        Click_Button();
    }
}
