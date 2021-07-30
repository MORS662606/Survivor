using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mors;

public class PlayerMove : MonoBehaviour
{
    public GameObject it;
    private CharacterController _characterController;
    private Text text;
    private GameObject _eyes, _body;
    private float _yRotation, _xRotation;
    public Vector3 test;

    void Start()
    {
        Get_Component();
    }
    void Update()
    {
        var varMove= GameGlobalVariables.game_status == GameStatus.Main &&
              GameGlobalVariables.mouse_status == MouseStatus.Locked;
        if (varMove)
        {
            Character_Move(Input.GetKey(KeyCode.LeftShift));
            Eyes_Move(!Input.GetKey(KeyCode.Tab));
        }
    }
    
    /// <summary>
    /// 组件获取函数
    /// </summary>
    private void Get_Component()
    {
        _characterController = GetComponent<CharacterController>();
        _eyes = GameObject.Find("Player/Body/Eyes");
        _body = GameObject.Find("Player");
    }

    /// <summary>
    /// 角色运动控制器(包括重力模拟
    /// </summary>
    /// <param name="meet_run_condition">
    ///Shift=真 
    /// </param>
    private void Character_Move(bool meet_run_condition)
    {
        //重力模拟
        if (!_characterController.isGrounded)
        {
            var varMotion = -transform.up * Time.deltaTime * 9.8f;
            _characterController.Move(varMotion);
            //return;
        }
        //移动模拟
        Vector3 finalSpeed = (Input.GetAxis("Horizontal") * transform.right + 
                              Input.GetAxis("Vertical") * transform.forward) *Time.deltaTime;
        test = finalSpeed;
        if (finalSpeed == Vector3.zero)
        {
            PlayerData.player_condition = PlayerCondition.Stand;
        }
        else if (meet_run_condition)
        {
            _characterController.Move(finalSpeed * PlayerData.player_run_speed);
            PlayerData.player_condition = PlayerCondition.Run;
        }
        else if (!meet_run_condition)
        {
            _characterController.Move(finalSpeed * PlayerData.player_walk_speed);
            PlayerData.player_condition = PlayerCondition.Walk;
        }
    }

    /// <summary>
    /// 角色视角转换器
    /// </summary>
    /// <param name="meet_move_condition">
    /// 主界面+鼠标锁定+tab未按下=真
    /// </param>
    private void Eyes_Move(bool meet_move_condition)
    {
        if (!meet_move_condition) return;
        var mouseX = Input.GetAxis("Mouse X") * BaseSetting.MouseSensitivity * Time.deltaTime;
        var mouseY = Input.GetAxis("Mouse Y") * BaseSetting.MouseSensitivity * Time.deltaTime;
        _yRotation -= mouseY;
        _yRotation = Mathf.Clamp(_yRotation, -50f, 50f);
        _xRotation += mouseX;
        //身体左右旋转
        _body.transform.rotation = Quaternion.Euler(0, _xRotation, 0);
        //头部同步左右旋转且上下点
        _eyes.transform.rotation = Quaternion.Euler(_yRotation, _xRotation, 0);
        //绘制射线
        PublicVariables.ray = new Ray(_eyes.transform.position, _eyes.transform.forward);
    }

}

