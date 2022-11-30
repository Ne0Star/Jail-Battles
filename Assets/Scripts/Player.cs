using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using YG;

public class Player : Enemu
{

    [SerializeField] public Joystick controller;

    [SerializeField] private PlayerController playerController;
    //public PlayerController PlayerController { get => playerController; }



    private void Awake()
    {
        playerController = new Controller_1(this, ref moveSpeed, controller);
        YG.YandexGame.GetDataEvent += DataGet;
        //#if UNITY_ANDROID
        //        playerController = new Controller_1(this, moveSpeed, controller);
        //#elif UNITY_WEBGL
        //        playerController = new Controller_0(this, moveSpeed);
        //#endif

    }

    private void DataGet()
    {
        Gun r = (Gun)LevelManager.Instance.WeaponManager.GetWeaponByType(YandexGame.savesData.equpData.gun.weaponType);
        SetGun(r);
        Mele re = (Mele)LevelManager.Instance.WeaponManager.GetWeaponByType(YandexGame.savesData.equpData.mele.weaponType);
        SetMele(re);


        animator.Play("fightStance");
    }

    int index = 0;

    private void Update()
    {
        playerController.Update();
        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (index == 0)
            {
                playerController = new Controller_1(this, ref moveSpeed, controller);
            }
            if (index == 1)
            {
                playerController = new Controller_0(this, ref moveSpeed);
            }

            index++;
            if (index >= 2)
            {
                index = 0;
            }
        }
    }
}
