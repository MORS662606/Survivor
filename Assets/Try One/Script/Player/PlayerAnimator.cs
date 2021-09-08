using System.Collections;
using System.Collections.Generic;
using Mors;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private Gun _gun;
    private bool isHide = true;

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _gun = GetComponentInChildren<Gun>();
    }

    void Update()
    {
        if (GameGlobalVariables.game_canavs == GameCanvas.MainUI)
        {
            var stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            
            //Move
            var walkId = Animator.StringToHash("Walk");
            var runId = Animator.StringToHash("Run");
            switch ( PlayerData.player_condition)
            {
                case PlayerCondition.Stand:
                    _animator.SetBool(walkId,false);
                    _animator.SetBool(runId,false);
                    break;
                case PlayerCondition.Walk:
                    _animator.SetBool(walkId,true);
                    _animator.SetBool(runId,false);
                    break;
                case PlayerCondition.Run:
                    _animator.SetBool(walkId,false);
                    _animator.SetBool(runId,true);
                    break;
            }

            //Aim
            var aimId = Animator.StringToHash("Aim");
            _animator.SetBool(aimId, Input.GetKey(KeyCode.Mouse1));


            //Shoot
            if (Input.GetKeyDown(KeyCode.Mouse0) && _gun.BulletUsing != 0)
            {
                if (stateInfo.IsName("Idle") || stateInfo.IsName("Walk"))
                {
                    _animator.Play("Shoot");
                }
                else if (stateInfo.IsName("Aim In") || stateInfo.IsName("Aim Fire Pose"))
                {
                   _animator.Play("Aim Shoot");
                }
            }
   

            var outId = Animator.StringToHash("Out Of Ammo");
            if (_gun.BulletUsing == 0)
            {
                _animator.SetBool(outId, true);
                _animator.SetLayerWeight(1, 1.0f);
            }
            else
            {
                _animator.SetLayerWeight(1, 0.0f);
            }


            //Reload
            if (Input.GetKeyDown(KeyCode.R) && _gun.BulletBackup != 0)
            {
                if (_gun.BulletUsing == 0)
                {
                    _animator.Play("Reload Out Of Ammo");
                }
                else if (_gun.BulletUsing != _gun.ammunitionCapacity)
                {
                    _animator.Play("Reload Ammo Left");
                }

                _gun.Reload();
            }

            //Inspect
            var inspectId = Animator.StringToHash("Inspect");
            if (Input.GetKeyDown(KeyCode.I) && !stateInfo.IsName("Inspect"))
            {
                _animator.SetTrigger(inspectId);
            }

            //Hide
            var hideId = Animator.StringToHash("Holster");
            if (Input.GetKeyDown(KeyCode.H))
            {
                isHide = !isHide;
                _animator.SetBool(hideId, isHide);
            }
        }
    }
}