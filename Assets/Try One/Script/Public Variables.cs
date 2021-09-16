using UnityEngine;
using Mors;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using UserNamespace.CustomData;

namespace Mors
{
    public enum MouseStatus
    {
        Locked,
        Freedom
    };
    public enum GameStatus
    {
        Main,
        Bag,
        Pause
    };
    public enum GameCanvas
    {
        PauseUI,
        MainUI,
        BagUI
    };
    public enum PlayerCondition
    {
        Walk,
        Run,
        Stand
    };
    public enum PlayerBuff
    {
    };

    [Serializable]
    class item
    {
        [SerializeField] private string item_name;
        [SerializeField] private byte item_number;

        internal byte num
        {
            get { return item_number; }
        }

        internal string name
        {
            get { return item_name; }
        }

        internal void add_number(byte num)
        {
            if ((item_number + num) > 255)
            {
                item_number = 255;
            }
            else
            {
                item_number += num;
            }
        }

        //Analysis Data to Struct the item
        internal item(string Data)
        {
            item_number = 0;
            char[] stop = { '_' };
            string[] var = Data.Split(stop, 3);
            item_name = var[0];
            for (int i = 0; i < var[1].Length; i++)
            {
                item_number = (byte)(item_number * 10 + var[1][i] - '0');
            }
        }
    }

    [Serializable]
    class Serialize
    {
        [SerializeField] internal List<string> dictionary_t_key;
        [SerializeField] internal List<string> dictionary_t_value;
        [SerializeField] internal List<item> props;

        public Serialize()
        {
        }

        public Serialize(List<item> data)
        {
            props = new List<item>();
            props = data;
        }

        public Serialize(Dictionary<string, string> data)
        {
            dictionary_t_key = new List<string>();
            dictionary_t_value = new List<string>();
            foreach (string key in data.Keys)
            {
                dictionary_t_key.Add(key);
                dictionary_t_value.Add(data[key]);
            }
        }

        internal List<item> Data_Back()
        {
            return props;
        }

        internal Dictionary<string, string> Data_Back(bool temp = true)
        {
            Dictionary<string, string> back_dictionary = new Dictionary<string, string>();
            for (int i = 0; i < dictionary_t_value.Count; i++)
            {
                back_dictionary.Add(dictionary_t_key[i], dictionary_t_value[i]);
            }

            return back_dictionary;
        }

        internal void Load(string path)
        {
            SimpleFunction.Json_Write(JsonUtility.ToJson(this, true), path);
        }
    }
}

namespace UserNamespace
{
   namespace CustomData
    {
        public struct Item
        {
            public int itemId;
            public int itemNum;
            public Item(int id, int num)
            {
                itemId = id;
                itemNum = num;
            }
        }
        public struct ItemData
        {
            public int ID;
            public string Name;
            public int Weight;
            public string Description;
           // public RawImage Picture;
        }
    }
    public static class UserFunction
    {
        public static int[] ToInt(string[] str)
        {
            var length = str.Length;
            var ret =new int[length];
            for (var i = 0; i < length; i++)
            {
                for (var k = 0; k < str[i].Length; k++)
                {
                    ret[i] = ret[i] * 10 + str[i][k] - '0';
                }
            }
            return ret;
        }
        public static int ToInt(string str)
        {
            var ret = 0;
            for (var k = 0; k < str.Length; k++)
            {
                ret = ret * 10 + str[k] - '0';
            }
            return ret;
        }
        public static void SerializeXmlWrite(object data, string path)
        {
            string xmlString;
            using (var sw = new StringWriter())
            {
                var xz = new XmlSerializer(data.GetType());
                var ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                xz.Serialize(sw, data, ns);
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
    }
}
internal class PlayerData
{
    internal static PlayerCondition player_condition = PlayerCondition.Stand;
    internal static float player_walk_speed = 2f;
    internal static float player_run_speed = 5f;
    internal static float Player_Hunger_Value = 0;
    internal static float Player_Temperature_Value = 36.5f;
    internal static float Player_Thirst_Value = 0;
    internal static float Player_Willpower_Value = 100;
    internal static float Player_Strength_Value = 100;


    internal static float player_hunger_speed = 10;
    internal static List<Item> BackUp = new List<Item>();
}

public class PublicVariables
{
    internal static Ray ray;
    internal static bool TabDown = false;
}

internal class BaseSetting
{
    internal static float MouseSensitivity = 200;
}

public class BaseFunction
{
    public static void StopOrContinu()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
}

internal class GameGlobalVariables
{
    internal static GameStatus game_status = GameStatus.Main;

    internal static GameCanvas game_canavs = GameCanvas.MainUI;

    internal static MouseStatus mouse_status = MouseStatus.Locked;
}

internal class SimpleFunction
{
    /// <summary>
    /// 将鼠标指针在Locked和Freedom切换
    /// </summary>
    /// <param name="destination">
    /// 切换的目的状态 
    /// </param>
    public static void Mouse_Point_Converter(MouseStatus destination)
    {
        if (destination == MouseStatus.Locked)
        {
            GameGlobalVariables.mouse_status = MouseStatus.Locked;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (destination == MouseStatus.Freedom)
        {
            GameGlobalVariables.mouse_status = MouseStatus.Freedom;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    /// <summary>
    /// 将Json文件写入文件中
    /// </summary>
    /// <param name="json">
    ///要写入的Json字段 
    /// </param>
    /// <param name="path">
    /// 目的文件地址
    /// </param>
    internal static void Json_Write(string json, string path)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(json);
            }
        }
        catch (Exception exceptione)
        {
            Debug.Log(exceptione.Message);
        }
    }

    /// <summary>
    /// 从文件中读出Json字段
    /// </summary>
    /// <param name="path">
    /// Json读取地址
    /// </param>
    /// <returns>
    /// 正常返回读取的Json，否则返回Null
    /// </returns>
    internal static string Json_Read(string path)
    {
        try
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string Json_Save;
                while ((Json_Save = sr.ReadToEnd()) != null)
                {
                    return Json_Save;
                }
            }
        }
        catch (Exception exception)
        {
            Debug.Log(exception.Message);
        }

        return null;
    }
}

internal class KeyboardManager
{
}

internal class GameComponent
{
}