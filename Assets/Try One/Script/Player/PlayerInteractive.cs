using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractive : MonoBehaviour
{
    private PlayerBag _playerBag;
    
    private void Awake()
    {
        _playerBag = GetComponentInChildren<PlayerBag>();
    }

    void Update()
    {
        InteractiveClick(PublicVariables.ray, Input.GetKeyDown(KeyCode.F));
    }

    private void InteractiveClick(Ray ray, bool clickButton)
    {
        if (!clickButton) return;
        _playerBag.Pickup();
       
    }
}
