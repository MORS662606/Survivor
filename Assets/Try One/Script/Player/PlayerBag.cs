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
    public List<Item> bagItem = new List<Item>();
    public string savePath = "D:/BagData.xml";
    private void Awake()
    {
        SerializeXmlRead(savePath);

        PlayerData.BackUp = bagItem;
    }

    private void Update()
    {
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
        if (bagItem.Exists(var => var.itemId == itemInfo[0]))
        {
            var addNum = bagItem.Find(var => var.itemId == itemInfo[0]);
            addNum.itemNum = addNum.itemNum + itemInfo[1];
        }
        else
        {
            var newItem = new Item(itemInfo[0], itemInfo[1]);
            bagItem.Add(newItem);
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
            using (var sw = new StreamWriter(path,false,Encoding.GetEncoding("UTF-8")))
            {
                sw.Write(xmlString);
            }
        }
        catch (Exception exception)
        {
            Debug.Log(exception.Message);
        }
    }
    private void SerializeXmlRead(string path)
    {
        try
        {
            using (var sr = new StreamReader(path, Encoding.GetEncoding("UTF-8")))
            {
                var xz = new XmlSerializer(typeof(List<Item>));
                bagItem = (List<Item>)xz.Deserialize(sr);
            }
        }
        catch (Exception exception)
        {
            Debug.Log(exception.Message);
        }
        
        Debug.Log("Load Success");
    }
    private int[] StringToInt(string[] str)
    {
        int[] ret = { 0, 0 };
        for (int i = 0; i < 2; i++)
        {
            for (int k = 0; k < str[i].Length; k++)
            {
                ret[i] = ret[i] * 10 + str[i][k] - '0';
            }
        }

        return ret;
    }
}