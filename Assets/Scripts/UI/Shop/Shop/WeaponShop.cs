using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class WeaponShop : Shop
{


    [SerializeField] private Vector3 lastPostion;
    [SerializeField] private Quaternion lastRotation;
    [SerializeField] private Quaternion playerRotation;
    [SerializeField] private Vector3 playerPosition;
    [SerializeField] private Transform player;


    [SerializeField] private GameObject notByuParent, fullUpdateParent, equpParent;

    [SerializeField] private GameObject mainView;
    [SerializeField] private Text categoryLabel;

    [SerializeField] private WeaponUpdateData weaponUpdateData;
    [SerializeField] private WeaponItemData weaponView;
    [SerializeField] private PlayerItemData playerView;
    [SerializeField] private ByuData byuData;

    [SerializeField] private WeaponUpdateManager updateManger;

    [System.Serializable]
    public struct WeaponUpdateData
    {
        public GameObject parent;
    }

    [System.Serializable]
    public struct ByuData
    {
        public Color onByu, offByu;
        public Image view;
        public Button btn;
    }

    [System.Serializable]
    public struct PlayerItemData
    {
        public Text balance;
    }
    [System.Serializable]
    public struct WeaponItemData
    {
        public Image ico;
        public Text name, price;

        public Text count, maxCount;

        public RectTransform ratingView;
    }

    public event System.Action<WeaponItem> onSelected;
    public event System.Action<WeaponCategory> onSwipeCategory;
    [SerializeField] private Transform listParent;
    [SerializeField] private WeaponShopItem itemPrefab;
    [SerializeField] private List<WeaponItem> shopStorage = new List<WeaponItem>();
    [SerializeField] private List<WeaponShopItem> allItems = new List<WeaponShopItem>();
    [SerializeField] private List<WeaponSlot> slots;
    private float startWidth;
    private void Awake()
    {
        for (int i = 0; i < batchCount; i++)
        {
            CreateItem();
        }
        updateManger = GetComponentInChildren<WeaponUpdateManager>(true);
        startWidth = weaponView.ratingView.GetWidth();
        mainView.SetActive(false);
        YandexGame.GetDataEvent += UpdateBalance;
    }

    private void UpdateBalance()
    {
        playerView.balance.text = YandexGame.savesData.money + " ";
    }


    private void OnEnable()
    {
        lastRotation = player.transform.rotation;
        lastPostion = player.transform.position;

        player.transform.position = playerPosition;
        player.transform.rotation = playerRotation;
    }


    private void OnDisable()
    {
        player.transform.position = lastPostion;
        player.transform.rotation = lastRotation;
        mainView.SetActive(false);
    }

    private void Set(RectTransform target, float value, float maxValue)
    {
        float precent = value * 100 / maxValue;
        float coof = startWidth / 100f;
        float totalWidth = coof * precent;
        target.SetWidth(Mathf.Clamp(totalWidth,0f, startWidth));
    }

    private void GetRating(WeaponItem item)
    {
        float weaponValue = float.MaxValue;
        float maxWeaponValue = 0f;
        foreach (WeaponData m in GameManager.Instance.DefaultWeaponDatas)
        {
            float value = m.attackDamage.Value + m.shootingAccuracy.Value + m.attackCount.Value + m.patronCount.Value + m.reloadSpeed.Value + m.patronCount.Value;
            if (m.weaponType == item.Weapon.WeaponType)
            {
                weaponValue = value;
            }
            else
            {
                if (maxWeaponValue < value)
                {
                    maxWeaponValue = value;
                }
            }
        }
        Set(weaponView.ratingView, weaponValue, maxWeaponValue);
    }

    private void SetWeaponView(WeaponItem item, WeaponData weaponData, LocalizerData textData)
    {
        weaponView.ico.sprite = item.ShopIco;
        weaponView.name.text = item.UseTranslator ? textData.resultText : item.Weapon.name;
        weaponView.name.font = textData.resultFont;
        weaponView.price.text = item.Price + " ";
        weaponView.price.font = textData.resultFont;
        weaponView.count.text = weaponData.weaponCount + " ";
        weaponView.count.font = textData.resultFont;
        weaponView.maxCount.text = item.MaxWeaponCount + " ";
        weaponView.maxCount.font = textData.resultFont;
        playerView.balance.text = YandexGame.savesData.money + " ";
        playerView.balance.font = textData.resultFont;
    }


    private void SetPreviewItem(WeaponItem item)
    {
        WeaponData weaponData = new WeaponData();
        LocalizerData textData = GameManager.Instance.GetValueByKey(item.NameKey);
        bool isByu = false, stackable = item.MaxWeaponCount > 1 ? true : false;
        mainView.SetActive(true);
        foreach (WeaponData m in YandexGame.savesData.byuWeapons)
        {
            if (m.weaponType == item.Weapon.WeaponType)
            {
                weaponData = m;
                isByu = true;
                break;
            }
        }
        SetWeaponView(item, weaponData, textData);
        byuData.btn.onClick?.RemoveAllListeners();
        byuData.btn.onClick?.AddListener(() =>
        {
            if (YandexGame.savesData.money - item.Price >= 0)
            {
                //#region Получение данных
                //WeaponData data = new WeaponData();
                //bool isByu = false;
                //foreach (WeaponData d in YandexGame.savesData.byuWeapons)
                //{
                //    if (d.weaponType == item.Weapon.WeaponType)
                //    {
                //        isByu = true;
                //        data = d;
                //        break;
                //    }
                //}
                //#endregion
                if (isByu) // Если куплен, добавляем к максимальному количеству
                {
                    for (int i = 0; i < YandexGame.savesData.byuWeapons.Length; i++)
                    {
                        if (YandexGame.savesData.byuWeapons[i].weaponType == item.Weapon.WeaponType)
                        {
                            Debug.Log("Дамн дамн");
                            YandexGame.savesData.byuWeapons[i].weaponCount = Mathf.Clamp(YandexGame.savesData.byuWeapons[i].weaponCount + 1, 0, item.MaxWeaponCount);
                        }
                    }
                    YandexGame.SaveProgress();
                }
                else // Некуплен
                {
                    List<WeaponData> wdl = new List<WeaponData>();
                    wdl.AddRange(YandexGame.savesData.byuWeapons);

                    WeaponData wd = GameManager.Instance.GetDefaultDataByType(item.Weapon.WeaponType);
                    wd.weaponCount = Mathf.Clamp(wd.weaponCount + 1, 0, item.MaxWeaponCount);
                    wdl.Add(wd);

                    YandexGame.savesData.byuWeapons = wdl.ToArray();
                    YandexGame.SaveProgress();
                }
                SetPreviewItem(item);
            }
            else
            {
                Debug.Log("Недостаточно средств для покупки");
            }
        });
        if (YandexGame.savesData.money - item.Price >= 0)
        {
            byuData.view.color = byuData.onByu;
        }
        else
        {
            byuData.view.color = byuData.offByu;
        }
        if (isByu && stackable) // Куплен, стакается
        {
            if (item.MaxWeaponCount == weaponData.weaponCount)
            {
                byuData.btn.gameObject.SetActive(false);
            }
            else
            {
                byuData.btn.gameObject.SetActive(true);
            }
            notByuParent.SetActive(false);
            weaponUpdateData.parent.SetActive(true);
            equpParent.SetActive(true);
            updateManger.SetItem(item, ref weaponData);
        }
        else if (isByu && !stackable) // Куплен нестакается
        {
            byuData.btn.gameObject.SetActive(false);
            notByuParent.SetActive(false);
            weaponUpdateData.parent.SetActive(true);
            equpParent.SetActive(true);
            updateManger.SetItem(item, ref weaponData);
        }
        else if (!isByu && stackable) // Некуплен, стакается
        {
            byuData.btn.gameObject.SetActive(true);
            notByuParent.SetActive(true);
            weaponUpdateData.parent.SetActive(false);
            equpParent.SetActive(false);
        }
        else if (!isByu && !stackable)// Некуплен нестакается
        {
            byuData.btn.gameObject.SetActive(true);
            notByuParent.SetActive(true);
            weaponUpdateData.parent.SetActive(false);
            equpParent.SetActive(false);
        }
        GetRating(item);
        onSelected?.Invoke(item);
    }

    public void SetCategory(int categoryIndex)
    {
        RemoveAllItem();
        WeaponCategory category = (WeaponCategory)categoryIndex;
        foreach (WeaponItem item in shopStorage)
        {
            if (item.Weapon.WeaponCategory == category)
            {
                AddItem(item);
            }
        }
        if (category == WeaponCategory.None)
        {
            LocalizerData data = GameManager.Instance.GetValueByKey("weaponShop");
            categoryLabel.text = data.resultText;
            categoryLabel.font = data.resultFont;
        }
        else
        {
            LocalizerData data = GameManager.Instance.GetValueByKey(category.ToString());
            categoryLabel.text = data.resultText;
            categoryLabel.font = data.resultFont;


        }
        playerView.balance.text = YandexGame.savesData.money + " ";
        onSwipeCategory?.Invoke(category);
    }

    private void AddItem(WeaponItem itemType)
    {
        WeaponShopItem item = GetFreeItem();
        item.SetItem(itemType);
        item.gameObject.SetActive(true);
        item.onSelect += SetPreviewItem;
    }

    public void RemoveAllItem()
    {
        foreach (WeaponShopItem item in allItems)
        {
            if (!item.gameObject.activeSelf) return;
            item.gameObject.SetActive(false);
        }
    }

    private WeaponShopItem GetFreeItem()
    {
        foreach (WeaponShopItem item in allItems)
        {
            if (!item.gameObject.activeSelf)
            {
                return item;
            }
        }
        return null;
    }


    private ShopItem CreateItem()
    {
        WeaponShopItem item = null;
        if (itemPrefab)
        {
            item = Instantiate(itemPrefab, listParent);
            item.gameObject.SetActive(false);
            allItems.Add(item);
        }
        else
        {
            GameObject go = new GameObject();
            go.transform.parent = listParent;
            go.SetActive(false);
            item = go.AddComponent<WeaponShopItem>();
            allItems.Add(item);
        }
        return item;
    }

    private void Start()
    {
        SetCategory(1);
    }

}
