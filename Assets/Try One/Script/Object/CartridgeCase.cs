using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartridgeCase : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    internal void CasePopUp()
    {
        Vector3 speedDir = Vector3.right;
        _rigidbody.velocity=speedDir.normalized;
        Destroy(this.gameObject,2f);
    }
}
