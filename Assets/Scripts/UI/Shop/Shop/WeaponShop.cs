using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class WeaponShop : Shop
{

    [SerializeField] private Text weaponName;
    [SerializeField] private Image weaponIco;

    [SerializeField] private TempPlayerData tempPlayerData;
    [SerializeField] private VisibleData vd;

    [System.Serializable]
    private struct VisibleData
    {
        [SerializeField] public Text weaponCount, weaponMaxCount, weaponPrice;



        [SerializeField] public Button byuBTN, equpBTN;

        [LabelOverride("Заголовок страницы ")]
        [SerializeField] public Text categoryLabel;

        [LabelOverride("Главное окно")]
        [SerializeField] public GameObject mainView;

        [LabelOverride("Окно улучшений")]
        [SerializeField] public GameObject updateView;


        public void DisableTempBTN()
        {
            byuBTN.gameObject.SetActive(false);
            equpBTN.gameObject.SetActive(false);
        }
        public void RemoveAllListeners()
        {
            byuBTN.onClick.RemoveAllListeners();
            equpBTN.onClick.RemoveAllListeners();
        }
    }

    [System.Serializable]
    private struct TempPlayerData
    {
        [SerializeField] public WeaponData[] byuWeapons;
        [SerializeField] public PlayerEqupData equpData;
    }


    [SerializeField] private EqupParent equpParent;
    [SerializeField] private WeaponUpdateManager updateManager;
    [SerializeField] private List<WeaponItem> shopStorage;

    [SerializeField] private Transform listParent;
    [SerializeField] private WeaponShopItem itemPrefab;

    [SerializeField] private List<WeaponShopItem> allItems;

    private void Awake()
    {
        if (!equpParent)
            equpParent = GetComponentInChildren<EqupParent>(true);
        if (!updateManager)
            updateManager = GetComponentInChildren<WeaponUpdateManager>(true);
        for (int i = 0; i < batchCount; i++)
        {
            CreateItem();
        }
    }

    private void OnEnable()
    {
        SetCategory(1);
        tempPlayerData.equpData = YandexGame.savesData.equpData;
        tempPlayerData.byuWeapons = YandexGame.savesData.byuWeapons;
        foreach (WeaponItem item in shopStorage)
        {
            if (item.Weapon.WeaponType == YandexGame.savesData.equpData.mele.weaponType || item.Weapon.WeaponType == YandexGame.savesData.equpData.gun.weaponType ||
                YandexGame.savesData.equpData.machine.weaponType == item.Weapon.WeaponType || YandexGame.savesData.equpData.missile.weaponType == item.Weapon.WeaponType)
            {
                WeaponData data = GetWeaponPlayerData(item);
                // Debug.Log(item + " " + data.weaponCategory);
                equpParent.EqupWeapon(item, data);
            }
        }
        vd.mainView.SetActive(false);
    }

    private void Start()
    {

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
            vd.categoryLabel.text = data.resultText;
            vd.categoryLabel.font = data.resultFont;
        }
        else
        {
            LocalizerData data = GameManager.Instance.GetValueByKey(category.ToString());
            vd.categoryLabel.text = data.resultText;
            vd.categoryLabel.font = data.resultFont;
        }
        //playerView.balance.text = YandexGame.savesData.money + " ";
        //onSwipeCategory?.Invoke(category);
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
    private bool CheckByu(WeaponItem item)
    {

        for (int i = 0; i < YandexGame.savesData.byuWeapons.Length; i++)
        {
            WeaponData playerWeaponData = YandexGame.savesData.byuWeapons[i];

            if (playerWeaponData.weaponType == item.Weapon.WeaponType)
            {
                return true;
            }
        }
        return false;
    }
    private WeaponData GetWeaponDefaultData(WeaponItem item)
    {
        foreach (WeaponData data in GameManager.Instance.PlayerDefaultWeaponData)
        {
            if (data.weaponType == item.Weapon.WeaponType)
            {
                return data;
            }
        }
        throw new KeyNotFoundException("Не удалось найти значения по умолчанию для оружия: " + item.Weapon + " " + item.Weapon.WeaponType);
    }
    private ref WeaponData GetWeaponPlayerData(WeaponItem item)
    {
        ref WeaponData data = ref YandexGame.savesData.byuWeapons[0];
        for (int i = 0; i < YandexGame.savesData.byuWeapons.Length; i++)
        {
            data = YandexGame.savesData.byuWeapons[i];
            if (data.weaponType == item.Weapon.WeaponType)
            {
                return ref YandexGame.savesData.byuWeapons[i];
            }
        }
        Debug.LogWarning("Оружие не было куплено, однако скрипты пытаются получить к нему доступ: " + item.Weapon + " " + item.Weapon.WeaponType);
        return ref data;
    }




    private void SetPreviewItem(WeaponItem item)
    {
        Debug.Log("Какого хуя ?)");
        WeaponData defaultData = GetWeaponDefaultData(item);


        vd.RemoveAllListeners();
        vd.DisableTempBTN();
        vd.mainView.SetActive(true);
        vd.updateView.SetActive(false);

        LocalizerData textData = GameManager.Instance.GetValueByKey(item.NameKey);
        weaponIco.sprite = item.ShopIco;
        weaponName.text = item.UseTranslator ? textData.resultText : item.Weapon.name;
        weaponName.font = textData.resultFont;


        if (CheckByu(item))
        {
            WeaponData currentData = GetWeaponPlayerData(item);
            vd.updateView.SetActive(true);
            vd.weaponPrice.text = defaultData.price + "";
            vd.weaponCount.text = currentData.weaponCount + "";
            vd.weaponMaxCount.text = currentData.maxWeaponCount + "";

            if (currentData.weaponCount < currentData.maxWeaponCount)
            {
                vd.byuBTN.gameObject.SetActive(true);
                vd.byuBTN.onClick.AddListener(() =>
                {
                    Debug.Log("Попытка купить стаковый предмет..");


                    List<WeaponData> datas = new List<WeaponData>();
                    datas.AddRange(YandexGame.savesData.byuWeapons);
                    for (int i = 0; i < datas.Count; i++)
                    {
                        WeaponData data = datas[i];
                        if (data.weaponType == currentData.weaponType)
                        {
                            YandexGame.savesData.byuWeapons[i].weaponCount = Mathf.Clamp(data.weaponCount + 1, 0, data.maxWeaponCount);
                        }
                    }
                    YandexGame.SaveProgress();
                    SetPreviewItem(item);
                });

            }
            else
            {
                Debug.Log("Куплено максимальное количество");
            }

            vd.equpBTN.gameObject.SetActive(true);
            vd.equpBTN.onClick.AddListener(() =>
            {

                equpParent.EqupWeapon(item, currentData);
                SetPreviewItem(item);
                Debug.Log("Попытка одеть");
            });
            updateManager.SetItem(item, defaultData, ref currentData);
        }
        else
        {
            Debug.Log("Покупка в первые");
            vd.weaponPrice.text = defaultData.price + "";
            vd.weaponCount.text = defaultData.weaponCount + "";
            vd.weaponMaxCount.text = defaultData.maxWeaponCount + "";

            vd.byuBTN.gameObject.SetActive(true);
            vd.byuBTN.onClick.AddListener(() =>
            {



                List<WeaponData> datas = new List<WeaponData>();
                datas.AddRange(YandexGame.savesData.byuWeapons);
                WeaponData newData = defaultData;
                newData.weaponCount += Mathf.Clamp(newData.weaponCount + 1, 0, newData.maxWeaponCount);
                datas.Add(newData);
                YandexGame.savesData.byuWeapons = datas.ToArray();
                YandexGame.SaveProgress();
                SetPreviewItem(item);
            });
        }
    }




    //[SerializeField] private EqupParent equpParent;

    //[SerializeField] private Vector3 lastPostion;
    //[SerializeField] private Quaternion lastRotation;
    //[SerializeField] private Quaternion playerRotation;
    //[SerializeField] private Vector3 playerPosition;
    //[SerializeField] private Transform player;


    //[SerializeField] private GameObject notByuParent, fullUpdateParent;
    //[SerializeField] private Button equpBtn;
    //[SerializeField] private GameObject mainView;
    //[SerializeField] private Text categoryLabel;
    //[SerializeField] private List<WeaponItem> shopStorage;

    //[SerializeField] private WeaponUpdateData weaponUpdateData;
    //[SerializeField] private WeaponItemData weaponView;
    //[SerializeField] private PlayerItemData playerView;
    //[SerializeField] private ByuData byuData;

    //[SerializeField] private WeaponUpdateManager updateManger;

    //[System.Serializable]
    //public struct WeaponUpdateData
    //{
    //    public GameObject parent;
    //}

    //[System.Serializable]
    //public struct ByuData
    //{
    //    public Color onByu, offByu;
    //    public Image view;
    //    public Button btn;
    //}

    //[System.Serializable]
    //public struct PlayerItemData
    //{
    //    public Text balance;
    //}
    //[System.Serializable]
    //public struct WeaponItemData
    //{
    //    public Image ico;
    //    public Text name, price;

    //    public Text count, maxCount;

    //    public RectTransform ratingView;
    //}

    //public event System.Action<WeaponItem> onSelected;
    //public event System.Action<WeaponCategory> onSwipeCategory;
    //[SerializeField] private Transform listParent;
    //[SerializeField] private WeaponShopItem itemPrefab;
    //[SerializeField] private List<WeaponShopItem> allItems = new List<WeaponShopItem>();
    //[SerializeField] private List<WeaponSlot> slots;
    //private float startWidth;
    //private void Awake()
    //{
    //    for (int i = 0; i < batchCount; i++)
    //    {
    //        CreateItem();
    //    }
    //    updateManger = GetComponentInChildren<WeaponUpdateManager>(true);
    //    startWidth = weaponView.ratingView.GetWidth();
    //    mainView.SetActive(false);
    //    YandexGame.GetDataEvent += UpdateBalance;
    //}

    //private void UpdateBalance()
    //{
    //    playerView.balance.text = YandexGame.savesData.money + " ";
    //}


    //private void OnEnable()
    //{
    //    lastRotation = player.transform.rotation;
    //    lastPostion = player.transform.position;

    //    player.transform.position = playerPosition;
    //    player.transform.rotation = playerRotation;


    //    if (equpParent.gameObject.activeSelf)
    //    {
    //        SetEqupItems();
    //    }

    //}

    //private void SetEqupItems()
    //{
    //    foreach (WeaponItem item in shopStorage)
    //    {
    //        if (item.Weapon.WeaponType == YandexGame.savesData.mele.weaponType || item.Weapon.WeaponType == YandexGame.savesData.gun.weaponType ||
    //            YandexGame.savesData.machine.weaponType == item.Weapon.WeaponType || YandexGame.savesData.missile.weaponType == item.Weapon.WeaponType)
    //        {
    //            equpParent.EqupWeapon(item);
    //        }
    //    }
    //}


    //private void OnDisable()
    //{
    //    player.transform.position = lastPostion;
    //    player.transform.rotation = lastRotation;
    //    mainView.SetActive(false);
    //}

    //private void Set(RectTransform target, float value, float maxValue, float minValue)
    //{
    //    float coof = Mathf.InverseLerp(minValue, maxValue, value);
    //    float total = Mathf.Lerp(value, startWidth, coof);
    //    target.SetWidth(total);
    //}

    //private void GetRating(WeaponItem item)
    //{
    //    float weaponValue = float.MaxValue;
    //    float maxWeaponValue = 0f;
    //    float minWeaponValue = 999f;
    //    foreach (WeaponData m in GameManager.Instance.PlayerDefaultWeaponData)
    //    {
    //        float value = m.attackDamage.Value;
    //        if (m.weaponType == item.Weapon.WeaponType)
    //        {
    //            weaponValue = value;
    //        }
    //        if (value > maxWeaponValue)
    //        {
    //            maxWeaponValue = value;
    //        }
    //        if (value < minWeaponValue)
    //        {
    //            minWeaponValue = value;
    //        }
    //    }
    //    Debug.Log(" value = " + weaponValue + " minValue = " + minWeaponValue + " maxValue = " + maxWeaponValue);
    //    Set(weaponView.ratingView, weaponValue, maxWeaponValue, minWeaponValue);
    //}

    //private void SetWeaponView(WeaponItem item, WeaponData shopData, WeaponData playerData, LocalizerData textData)
    //{
    //    weaponView.ico.sprite = item.ShopIco;
    //    weaponView.name.text = item.UseTranslator ? textData.resultText : item.Weapon.name;
    //    weaponView.name.font = textData.resultFont;
    //    weaponView.price.text = shopData.price + " ";
    //    weaponView.price.font = textData.resultFont;
    //    weaponView.count.text = playerData.weaponCount + " ";
    //    weaponView.count.font = textData.resultFont;
    //    weaponView.maxCount.text = shopData.maxWeaponCount + " ";
    //    weaponView.maxCount.font = textData.resultFont;
    //    playerView.balance.text = YandexGame.savesData.money + " ";
    //    playerView.balance.font = textData.resultFont;
    //}

    //private WeaponItem currentItem;

    //private void temp()
    //{
    //    SetPreviewItem(currentItem);
    //    equpParent.onRemove -= temp;
    //}

    //private void SetPreviewItem(WeaponItem item)
    //{
    //    WeaponData playerWeaponData = new WeaponData();
    //    WeaponData shopWeaponData = new WeaponData();
    //    LocalizerData textData = GameManager.Instance.GetValueByKey(item.NameKey);
    //    currentItem = item;
    //    equpParent.onRemove += temp;

    //    bool isByu = false;
    //    int index = 0;
    //    foreach (WeaponData data in GameManager.Instance.PlayerDefaultWeaponData)
    //    {
    //        if (data.weaponType == item.Weapon.WeaponType)
    //        {
    //            shopWeaponData = data;
    //            break;
    //        }
    //    }

    //    foreach (WeaponData m in YandexGame.savesData.byuWeapons)
    //    {
    //        if (m.weaponType == item.Weapon.WeaponType)
    //        {
    //            playerWeaponData = m;
    //            isByu = true;
    //            break;
    //        }
    //        index++;
    //    }

    //    bool stackable = playerWeaponData.maxWeaponCount > 1 ? true : false;
    //    mainView.SetActive(true);

    //    SetWeaponView(item, shopWeaponData, playerWeaponData, textData);
    //    byuData.btn.onClick?.RemoveAllListeners();
    //    byuData.btn.onClick?.AddListener(() =>
    //    {
    //        if (YandexGame.savesData.money - shopWeaponData.price >= 0)
    //        {
    //            if (isByu) // Если куплен, добавляем к максимальному количеству
    //            {
    //                if (YandexGame.savesData.money - shopWeaponData.price >= 0)
    //                {
    //                    for (int i = 0; i < YandexGame.savesData.byuWeapons.Length; i++)
    //                    {
    //                        if (YandexGame.savesData.byuWeapons[i].weaponType == item.Weapon.WeaponType)
    //                        {
    //                            Debug.Log("Дамн дамн");
    //                            YandexGame.savesData.byuWeapons[i].weaponCount = Mathf.Clamp(YandexGame.savesData.byuWeapons[i].weaponCount + 1, 0, shopWeaponData.maxWeaponCount);
    //                        }
    //                    }
    //                    YandexGame.savesData.money -= shopWeaponData.price;
    //                }
    //                else
    //                {
    //                    Debug.Log("Недостаточно средств");
    //                }

    //            }
    //            else // Некуплен, добавляем новый элемент в масств
    //            {
    //                Debug.Log("Не куплен");
    //                if (YandexGame.savesData.money - shopWeaponData.price >= 0)
    //                {

    //                    List<WeaponData> wdl = new List<WeaponData>();
    //                    wdl.AddRange(YandexGame.savesData.byuWeapons);

    //                    WeaponData data = shopWeaponData;
    //                    data.weaponCount = Mathf.Clamp(data.weaponCount + 1, 0, data.maxWeaponCount);
    //                    wdl.Add(data);

    //                    YandexGame.savesData.money -= shopWeaponData.price;
    //                    YandexGame.savesData.byuWeapons = wdl.ToArray();

    //                }
    //                else
    //                {
    //                    Debug.Log("Недостаточно средств");
    //                }
    //            }
    //            YandexGame.SaveProgress();
    //            SetPreviewItem(item);
    //        }
    //        else
    //        {
    //            Debug.Log("Недостаточно средств для покупки");
    //        }
    //    });
    //    equpBtn.onClick?.RemoveAllListeners();
    //    equpBtn.onClick?.AddListener(() =>
    //    {
    //        switch (item.Weapon.WeaponCategory)
    //        {
    //            case WeaponCategory.None:
    //                YandexGame.savesData.mele = playerWeaponData;
    //                break;
    //            case WeaponCategory.Стрелковое_Легкое:
    //                YandexGame.savesData.gun = playerWeaponData;
    //                break;
    //            case WeaponCategory.Стрелковое_Тяжелое:
    //                YandexGame.savesData.machine = playerWeaponData;
    //                break;
    //            case WeaponCategory.Ближний_Бой_Одноручное:
    //                YandexGame.savesData.mele = playerWeaponData;
    //                break;
    //            case WeaponCategory.Ближний_Бой_Двуручное:
    //                YandexGame.savesData.mele = playerWeaponData;
    //                break;
    //            case WeaponCategory.Только_Метательное:
    //                YandexGame.savesData.missile = playerWeaponData;
    //                break;
    //        }
    //        YandexGame.SaveProgress();
    //        SetPreviewItem(item);
    //        SetEqupItems();
    //    });
    //    bool isEqup =
    //        YandexGame.savesData.mele.weaponType != item.Weapon.WeaponType &&
    //        YandexGame.savesData.gun.weaponType != item.Weapon.WeaponType &&
    //        YandexGame.savesData.missile.weaponType != item.Weapon.WeaponType &&
    //        YandexGame.savesData.machine.weaponType != item.Weapon.WeaponType ? true : false;

    //    if (YandexGame.savesData.money - shopWeaponData.price >= 0)
    //    {
    //        byuData.view.color = byuData.onByu;
    //    }
    //    else
    //    {
    //        byuData.view.color = byuData.offByu;
    //    }
    //    SetEqupItems();
    //    if (isByu && stackable) // Куплен, стакается
    //    {
    //        if (shopWeaponData.maxWeaponCount == playerWeaponData.weaponCount)
    //        {
    //            byuData.btn.gameObject.SetActive(false);
    //        }
    //        else
    //        {
    //            byuData.btn.gameObject.SetActive(true);
    //        }

    //        notByuParent.SetActive(false);
    //        weaponUpdateData.parent.SetActive(true);
    //        if (isEqup)
    //        {
    //            equpBtn.gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            equpBtn.gameObject.SetActive(false);
    //        }
    //        updateManger.SetItem(item, index);
    //    }
    //    else if (isByu && !stackable) // Куплен нестакается
    //    {
    //        byuData.btn.gameObject.SetActive(false);
    //        notByuParent.SetActive(false);
    //        weaponUpdateData.parent.SetActive(true);
    //        if (isEqup)
    //        {
    //            equpBtn.gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            equpBtn.gameObject.SetActive(false);
    //        }

    //        updateManger.SetItem(item, index);
    //    }
    //    else if (!isByu && stackable) // Некуплен, стакается
    //    {
    //        byuData.btn.gameObject.SetActive(true);
    //        notByuParent.SetActive(true);
    //        weaponUpdateData.parent.SetActive(false);
    //        equpBtn.gameObject.SetActive(false);
    //    }
    //    else if (!isByu && !stackable)// Некуплен нестакается
    //    {
    //        byuData.btn.gameObject.SetActive(true);
    //        notByuParent.SetActive(true);
    //        weaponUpdateData.parent.SetActive(false);
    //        equpBtn.gameObject.SetActive(false);
    //    }
    //    GetRating(item);
    //    onSelected?.Invoke(item);
    //}

    //public void SetCategory(int categoryIndex)
    //{
    //    RemoveAllItem();
    //    WeaponCategory category = (WeaponCategory)categoryIndex;



    //    foreach (WeaponItem item in shopStorage)
    //    {
    //        if (item.Weapon.WeaponCategory == category)
    //        {
    //            AddItem(item);
    //        }
    //    }
    //    if (category == WeaponCategory.None)
    //    {
    //        LocalizerData data = GameManager.Instance.GetValueByKey("weaponShop");
    //        categoryLabel.text = data.resultText;
    //        categoryLabel.font = data.resultFont;
    //    }
    //    else
    //    {
    //        LocalizerData data = GameManager.Instance.GetValueByKey(category.ToString());
    //        categoryLabel.text = data.resultText;
    //        categoryLabel.font = data.resultFont;


    //    }
    //    playerView.balance.text = YandexGame.savesData.money + " ";
    //    onSwipeCategory?.Invoke(category);
    //}

    //private void AddItem(WeaponItem itemType)
    //{
    //    WeaponShopItem item = GetFreeItem();
    //    item.SetItem(itemType);
    //    item.gameObject.SetActive(true);
    //    item.onSelect += SetPreviewItem;
    //}

    //public void RemoveAllItem()
    //{
    //    foreach (WeaponShopItem item in allItems)
    //    {
    //        if (!item.gameObject.activeSelf) return;
    //        item.gameObject.SetActive(false);
    //    }
    //}

    //private WeaponShopItem GetFreeItem()
    //{
    //    foreach (WeaponShopItem item in allItems)
    //    {
    //        if (!item.gameObject.activeSelf)
    //        {
    //            return item;
    //        }
    //    }
    //    return null;
    //}


    //private ShopItem CreateItem()
    //{
    //    WeaponShopItem item = null;
    //    if (itemPrefab)
    //    {
    //        item = Instantiate(itemPrefab, listParent);
    //        item.gameObject.SetActive(false);
    //        allItems.Add(item);
    //    }
    //    else
    //    {
    //        GameObject go = new GameObject();
    //        go.transform.parent = listParent;
    //        go.SetActive(false);
    //        item = go.AddComponent<WeaponShopItem>();
    //        allItems.Add(item);
    //    }
    //    return item;
    //}

    //private void Start()
    //{
    //    SetCategory(1);
    //}

}
