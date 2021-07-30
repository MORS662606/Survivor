using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mors;

public class PauseUI : MonoBehaviour
{
    public void Button_Back()
    {
        UIManager.pause_ui.gameObject.SetActive(false);
        UIManager.main_ui.gameObject.SetActive(true);
        GameGlobalVariables.game_canavs = GameCanvas.MainUI;
        SimpleFunction.Mouse_Point_Converter(MouseStatus.Locked);
        Time.timeScale = 1;
    }

    public void Button_Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Button_Setting() { }

}
