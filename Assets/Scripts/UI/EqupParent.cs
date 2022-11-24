using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EqupParent : MonoBehaviour
{
    [SerializeField] private Image meleIco, gunIco, machineIco, missilesIco;
    public event System.Action onRemove;

    private void OnEnable()
    {
        DisableAll();
    }

    private void OnDisable()
    {
        DisableAll();
    }
    public void RemoveMissiles()
    {
        YG.YandexGame.savesData.missile = new YG.WeaponData();
        YG.YandexGame.SaveProgress();
        missilesIco.gameObject.SetActive(false);
        onRemove?.Invoke();
        onRemove = null;
    }
    public void RemoveMachine()
    {
        YG.YandexGame.savesData.machine = new YG.WeaponData();
        YG.YandexGame.SaveProgress();
        machineIco.gameObject.SetActive(false);
        onRemove?.Invoke();
        onRemove = null;
    }
    public void RemoveGun()
    {
        YG.YandexGame.savesData.gun = new YG.WeaponData();
        YG.YandexGame.SaveProgress();
        gunIco.gameObject.SetActive(false);
        onRemove?.Invoke();
        onRemove = null;
    }
    public void RemoveMele()
    {
        YG.YandexGame.savesData.mele = new YG.WeaponData();
        YG.YandexGame.SaveProgress();
        meleIco.gameObject.SetActive(false);
        onRemove?.Invoke();
        onRemove = null;
    }

    public void DisableAll()
    {
        meleIco.gameObject.SetActive(false);
        gunIco.gameObject.SetActive(false);
        machineIco.gameObject.SetActive(false);
        missilesIco.gameObject.SetActive(false);
    }

    public void EqupWeapon(WeaponItem item)
    {
        switch (item.Weapon.WeaponCategory)
        {
            case WeaponCategory.None:
                meleIco.gameObject.SetActive(true);
                meleIco.sprite = item.ShopIco;
                break;
            case WeaponCategory.����������_������:
                gunIco.gameObject.SetActive(true);
                gunIco.sprite = item.ShopIco;
                break;
            case WeaponCategory.����������_�������:
                machineIco.gameObject.SetActive(true);
                machineIco.sprite = item.ShopIco;
                break;
            case WeaponCategory.�������_���_����������:
                meleIco.gameObject.SetActive(true);
                meleIco.sprite = item.ShopIco;
                break;
            case WeaponCategory.�������_���_���������:
                meleIco.gameObject.SetActive(true);
                meleIco.sprite = item.ShopIco;
                break;
            case WeaponCategory.������_�����������:
                missilesIco.gameObject.SetActive(true);
                missilesIco.sprite = item.ShopIco;
                break;
        }
    }

}
