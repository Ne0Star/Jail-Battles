using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class EqupParent : MonoBehaviour
{
    [SerializeField] private Button unEqupAll;
    [SerializeField] private Image meleIco, gunIco, machineIco, missilesIco;
    public event System.Action onRemove;

    private void OnEnable()
    {
        unEqupAll.onClick.RemoveAllListeners();
        unEqupAll.onClick.AddListener(() =>
        {
            RemoveMachine();
            RemoveGun();
            RemoveMele();
            RemoveMissiles();
        });
    }
    private void OnDisable()
    {
        Save();
        DisableAll();
    }

    public void Save()
    {
        YG.YandexGame.savesData.equpData = this.equpData;
        YG.YandexGame.SaveProgress();
    }

    public void RemoveMissiles()
    {
        YG.YandexGame.savesData.equpData.missile = new YG.WeaponData();
        YG.YandexGame.SaveProgress();
        equpData.missile = new WeaponData();
        missilesIco.gameObject.SetActive(false);
        onRemove?.Invoke();
        onRemove = null;
    }
    public void RemoveMachine()
    {
        YG.YandexGame.savesData.equpData.machine = new YG.WeaponData();
        YG.YandexGame.SaveProgress();
        equpData.machine = new WeaponData();
        machineIco.gameObject.SetActive(false);
        onRemove?.Invoke();
        onRemove = null;
    }
    public void RemoveGun()
    {
        YG.YandexGame.savesData.equpData.gun = new YG.WeaponData();
        YG.YandexGame.SaveProgress();
        equpData.gun = new WeaponData();
        gunIco.gameObject.SetActive(false);
        onRemove?.Invoke();
        onRemove = null;
    }
    public void RemoveMele()
    {
        YG.YandexGame.savesData.equpData.mele = new YG.WeaponData();
        YG.YandexGame.SaveProgress();
        meleIco.gameObject.SetActive(false);
        equpData.mele = new YG.WeaponData();
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

    [SerializeField] private YG.PlayerEqupData equpData;

    public void EqupWeapon(WeaponItem item, WeaponData data)
    {
        switch (item.Weapon.WeaponCategory)
        {
            case WeaponCategory.None:
                meleIco.gameObject.SetActive(true);
                meleIco.sprite = item.ShopIco;
                equpData.mele = data;
                break;
            case WeaponCategory.Стрелковое_Легкое:
                gunIco.gameObject.SetActive(true);
                gunIco.sprite = item.ShopIco;
                equpData.gun = data;
                break;
            case WeaponCategory.Стрелковое_Тяжелое:
                machineIco.gameObject.SetActive(true);
                machineIco.sprite = item.ShopIco;
                equpData.gun = data;
                break;
            case WeaponCategory.Ближний_Бой_Одноручное:
                meleIco.gameObject.SetActive(true);
                meleIco.sprite = item.ShopIco;
                equpData.mele = data;
                break;
            case WeaponCategory.Ближний_Бой_Двуручное:
                meleIco.gameObject.SetActive(true);
                meleIco.sprite = item.ShopIco;
                equpData.mele = data;
                break;
            case WeaponCategory.Только_Метательное:
                missilesIco.gameObject.SetActive(true);
                missilesIco.sprite = item.ShopIco;
                equpData.missile = data;
                break;
        }
    }

}
