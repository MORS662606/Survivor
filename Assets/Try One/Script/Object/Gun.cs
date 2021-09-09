using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    internal ushort ammunitionCapacity = 9;
    internal int BulletBackup = 20;
    internal int BulletUsing = 9;
    public Text bulletNum;
    public Transform muzzle;
    public Transform start;
    public Transform startAim;
    public Transform casePop;
    public GameObject bullet;
    public GameObject  cartridge_case;

    private void FixedUpdate()
    {
        bulletNum.text = BulletUsing.ToString() + "/" + BulletBackup.ToString();

        // Debug.DrawRay(start.position,muzzle.position-start.position,Color.cyan);//绘制射线
        //  Debug.DrawRay(start.position,muzzle.position-start_aim.position,Color.cyan);//绘制射线
    }

    public void AimShoot()
    {
        if (BulletUsing == 0) return;
        BulletUsing--;
        //子弹射出
        var varBullet = (GameObject)Instantiate(bullet, muzzle.position, muzzle.rotation);
        varBullet.GetComponent<Bullet>().Popup(muzzle.position - startAim.position);
        //弹壳弹出
        var varCase = (GameObject)Instantiate(cartridge_case, casePop.position, casePop.rotation);
        varCase.GetComponent<CartridgeCase>().CasePopUp();
    }

    public void Shoot()
    {
        if (BulletUsing == 0) return;
       BulletUsing--;
       //子弹射出
        var varBullet = (GameObject)Instantiate(bullet, muzzle.position, muzzle.rotation);
        var speedDir = muzzle.position - start.position;
        var stochastic = UnityEngine.Random.rotation.eulerAngles;
        var offset =UnityEngine.Random.Range(-2f,2f)*
            UnityEngine.Vector3.Cross(stochastic, speedDir).normalized/100;
        varBullet.GetComponent<Bullet>().Popup(speedDir-offset);
        //弹壳弹出
        var varCase = (GameObject)Instantiate(cartridge_case, casePop.position, casePop.rotation);
        varCase.GetComponent<CartridgeCase>().CasePopUp();
    }

    internal void Reload()
    {
        if (BulletBackup > ammunitionCapacity)
        {
            var allBullet = BulletBackup + BulletUsing;
            BulletUsing = ammunitionCapacity;
            BulletBackup = allBullet - ammunitionCapacity;
        }
        else
        {
            BulletUsing = BulletBackup;
            BulletBackup = 0;
        }
    }
    
    //共用代码块

  
}