using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class WeaponShop : Shop
{
    [SerializeField] private EqupParent equpParent;

    [SerializeField] private Vector3 lastPostion;
    [SerializeField] private Quaternion lastRotation;
    [SerializeField] private Quaternion playerRotation;
    [SerializeField] private Vector3 playerPosition;
    [SerializeField] private Transform player;


    [SerializeField] private GameObject notByuParent, fullUpdateParent;
    [SerializeField] private Button equpBtn;
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


        if (equpParent.gameObject.activeSelf)
        {
            SetEqupItems();
        }

    }

    private void SetEqupItems()
    {
        foreach (WeaponItem item in shopStorage)
        {
            if (item.Weapon.WeaponType == YandexGame.savesData.mele.weaponType || item.Weapon.WeaponType == YandexGame.savesData.gun.weaponType ||
                YandexGame.savesData.machine.weaponType == item.Weapon.WeaponType || YandexGame.savesData.missile.weaponType == item.Weapon.WeaponType)
            {
                equpParent.EqupWeapon(item);
            }
        }
    }


    private void OnDisable()
    {
        player.transform.position = lastPostion;
        player.transform.rotation = lastRotation;
        mainView.SetActive(false);
    }

    private void Set(RectTransform target, float value, float maxValue, float minValue)
    {
        float coof = Mathf.InverseLerp(minValue, maxValue, value);
        float total = Mathf.Lerp(value, startWidth, coof);
        target.SetWidth(total);
    }

    private void GetRating(WeaponItem item)
    {
        float weaponValue = float.MaxValue;
        float maxWeaponValue = 0f;
        float minWeaponValue = 999f;
        foreach (WeaponData m in GameManager.Instance.PlayerWeaponData)
        {
            float value = m.attackDamage.Value;
            if (m.weaponType == item.Weapon.WeaponType)
            {
                weaponValue = value;
            }
            if (value > maxWeaponValue)
            {
                maxWeaponValue = value;
            }
            if (value < minWeaponValue)
            {
                minWeaponValue = value;
            }
        }
        Debug.Log(" value = " + weaponValue + " minValue = " + minWeaponValue + " maxValue = " + maxWeaponValue);
        Set(weaponView.ratingView, weaponValue, maxWeaponValue, minWeaponValue);
    }

    private void SetWeaponView(WeaponItem item, WeaponData weaponData, LocalizerData textData)
    {
        weaponView.ico.sprite = item.ShopIco;
        weaponView.name.text = item.UseTranslator ? textData.resultText : item.Weapon.name;
        weaponView.name.font = textData.resultFont;
        weaponView.price.text = weaponData.price + " ";
        weaponView.price.font = textData.resultFont;
        weaponView.count.text = weaponData.weaponCount + " ";
        weaponView.count.font = textData.resultFont;
        weaponView.maxCount.text = weaponData.maxWeaponCount + " ";
        weaponView.maxCount.font = textData.resultFont;
        playerView.balance.text = YandexGame.savesData.money + " ";
        playerView.balance.font = textData.resultFont;
    }

    private WeaponItem currentItem;

    private void temp()
    {
        SetPreviewItem(currentItem);
        equpParent.onRemove -= temp;
    }

    private void SetPreviewItem(WeaponItem item)
    {
        WeaponData weaponData = new WeaponData();
        LocalizerData textData = GameManager.Instance.GetValueByKey(item.NameKey);
        currentItem = item;
        equpParent.onRemove += temp;

        bool isByu = false, stackable = weaponData.maxWeaponCount > 1 ? true : false;
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
            if (YandexGame.savesData.money - weaponData.price >= 0)
            {
                if (isByu) // Если куплен, добавляем к максимальному количеству
                {
                    for (int i = 0; i < YandexGame.savesData.byuWeapons.Length; i++)
                    {
                        if (YandexGame.savesData.byuWeapons[i].weaponType == item.Weapon.WeaponType)
                        {
                            Debug.Log("Дамн дамн");
                            YandexGame.savesData.byuWeapons[i].weaponCount = Mathf.Clamp(YandexGame.savesData.byuWeapons[i].weaponCount + 1, 0, weaponData.maxWeaponCount);
                        }
                    }
                }
                else // Некуплен
                {
                    if (YandexGame.savesData.money - weaponData.price >= 0)
                    {

                        List<WeaponData> wdl = new List<WeaponData>();
                        wdl.AddRange(YandexGame.savesData.byuWeapons);

                        WeaponData wd = GameManager.Instance.GetDefaultPlayerDataByType(item.Weapon.WeaponType);
                        wd.weaponCount = Mathf.Clamp(wd.weaponCount + 1, 0, weaponData.maxWeaponCount);
                        wdl.Add(wd);

                        YandexGame.savesData.money -= weaponData.price;
                        YandexGame.savesData.byuWeapons = wdl.ToArray();

                    }
                    else
                    {
                        Debug.Log("Недостаточно средств");
                    }
                }
                YandexGame.SaveProgress();
                SetPreviewItem(item);
            }
            else
            {
                Debug.Log("Недостаточно средств для покупки");
            }
        });
        equpBtn.onClick?.RemoveAllListeners();
        equpBtn.onClick?.AddListener(() =>
        {
            switch (item.Weapon.WeaponCategory)
            {
                case WeaponCategory.None:
                    YandexGame.savesData.mele = weaponData;
                    break;
                case WeaponCategory.Стрелковое_Легкое:
                    YandexGame.savesData.gun = weaponData;
                    break;
                case WeaponCategory.Стрелковое_Тяжелое:
                    YandexGame.savesData.machine = weaponData;
                    break;
                case WeaponCategory.Ближний_Бой_Одноручное:
                    YandexGame.savesData.mele = weaponData;
                    break;
                case WeaponCategory.Ближний_Бой_Двуручное:
                    YandexGame.savesData.mele = weaponData;
                    break;
                case WeaponCategory.Только_Метательное:
                    YandexGame.savesData.missile = weaponData;
                    break;
            }
            YandexGame.SaveProgress();
            SetPreviewItem(item);
            SetEqupItems();
        });
        bool isEqup =
            YandexGame.savesData.mele.weaponType != item.Weapon.WeaponType &&
            YandexGame.savesData.gun.weaponType != item.Weapon.WeaponType &&
            YandexGame.savesData.missile.weaponType != item.Weapon.WeaponType &&
            YandexGame.savesData.machine.weaponType != item.Weapon.WeaponType ? true : false;

        if (YandexGame.savesData.money - weaponData.price >= 0)
        {
            byuData.view.color = byuData.onByu;
        }
        else
        {
            byuData.view.color = byuData.offByu;
        }
        SetEqupItems();
        if (isByu && stackable) // Куплен, стакается
        {
            if (weaponData.maxWeaponCount == weaponData.weaponCount)
            {
                byuData.btn.gameObject.SetActive(false);
            }
            else
            {
                byuData.btn.gameObject.SetActive(true);
            }

            notByuParent.SetActive(false);
            weaponUpdateData.parent.SetActive(true);
            if (isEqup)
            {
                equpBtn.gameObject.SetActive(true);
            }
            else
            {
                equpBtn.gameObject.SetActive(false);
            }

            updateManger.SetItem(item, ref weaponData);
        }
        else if (isByu && !stackable) // Куплен нестакается
        {
            byuData.btn.gameObject.SetActive(false);
            notByuParent.SetActive(false);
            weaponUpdateData.parent.SetActive(true);
            if (isEqup)
            {
                equpBtn.gameObject.SetActive(true);
            }
            else
            {
                equpBtn.gameObject.SetActive(false);
            }

            updateManger.SetItem(item, ref weaponData);
        }
        else if (!isByu && stackable) // Некуплен, стакается
        {
            byuData.btn.gameObject.SetActive(true);
            notByuParent.SetActive(true);
            weaponUpdateData.parent.SetActive(false);
            equpBtn.gameObject.SetActive(false);
        }
        else if (!isByu && !stackable)// Некуплен нестакается
        {
            byuData.btn.gameObject.SetActive(true);
            notByuParent.SetActive(true);
            weaponUpdateData.parent.SetActive(false);
            equpBtn.gameObject.SetActive(false);
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
