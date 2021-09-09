using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartridgeCase : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private AudioSource _audio;
    private bool _played = false;
    [SerializeField]
    internal AudioClip[] landingClips=new AudioClip[5];
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        LandingAudio(_played);
    }

    private void FixedUpdate()
    {
    }

    internal void CasePopUp()
    {
        _rigidbody.velocity=this.transform.right*3;
        Destroy(this.gameObject,10f);
    }

    private void LandingAudio(bool played)
    {
        if (played == true) return;
        var rangeSelect=UnityEngine.Random.Range(0, 5);
        _audio.clip=landingClips[rangeSelect];
        _audio.Play();
        _played = true;
    }
}
