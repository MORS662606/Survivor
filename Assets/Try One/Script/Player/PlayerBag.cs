using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;
using UserNamespace;
using UserNamespace.CustomData;


public class PlayerBag : MonoBehaviour
{
    public List<ItemList> BagItemLocal = new List<ItemList>();
    private const string BagListPath = "D:/BagData.xml";
    private const string BagListPath2 = "D:/BagData2.xml";
    private const string ItemInfoPath = "D:/Dictionary.xml";

    private void Awake()
    {
        GameLoad(BagListPath, ItemInfoPath);
        BagItemLocal = GameData.ItemList;
    }


    internal void Pickup()
    {
        var isCollider = Physics.Raycast(PublicVariables.ray, out RaycastHit hit, 10, 1 << 8);
        if (!isCollider) return;
        var unknownItem = hit.collider.gameObject.name;
        var itemInfoStr = unknownItem.Split('_');
        // Debug.Log(itemInfoStr[0]);
        //Debug.Log(itemInfoStr[1]);
        var itemInfo = UserFunction.ToInt(itemInfoStr);
        if (BagItemLocal.Exists(var => var.itemId == itemInfo[0]))
        {
            var addNum = BagItemLocal.Find(var => var.itemId == itemInfo[0]);
            addNum.itemNum = addNum.itemNum + itemInfo[1];
        }
        else
        {
            var newItem = new ItemList(itemInfo[0], itemInfo[1]);
            BagItemLocal.Add(newItem);
        }

        Destroy(hit.collider.gameObject);
    }

    public void SerializeXmlWrite(object data, string path)
    {
        string xmlString;
        using (var sw = new StringWriter())
        {
            var xz = new XmlSerializer(data.GetType());
            xz.Serialize(sw, data);
            xmlString = sw.ToString();
        }

        try
        {
            using (var sw = new StreamWriter(path, false, Encoding.GetEncoding("UTF-8")))
            {
                sw.Write(xmlString);
            }
        }
        catch (Exception exception)
        {
            Debug.Log(exception.Message);
        }
    }

    private void GameLoad(string pathBagList, string pathItemInfo)
    {
        try
        {
            //背包物品
            using (var sr = new StreamReader(pathBagList, Encoding.UTF8))
            {
                var xz = new XmlSerializer(typeof(List<ItemList>));
                GameData.ItemList = (List<ItemList>)xz.Deserialize(sr);
            }

            //物品数据
            using (var sr = new StreamReader(pathItemInfo, Encoding.UTF8))
            {
                var xz = new XmlSerializer(typeof(List<ItemInfo>));
                GameData.ItemInfo = (List<ItemInfo>)xz.Deserialize(sr);
            }
        }
        catch (Exception exception)
        {
            Debug.Log(exception.Message);
        }

        Debug.Log("Load Success");
    }
}