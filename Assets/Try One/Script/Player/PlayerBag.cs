using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mors;

public class PlayerBag : MonoBehaviour
{
    ///<value> 
    ///包装物品的类项
    ///</value>
    [SerializeField] private Serialize serializeProps = new Serialize();

    private void Awake()
    {
        BagItemsLoad("C:/Users/expre/Desktop/DataBase.json");
    }
    
    /// <summary>
    /// 加载存档中的背包物品，加载物品的美术资源
    /// </summary>
    /// <param name="save_path">
    /// 背包存档路径
    /// </param>
  private void BagItemsLoad(string save_path)
    {
        serializeProps = JsonUtility.FromJson<Serialize>(SimpleFunction.Json_Read(save_path));
        PlayerData.props_value = serializeProps.props;
    }

    /// <summary>
    /// 将新拾取物品加入背包中的物品栏
    /// </summary>
    /// <param name="new_item">
    /// 新拾取的物品
    /// </param>
    private void BagPropsUpdate(item new_item)
    {

        for (int i = 0; i < serializeProps.props.Count; i++)
        {
            if (serializeProps.props[i].name == new_item.name)
            {
                serializeProps.props[i].add_number(new_item.num);
                return;
            }
        }
        serializeProps.props.Add(new_item);
    }
    
    /// <summary>
    /// 捡起物品时的操作
    /// </summary>
    internal void PropsPickUp()
    {
        //检测射线是否触及物体
        bool isCollider = Physics.Raycast(PublicVariables.ray, out RaycastHit hit, 10, 1 << 8);
        if (!isCollider) return;
        
        BagPropsUpdate(new item(hit.collider.gameObject.name));
        //摧毁环境物品
        Destroy(hit.collider.gameObject);
        PlayerData.props_value = serializeProps.props;
        serializeProps.Load("C:/Users/expre/Desktop/DataBase.json");
    }


}