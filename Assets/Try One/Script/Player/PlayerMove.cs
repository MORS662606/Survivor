using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mors;

public class PlayerMove : MonoBehaviour
{
    private CharacterController _characterController;
    private Text text;
    public GameObject mainCamera;
    public float _yRotation, _xRotation;
    public Vector3 test;
    private Animator _animator;
    private Transform _cameraTransform;
    public float Axize;
    
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Awake()
    {
         _cameraTransform = mainCamera.transform;
    }

    void Update()
    {
        var varMove= GameGlobalVariables.game_status == GameStatus.Main &&
              GameGlobalVariables.mouse_status == MouseStatus.Locked;
        if (varMove)
        {
            Character_Move(Input.GetKey(KeyCode.LeftShift));
            Camera_Rotation(!Input.GetKey(KeyCode.Tab));
        }
        
        var varReadyFire = Input.GetKey(KeyCode.Mouse1);
        var aimId = Animator.StringToHash("Aim");
        _animator.SetBool(aimId,varReadyFire);
        var runId = Animator.StringToHash("Run");
        _animator.SetBool(runId,Input.GetKey(KeyCode.LeftShift));

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
        Vector3 moveDirection = Vector3.Normalize(Input.GetAxisRaw("Horizontal") * transform.right +
                                                 Input.GetAxisRaw("Vertical") * transform.forward)*Time.deltaTime;
        test = moveDirection;
        if (moveDirection == Vector3.zero)
        {
            PlayerData.player_condition = PlayerCondition.Stand;
        }
        else if (meet_run_condition)
        {
            var moveVector =  PlayerData.player_run_speed *moveDirection;
            _characterController.Move(moveVector);
            PlayerData.player_condition = PlayerCondition.Run;
        }
        else
        {            
            var moveVector =  PlayerData.player_walk_speed *moveDirection;
            _characterController.Move(moveDirection * PlayerData.player_walk_speed);
            PlayerData.player_condition = PlayerCondition.Walk;
        }
    }

    /// <summary>
    /// 角色视角转换器
    /// </summary>
    /// <param name="meet_move_condition">
    /// 主界面+鼠标锁定+tab未按下=真
    /// </param>
    private void Camera_Rotation(bool meet_move_condition)
    {
        if (!meet_move_condition) return;
        var mouseX = Input.GetAxis("Mouse X") * BaseSetting.MouseSensitivity * Time.deltaTime;
        var mouseY = Input.GetAxis("Mouse Y") * BaseSetting.MouseSensitivity * Time.deltaTime;
        _yRotation -= mouseY;
        _yRotation = Mathf.Clamp(_yRotation, -50f, 50f);
        _xRotation += mouseX;
        //身体左右旋转
        this.transform.rotation = Quaternion.Euler(0, _xRotation, 0);
        //Quaternion targetDir = Quaternion.Euler(0, _xRotation, 0);
        //this.transform.rotation = Quaternion.Lerp(transform.rotation, targetDir, 0);
        //头部同步左右旋转且上下点
        mainCamera.transform.rotation = Quaternion.Euler(_yRotation, _xRotation, 0);
        //绘制射线
        PublicVariables.ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
    }


    private void InputCheck()
    {
    }
}

