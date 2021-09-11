using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

  [Serializable]public struct Item
{
    public int itemId;
    public int itemNum;
    public Item(int id,int num)
    {
        itemId = id;
        itemNum = num;
    }
}

public class PlayerBag : MonoBehaviour
{
    public  List<Item> bagItem = new List<Item>();

    private void Awake()
    {
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
        Debug.Log(itemInfoStr[0]);
        Debug.Log(itemInfoStr[1]);
        var itemInfo = StringToInt(itemInfoStr);
        if (bagItem.Exists(var => var.itemId == itemInfo[0]))
        {
            var addNum = bagItem.Find(var => var.itemId == itemInfo[0]);
            addNum.itemNum = addNum.itemNum + itemInfo[1];
        }
        else
        {
            var newItem = new Item(itemInfo[0],itemInfo[1]);
            bagItem.Add(newItem);
        }
        Destroy(hit.collider.gameObject);
    }


    public string SerializeXml(object data)
    {
        using (StringWriter sw = new StringWriter())
        {
            XmlSerializer xz = new XmlSerializer(data.GetType());
            xz.Serialize(sw, data);
            return sw.ToString();
        }
    }
    private int[] StringToInt(string[] str)
    {
        int[] ret = { 0, 0 };
        for (int i = 0; i < 2; i++)
        {
            for (int k = 0; k < str[i].Length; k++)
            {
                ret[i] = ret[i] * 10 + str[i][k]-'0';
            }
        }

        return ret;
    }

    }