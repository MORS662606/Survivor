using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        
    }

    internal void Popup(Vector3 speedDir)
    {
        _rigidbody.velocity=speedDir.normalized*100;
        Destroy(this.gameObject,2f);
    } 
    
    
}
