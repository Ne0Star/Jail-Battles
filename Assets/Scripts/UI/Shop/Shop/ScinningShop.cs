using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;
using YG;

public class ScinningShop : Shop
{



    [SerializeField] private SpriteLibrary lib;
    [SerializeField] ScinningShopItem itemPrefab;
    [SerializeField] private List<ScinningShopItem> allItems;
    [SerializeField] private Transform listParent;
    [SerializeField] private PlayerView playerView;

    private void Awake()
    {
        if (!playerView)
            playerView = FindObjectOfType<PlayerView>();
        for (int i = 0; i < batchCount; i++)
        {
            CreateItem();
        }
    }

    [SerializeField] private Vector3 lastPostion;
    [SerializeField] private Quaternion lastRotation;
    [SerializeField] private Quaternion playerRotation;
    [SerializeField] private Vector3 playerPosition;
    [SerializeField] private Transform player;
    private void OnDisable()
    {
        player.transform.position = lastPostion;
        player.transform.rotation = lastRotation;
        playerView.UpdateSkins();
    }
    private void OnEnable()
    {
        lastRotation = player.transform.rotation;
        lastPostion = player.transform.position;

        player.transform.position = playerPosition;
        player.transform.rotation = playerRotation;
    }

    private void Start()
    {
        SetCategory(0);
        playerView.UpdateSkins();
    }

    private void AddItem(Sprite ico, string category, string label)
    {
        ScinningShopItem item = GetFreeItem();
        item.gameObject.SetActive(true);
        item.onSelect += SetPreview;
        item.SetItem(category, label, ico);
    }

    [SerializeField] private ByuData byuData;

    [System.Serializable]
    public struct ByuData
    {
        public Color onByu, offByu;
        public Image view;
        public Button byuScin;
        public Text byuValue;
    }
    [SerializeField] private Button equpScin;
    [SerializeField] Text playerBalanceValue;
    private void SetPreview(string category, string label)
    {
        playerBalanceValue.text = YandexGame.savesData.money + "";
        playerView.SetPreview(category, label);


        bool isByu = false, isEqup = false;

        switch (category)
        {
            case "Тело и руки":
                foreach (string s in YandexGame.savesData.byuBodies)
                {
                    if (s == label)
                    {
                        isByu = true;
                    }
                }
                break;
            case "Голова":
                foreach (string s in YandexGame.savesData.byuHead)
                {
                    if (s == label)
                    {
                        isByu = true;
                    }
                }
                break;
            case "Ноги":
                foreach (string s in YandexGame.savesData.byuLeggs)
                {
                    if (s == label)
                    {
                        isByu = true;
                    }
                }
                break;
            case "Глаза":
                foreach (string s in YandexGame.savesData.byuBlinks)
                {
                    if (s == label)
                    {
                        isByu = true;
                    }
                }
                break;
        }

        switch (category)
        {
            case "Тело и руки":
                isEqup = YandexGame.savesData.body == label;
                break;
            case "Голова":
                isEqup = YandexGame.savesData.head == label;
                break;
            case "Ноги":
                isEqup = YandexGame.savesData.leggs == label;
                break;
            case "Глаза":
                isEqup = YandexGame.savesData.blink == label;
                break;
        }
        byuData.byuScin.gameObject.SetActive(!isByu);


        if (isEqup) // Если в сохрании эта голова
        {
            equpScin.gameObject.SetActive(false);
        }
        else
        {
            if (isByu)
            {
                equpScin.gameObject.SetActive(true);
            }
            else
            {
                equpScin.gameObject.SetActive(false);
            }

        }



        int index = 1;
        foreach (string n in lib.spriteLibraryAsset.GetCategoryLabelNames(category))
        {
            if (n == label)
            {
                break;
            }
            index++;
        }
        int price = ((100 - index) * index);
        byuData.byuValue.text = price + "";

        if (YandexGame.savesData.money - price >= 0)
        {
            byuData.view.color = byuData.onByu;
        }
        else
        {
            byuData.view.color = byuData.offByu;
        }

        byuData.byuScin.onClick?.RemoveAllListeners();
        equpScin.onClick?.RemoveAllListeners();

        byuData.byuScin.onClick?.AddListener(() =>
        {
            if (YandexGame.savesData.money - price >= 0)
            {
                List<string> byuScinns = new List<string>();
                byuScinns.Add(label);
                switch (category)
                {
                    case "Тело и руки":
                        byuScinns.AddRange(YandexGame.savesData.byuBodies);
                        YandexGame.savesData.byuBodies = byuScinns.ToArray();
                        break;
                    case "Голова":
                        byuScinns.AddRange(YandexGame.savesData.byuHead);
                        YandexGame.savesData.byuHead = byuScinns.ToArray();
                        break;
                    case "Ноги":
                        byuScinns.AddRange(YandexGame.savesData.byuLeggs);
                        YandexGame.savesData.byuLeggs = byuScinns.ToArray();
                        break;
                    case "Глаза":
                        byuScinns.AddRange(YandexGame.savesData.byuBlinks);
                        YandexGame.savesData.byuBlinks = byuScinns.ToArray();
                        break;
                }
                YandexGame.savesData.money -= price;
                YandexGame.SaveProgress();
                SetPreview(category, label);
            }
            else
            {
                Debug.Log("Недостаточно средств");
            }
        });
        equpScin.onClick?.AddListener(() =>
        {
            if (isByu)
            {
                switch (category)
                {
                    case "Тело и руки":
                        YandexGame.savesData.body = label;
                        break;
                    case "Голова":
                        YandexGame.savesData.head = label;
                        break;
                    case "Ноги":
                        YandexGame.savesData.leggs = label;
                        break;
                    case "Глаза":
                        YandexGame.savesData.blink = label;
                        break;
                }
                YandexGame.SaveProgress();
                SetPreview(category, label);
            }
        });
    }

    private string GetCategoryByIndex(int index)
    {
        switch (index)
        {
            case 0:
                return "Голова";
            case 1:
                return "Тело и руки";
            case 2:
                return "Ноги";
            case 3:
                return "Глаза";
        }
        return null;
    }

    public void SetCategory(int categoryIndex)
    {
        RemoveAllItem();

        string categoryName = GetCategoryByIndex(categoryIndex);


        foreach (string n in lib.spriteLibraryAsset.GetCategoryLabelNames(categoryName))
        {
            Sprite s = lib.GetSprite(categoryName, n);

            AddItem(s, categoryName, n);
        }

        //for (int i = 0; i < items.Length; i++)
        //{
        //    Debug.Log(items[i]);
        //}
    }
    public void RemoveAllItem()
    {
        foreach (ScinningShopItem item in allItems)
        {
            if (!item.gameObject.activeSelf) return;
            item.gameObject.SetActive(false);
        }
    }

    private ScinningShopItem GetFreeItem()
    {
        foreach (ScinningShopItem item in allItems)
        {
            if (!item.gameObject.activeSelf)
            {
                return item;
            }
        }
        return null;
    }


    private ScinningShopItem CreateItem()
    {
        ScinningShopItem item = null;
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
            item = go.AddComponent<ScinningShopItem>();
            allItems.Add(item);
        }

        return item;
    }
}
