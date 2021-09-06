using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    internal ushort ammunitionCapacity = 9;
    internal int BulletBackup=20;
    internal int BulletUsing=9;
    public Text bulletNum;
    public Transform muzzle ;
    public Transform test;
    public GameObject bullet;
    private void FixedUpdate()
    {
        bulletNum.text = BulletUsing.ToString() + "/" + BulletBackup.ToString();
        
        Debug.DrawRay(test.position,muzzle.position-test.position,Color.cyan);//绘制射线

    }

    public void AimShoot()
    {
        if (BulletUsing != 0)
        {
            BulletUsing--;
            GameObject varBullet = (GameObject)Instantiate(bullet,muzzle.position,muzzle.rotation);
            varBullet.GetComponent<Bullet>().OutOfTheChamber(muzzle.position-test.position);
        }
    }

    public void Shoot()
    {
        if (BulletUsing != 0)
        {
            BulletUsing--;
            GameObject varBullet = (GameObject)Instantiate(bullet,muzzle.position,muzzle.rotation);
            varBullet.GetComponent<Bullet>().OutOfTheChamber(muzzle.position-test.position);
        }
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
            BulletUsing = BulletBackup ;
            BulletBackup = 0;
        }
    }
}
