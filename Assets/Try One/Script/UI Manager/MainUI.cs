using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mors;

public class MainUI : MonoBehaviour
{
    private Sprite[] pie_menu_sprite = new Sprite[7];
    private Image pie_menu_image;
    private float mouse_x, mouse_y;
    [SerializeField] private byte pie_menu_area;
    private Slider[] value_ui = new Slider[4];

    /// <summary>
    /// 加载放射菜单的精灵
    /// </summary>
    private void Load_Pie_Menu_Sprite()
    {
        for (int i = 0; i < 7; i++)
        {
            pie_menu_sprite[i] = Resources.Load<Sprite>("Material/temp" + (i + 1).ToString());
        }
        pie_menu_image = GameObject.Find("UI/Main UI/Pie Menu").GetComponent<Image>();
    }

    /// <summary>
    /// 根据条件显示放射菜单
    /// </summary>
    private void Show_Pie_Menu()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            Time.timeScale = 0.1f;
            SimpleFunction.Mouse_Point_Converter(MouseStatus.Freedom);
            pie_menu_image.color = new Color(255, 255, 255, 255);
            mouse_x = (int)(Input.mousePosition.x - Screen.width / 2f);
            mouse_y = (int)(Input.mousePosition.y - Screen.height / 2f);
            if (Mathf.Abs(mouse_x) >= 80 || Mathf.Abs(mouse_y) >= 80)
            {
                pie_menu_area = (byte)((180 + (180 * Mathf.Atan2(mouse_y, mouse_x) / Mathf.PI)) / 60);
                pie_menu_image.sprite = pie_menu_sprite[pie_menu_area % 6];
            }
            else
            {
                pie_menu_image.sprite = pie_menu_sprite[6];
            }

        }
        else if(Input.GetKeyUp(KeyCode.Tab))
        {
            Time.timeScale = 1;
            pie_menu_image.color = new Color(255, 255, 255, 0);
            SimpleFunction.Mouse_Point_Converter(MouseStatus.Locked);
        }
    }

    private void Select_Pie_Menu(bool meet_condition)
    {
        if (!meet_condition) return;

        switch (pie_menu_area)
        {

            case 0:
                break;
            case 1:
                UIManager.main_ui.gameObject.SetActive(false);
                UIManager.bag_ui.gameObject.SetActive(true);
                GameGlobalVariables.game_canavs = GameCanvas.BagUI;
                SimpleFunction.Mouse_Point_Converter(MouseStatus.Freedom);
                Time.timeScale = 0;
                break;
            case 2: 
                break;
            case 3:
                break;
            case 4: 
                break;
            case 5: 
                break;
            default:
                break;
        }
        Debug.Log(pie_menu_area);
    }


   
    /// <summary>
    /// 初始化角色状态UI组
    /// </summary>
    /// <param name="Player_Value_UI">
    /// 角色状态UI组父组件
    /// </param>
    private void Player_Value_Construction(GameObject Player_Value_UI)
    {
        value_ui = Player_Value_UI.GetComponentsInChildren<Slider>();
    }
    /// <summary>
    /// 更新角色状态数值
    /// </summary>
    private void Player_Value_Update()
    {
        value_ui[0].value = PlayerData.Player_Hunger_Value / 100;
        value_ui[1].value = PlayerData.Player_Thirst_Value / 100;
        value_ui[2].value = PlayerData.Player_Willpower_Value / 100;
        value_ui[3].value = PlayerData.Player_Strength_Value / 100;
    }




    private void Start()
    {
        Player_Value_Construction(GameObject.Find("UI/Main UI/Player_Value"));
        Load_Pie_Menu_Sprite();

        Time.timeScale = 1;
        pie_menu_image.color = new Color(255, 255, 255, 0);
        SimpleFunction.Mouse_Point_Converter(MouseStatus.Locked);
    }

    private void Update()
    {
        Player_Value_Update();
        if (GameGlobalVariables.game_status == GameStatus.Main)
        {
            Show_Pie_Menu();
            Select_Pie_Menu(Input.GetKeyUp(KeyCode.Tab));
        }
    }
}
