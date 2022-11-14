using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class WeaponUpdateManager : MonoBehaviour
{
    [SerializeField] private int batchCount = 20;

    [SerializeField] private Transform itemsParent;
    [SerializeField] private WeaponShopItemUpdate itemPrefab;
    private WeaponShopItemUpdate[] items;

    private void Awake()
    {
        items = new WeaponShopItemUpdate[batchCount];
        for (int i = 0; i < batchCount; i++)
        {
            WeaponShopItemUpdate item = Instantiate(itemPrefab, itemsParent);
            item.gameObject.SetActive(false);
            items[i] = item;
        }
    }


    public void SetItem(WeaponItem item, ref WeaponData data)
    {
        foreach (WeaponShopItemUpdate sp in items)
        {
            sp.gameObject.SetActive(false);
        }

        if (data.attackDistance.AllowUpdate)
        {
            WeaponShopItemUpdate shopItem = GetFreeItem();
            shopItem.gameObject.SetActive(true);
            shopItem.SetStat(item, ref data.attackDistance, "attackRange");
        }
        if (data.reloadSpeed.AllowUpdate)
        {
            WeaponShopItemUpdate shopItem = GetFreeItem();
            shopItem.gameObject.SetActive(true);
            shopItem.SetStat(item, ref data.reloadSpeed, "reloadSpeed");
        }
        if (data.attackSpeed.AllowUpdate)
        {
            WeaponShopItemUpdate shopItem = GetFreeItem();
            shopItem.gameObject.SetActive(true);
            shopItem.SetStat(item, ref data.attackSpeed, "attackSpeed");
        }
        if (data.attackDamage.AllowUpdate)
        {
            WeaponShopItemUpdate shopItem = GetFreeItem();
            shopItem.gameObject.SetActive(true);
            shopItem.SetStat(item, ref data.attackDamage, "attackDamage");
        }
        if (data.patronCount.AllowUpdate)
        {
            WeaponShopItemUpdate shopItem = GetFreeItem();
            shopItem.gameObject.SetActive(true);
            shopItem.SetStat(item, ref data.patronCount, "patronCount");
        }
        if (data.attackCount.AllowUpdate)
        {
            WeaponShopItemUpdate shopItem = GetFreeItem();
            shopItem.gameObject.SetActive(true);
            shopItem.SetStat(item, ref data.attackCount, "attackCount");
        }
        if (data.patronStorage.AllowUpdate)
        {
            WeaponShopItemUpdate shopItem = GetFreeItem();
            shopItem.gameObject.SetActive(true);
            shopItem.SetStat(item, ref data.patronStorage, "patronStorage");
        }
        if (data.shootingAccuracy.AllowUpdate)
        {
            WeaponShopItemUpdate shopItem = GetFreeItem();
            shopItem.gameObject.SetActive(true);
            shopItem.SetStat(item, ref data.shootingAccuracy, "accuracy");
        }
    }

    private WeaponShopItemUpdate GetFreeItem()
    {
        WeaponShopItemUpdate result = null;
        foreach (WeaponShopItemUpdate item in items)
        {
            if (!item.gameObject.activeSelf)
            {
                result = item;
                break;
            }
        }
        return result;
    }










    //    [SerializeField] private UIWeaponStatUpdate reloadSpeed;
    //    [SerializeField] private UIWeaponStatUpdate attackDamage;
    //    [SerializeField] private UIWeaponStatUpdate attackDistance;
    //    [SerializeField] private UIWeaponStatUpdate attackCount;



    //    [SerializeField] private Text categoryLabel;
    //    [SerializeField] private Text playerBalance;

    //    [SerializeField] private Button byuItemBTN;
    //    [SerializeField] private Button byuUpdateBTN;
    //    [SerializeField] private Button equpBTN;

    //    [SerializeField]
    //    private Text byuValue;

    //    [SerializeField] private Text attackSpeed_v, reloadSpeed_v, attackCount_v, attackDamage_v;

    //    [SerializeField] private RectTransform attackSpeed_vT, reloadSpeed_vT, attackCount_vT, attackDamage_vT, attackDistance_vT;

    //    [SerializeField] private WeaponShop shop;

    //    [SerializeField] private float startWidth;


    //    [SerializeField] GameObject updateBody, buyBody, equpBody, fullUpdate;

    //    private void Awake()
    //    {
    //        shop.onSelected += SetItem;

    //        startWidth = attackSpeed_vT.sizeDelta.x;

    //        byuItemBTN?.onClick?.AddListener(() =>
    //        {
    //            playerBalance.text = YG.YandexGame.savesData.money + " ";

    //            if (YG.YandexGame.savesData.money < selectedItem.Price)
    //            {
    //                Debug.Log("Недостаточно средств для покупки оружия");
    //                return;
    //            }

    //            ByuWeapon();
    //        });
    //        equpBTN?.onClick?.AddListener(() =>
    //        {
    //            Equp();
    //        });

    //        shop.onSwipeCategory += Cansel;

    //    }
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            YG.YandexGame.savesData.money += 1000;
            //GameManager.Instance.ScreenShoot();
        }
    }
#endif


    //    private void Equp()
    //    {

    //    }

    //    private void ByuWeapon()
    //    {
    //        Debug.Log("Попытка купить ");

    //        bool itsStackable = false;
    //        for (int i = 0; i < YG.YandexGame.savesData.byuWeapons.Length; i++)
    //        {
    //            WeaponPlayerData data = YG.YandexGame.savesData.byuWeapons[i];
    //            if (data.wapon == selectedItem.Weapon.WeaponType)
    //            {
    //                if (data.counts < selectedItem.MaxWeaponCount)
    //                {
    //                    data.counts++;
    //                    YG.YandexGame.savesData.byuWeapons[i] = data;
    //                    itsStackable = true;
    //                    break;
    //                }
    //            }
    //        }
    //        if (!itsStackable)
    //        {
    //            WeaponPlayerData[] lastData = YG.YandexGame.savesData.byuWeapons;
    //            List<WeaponPlayerData> newData = new List<WeaponPlayerData>();
    //            newData.AddRange(lastData);
    //            newData.Add(new WeaponPlayerData()
    //            {
    //                counts = 1,
    //                wapon = selectedItem.Weapon.WeaponType
    //            });
    //            YG.YandexGame.savesData.byuWeapons = newData.ToArray();
    //        }
    //        YandexGame.savesData.money = Mathf.Clamp(YandexGame.savesData.money - selectedItem.Price, 0, 99999);
    //        SetItem(selectedItem);
    //        YG.YandexGame.SaveProgress();
    //    }

    //    public void Cansel(WeaponCategory category)
    //    {
    //        if (category == WeaponCategory.None)
    //        {
    //            LocalizerData data = GameManager.Instance.GetValueByKey("weaponShop");
    //            categoryLabel.text = data.resultText;
    //            categoryLabel.font = data.resultFont;
    //        }
    //        else
    //        {
    //            LocalizerData data = GameManager.Instance.GetValueByKey(category.ToString());
    //            categoryLabel.text = data.resultText;
    //            categoryLabel.font = data.resultFont;
    //        }

    //        selectedItem = null;
    //        previewItem.gameObject.SetActive(false);
    //        equpBody.gameObject.SetActive(false);
    //        buyBody.gameObject.SetActive(false);
    //        updateBody.gameObject.SetActive(false);
    //        stackableBody.gameObject.SetActive(false);
    //        byuItemBTN.gameObject.SetActive(false);
    //        byuUpdateBTN.gameObject.SetActive(false);
    //        fullUpdate.gameObject.SetActive(false);
    //    }

    //    private void OnEnable()
    //    {
    //        if (!selectedItem) Cansel(WeaponCategory.None);
    //    }

    //    public void UpdateWeapon()
    //    {
    //        Debug.Log("Попытка улучшить ");
    //        for (int i = 0; i < YG.YandexGame.savesData.byuWeapons.Length; i++)
    //        {
    //            WeaponPlayerData data = YG.YandexGame.savesData.byuWeapons[i];
    //            if (data.wapon == selectedItem.Weapon.WeaponType)
    //            {

    //                data.attackDistanceUpdate = Mathf.Clamp(data.attackDistanceUpdate + 1, 0, selectedItem.Weapon.AttackDistance.MaxUpdateCount);
    //                data.attackDamageUpdate = Mathf.Clamp(data.attackDamageUpdate + 1, 0, selectedItem.Weapon.AttackDamage.MaxUpdateCount);
    //                data.attackCountUpdate = Mathf.Clamp(data.attackCountUpdate + 1, 0, selectedItem.Weapon.AttackCount.MaxUpdateCount);
    //                data.attackSpeedUpdate = Mathf.Clamp(data.attackSpeedUpdate + 1, 0, selectedItem.Weapon.AttackSpeed.MaxUpdateCount);
    //                data.reloadSpeedUpdate = Mathf.Clamp(data.reloadSpeedUpdate + 1, 0, selectedItem.Weapon.ReloadSpeed.MaxUpdateCount);

    //                YG.YandexGame.savesData.byuWeapons[i] = data;
    //                break;
    //            }
    //        }
    //        SetItem(selectedItem);
    //        YG.YandexGame.SaveProgress();
    //    }

    //    [SerializeField] private WeaponItem selectedItem;
    //    [SerializeField] private Text stackableValue, stackableMaxValue;
    //    [SerializeField] private GameObject stackableBody;

    //    private void CheckFullUpdate(WeaponPlayerData currentData)
    //    {



    //        if (currentData.attackCountUpdate == selectedItem.MaxWeaponCount)
    //        {
    //            updateBody.gameObject.SetActive(false);
    //            byuUpdateBTN.gameObject.SetActive(false);
    //            fullUpdate.gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            updateBody.gameObject.SetActive(true);
    //            byuUpdateBTN.gameObject.SetActive(true);
    //            fullUpdate.gameObject.SetActive(false);
    //        }
    //    }
    //    [SerializeField] private Image weaponIco;
    //    [SerializeField] private GameObject previewItem;
    //    private void SetItem(WeaponItem item)
    //    {
    //        selectedItem = item;
    //        previewItem.gameObject.SetActive(true);
    //        weaponIco.sprite = item.ShopIco;
    //        bool isByu = false;
    //        WeaponPlayerData currentData = new WeaponPlayerData();
    //        foreach (WeaponPlayerData data in YG.YandexGame.savesData.byuWeapons)
    //        {
    //            if (data.wapon == item.Weapon.WeaponType)
    //            {
    //                currentData = data;
    //                //item.Weapon.SetUpdate(data.updateCount);
    //                isByu = true;
    //                break;
    //            }
    //        }
    //        bool stackable = item.MaxWeaponCount > 1;
    //        byuValue.text = selectedItem.Price + " ";
    //        //updateValue.text = (selectedItem.Price * selectedItem.Weapon.CurrentUpdateCount) + " ";
    //        playerBalance.text = YG.YandexGame.savesData.money + " ";
    //        fullUpdate.gameObject.SetActive(false);
    //        if (isByu && stackable) // Куплен, стакается
    //        {
    //            buyBody.gameObject.SetActive(false);
    //            updateBody.gameObject.SetActive(true);
    //            stackableBody.gameObject.SetActive(true);
    //            equpBody.SetActive(true);
    //            stackableValue.text = currentData.counts + " ";
    //            stackableMaxValue.text = item.MaxWeaponCount + " ";
    //            byuUpdateBTN.gameObject.SetActive(true);
    //            CheckFullUpdate(currentData);
    //            if (currentData.counts < item.MaxWeaponCount) // Куплено не максимальное количество
    //            {
    //                byuItemBTN.gameObject.SetActive(true);
    //            }
    //            else // Куплено максимальное количество
    //            {
    //                byuItemBTN.gameObject.SetActive(false);
    //            }
    //            VisibilityUpdateItem(selectedItem);
    //        }
    //        else if (isByu && !stackable) // Куплен нестакается
    //        {
    //            buyBody.gameObject.SetActive(false);
    //            CheckFullUpdate(currentData);
    //            stackableBody.gameObject.SetActive(false);
    //            equpBody.SetActive(true);
    //            byuItemBTN.gameObject.SetActive(false);
    //            VisibilityUpdateItem(selectedItem);
    //        }
    //        else if (!isByu && stackable) // Некуплен, стакается
    //        {
    //            stackableBody.gameObject.SetActive(true);
    //            byuItemBTN.gameObject.SetActive(true);
    //            byuUpdateBTN.gameObject.SetActive(false);
    //        }
    //        else if (!isByu && !stackable)// Некуплен нестакается
    //        {
    //            buyBody.gameObject.SetActive(true);
    //            updateBody.gameObject.SetActive(false);
    //            stackableBody.gameObject.SetActive(false);
    //            equpBody.SetActive(false);
    //            byuItemBTN.gameObject.SetActive(true);
    //            byuUpdateBTN.gameObject.SetActive(false);
    //        }
    //    }

    //    private void VisibilityUpdateItem(WeaponItem item)
    //    {
    //        Set(attackSpeed_vT, item.Weapon.AttackSpeed.Value, item.Weapon.AttackSpeed.MaxValue);
    //        Set(attackCount_vT, item.Weapon.AttackCount.Value, item.Weapon.AttackCount.MaxValue);
    //        Set(attackDamage_vT, item.Weapon.AttackDamage.Value, item.Weapon.AttackDamage.MaxValue);
    //        Set(reloadSpeed_vT, item.Weapon.ReloadSpeed.Value, item.Weapon.ReloadSpeed.MaxValue);
    //        Set(attackDistance_vT, item.Weapon.AttackDistance.Value, item.Weapon.AttackDistance.MaxValue);
    //    }

    //    private void Set(RectTransform target, float value, float maxValue)
    //    {
    //        float precent = value * 100 / maxValue;
    //        float coof = startWidth / 100f;
    //        float totalWidth = coof * precent;
    //        target.sizeDelta = new Vector2(totalWidth, target.sizeDelta.y);
    //    }
}
