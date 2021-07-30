using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mors;

public class UIManager : MonoBehaviour
{
    public GameObject ui;
    [SerializeField] internal static Canvas main_ui;
    [SerializeField] internal static Canvas pause_ui;
    [SerializeField] internal static Canvas bag_ui;

    

    /// <summary>
    /// 加载每个画布
    /// </summary>
    internal void Load_All_Canvas()
    {
        ui = GameObject.Find("UI");
        main_ui = ui.transform.Find("Main UI").gameObject.GetComponent<Canvas>();
        pause_ui = ui.transform.Find("Pause UI").gameObject.GetComponent<Canvas>();
        bag_ui = ui.transform.Find("Bag UI").gameObject.GetComponent<Canvas>();
    }

    internal  void Change_Different_Canvas(GameCanvas now_game_canvas)
    {
        switch (now_game_canvas)
        {
            case GameCanvas.PauseUI:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    pause_ui.gameObject.SetActive(false);
                    main_ui.gameObject.SetActive(true);
                    GameGlobalVariables.game_canavs = GameCanvas.MainUI;
                    SimpleFunction.Mouse_Point_Converter(MouseStatus.Locked);
                    Time.timeScale = 1;
                }
                break;

            case GameCanvas.MainUI:
                if (Input.GetKeyDown(KeyCode.B))
                {
                    main_ui.gameObject.SetActive(false);
                    bag_ui.gameObject.SetActive(true);
                    GameGlobalVariables.game_canavs = GameCanvas.BagUI;
                    SimpleFunction.Mouse_Point_Converter(MouseStatus.Freedom);
                    Time.timeScale = 0;
                }
                else if (Input.GetKeyDown(KeyCode.Escape))
                {
                    main_ui.gameObject.SetActive(false);
                    pause_ui.gameObject.SetActive(true);
                    GameGlobalVariables.game_canavs = GameCanvas.PauseUI;
                    SimpleFunction.Mouse_Point_Converter(MouseStatus.Freedom);
                    Time.timeScale = 0;

                }
                break;

            case GameCanvas.BagUI:
                if (Input.GetKeyDown(KeyCode.Escape)|| Input.GetKeyDown(KeyCode.B))
                {
                    bag_ui.gameObject.SetActive(false);
                    main_ui.gameObject.SetActive(true);
                    GameGlobalVariables.game_canavs = GameCanvas.MainUI;
                    SimpleFunction.Mouse_Point_Converter(MouseStatus.Locked);
                    Time.timeScale = 1;
                }
                break;
            default:
                break;
        }
    }


    void Start()
    {
        Load_All_Canvas();
    }


    void Update()
    {
        Change_Different_Canvas(GameGlobalVariables.game_canavs);
    }
}
