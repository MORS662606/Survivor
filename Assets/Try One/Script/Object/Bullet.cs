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

    internal void OutOfTheChamber(Vector3 speedDir)
    {
        var normalized = speedDir.normalized;
        _rigidbody.velocity=normalized*10;
        Destroy(this.gameObject,2f);

    } 
    
    
}
