using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class WeaponUpdateManager : MonoBehaviour
{
    [SerializeField] private Text playerBalance;

    [SerializeField] private Button byuItemBTN;
    [SerializeField] private Button byuUpdateBTN;
    [SerializeField] private Button equpBTN;

    [SerializeField]
    private Text byuValue, updateValue;

    [SerializeField] private Text attackSpeed_v, reloadSpeed_v, attackCount_v, attackDamage_v;

    [SerializeField] private RectTransform attackSpeed_vT, reloadSpeed_vT, attackCount_vT, attackDamage_vT;

    [SerializeField] private WeaponShop shop;

    [SerializeField] private float startWidth;


    [SerializeField] GameObject updateBody, buyBody, equpBody;

    private void Awake()
    {
        shop.onSelected += SetItem;

        startWidth = attackSpeed_vT.sizeDelta.x;

        byuItemBTN?.onClick?.AddListener(() =>
        {
            playerBalance.text = YG.YandexGame.savesData.money + " ";

            if (YG.YandexGame.savesData.money < selectedItem.Price)
            {
                Debug.Log("������������ ������� ��� ������� ������");
                return;
            }

            ByuWeapon();
        });
        byuUpdateBTN?.onClick?.AddListener(() =>
        {
            playerBalance.text = YG.YandexGame.savesData.money + " ";

            if (YG.YandexGame.savesData.money < selectedItem.Price * selectedItem.Weapon.CurrentUpdateCount)
            {
                Debug.Log("������������ ������� ��� ������� ���������");
                return;
            }

            UpdateWeapon();
        });
        equpBTN?.onClick?.AddListener(() =>
        {
            Equp();
        });

        shop.onSwipeCategory += Cansel;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.ScreenShoot();
        }
    }

    private void Equp()
    {

    }

    private void ByuWeapon()
    {
        Debug.Log("������� ������ ");

        bool itsStackable = false;
        for (int i = 0; i < YG.YandexGame.savesData.byuWeapons.Length; i++)
        {
            WeaponPlayerData data = YG.YandexGame.savesData.byuWeapons[i];
            if (data.wapon == selectedItem.Weapon.WeaponType)
            {
                if (data.counts < selectedItem.MaxWeaponCount)
                {
                    data.counts++;
                    YG.YandexGame.savesData.byuWeapons[i] = data;
                    itsStackable = true;
                    break;
                }
            }
        }
        if (!itsStackable)
        {
            WeaponPlayerData[] lastData = YG.YandexGame.savesData.byuWeapons;
            List<WeaponPlayerData> newData = new List<WeaponPlayerData>();
            newData.AddRange(lastData);
            newData.Add(new WeaponPlayerData()
            {
                counts = 1,
                wapon = selectedItem.Weapon.WeaponType,
                updateCount = 0
            });
            YG.YandexGame.savesData.byuWeapons = newData.ToArray();
        }
        SetItem(selectedItem);
        YG.YandexGame.SaveProgress();
    }

    public void Cansel()
    {
        selectedItem = null;
        equpBody.gameObject.SetActive(false);
        buyBody.gameObject.SetActive(false);
        updateBody.gameObject.SetActive(false);
        stackableBody.gameObject.SetActive(false);
        byuItemBTN.gameObject.SetActive(false);
        byuUpdateBTN.gameObject.SetActive(false);
    }

    public void UpdateWeapon()
    {
        Debug.Log("������� �������� ");
        for (int i = 0; i < YG.YandexGame.savesData.byuWeapons.Length; i++)
        {
            WeaponPlayerData data = YG.YandexGame.savesData.byuWeapons[i];
            if (data.wapon == selectedItem.Weapon.WeaponType)
            {

                data.updateCount = Mathf.Clamp(data.updateCount + 1, 0, selectedItem.Weapon.MaxUpdateCount);


                YG.YandexGame.savesData.byuWeapons[i] = data;
                break;
            }
        }
        SetItem(selectedItem);
        YG.YandexGame.SaveProgress();
    }

    [SerializeField] private WeaponItem selectedItem;
    [SerializeField] private Text stackableValue, stackableMaxValue;
    [SerializeField] private GameObject stackableBody;
    private void SetItem(WeaponItem item)
    {
        //attackCount_v.text = item.Weapon.AttackCount + " ";
        //reloadSpeed_v.text = item.Weapon.ReloadSpeed + " ";
        //attackSpeed_v.text = item.Weapon.AttackSpeed + " ";
        //attackDamage_v.text = item.Weapon.AttackDamage + " ";

        selectedItem = item;

        bool isByu = false;
        WeaponPlayerData currentData = new WeaponPlayerData();
        foreach (WeaponPlayerData data in YG.YandexGame.savesData.byuWeapons)
        {
            if (data.wapon == item.Weapon.WeaponType)
            {
                currentData = data;
                item.Weapon.SetUpdate(data.updateCount);
                isByu = true;
                break;
            }
        }
        bool stackable = item.MaxWeaponCount > 1;

        byuValue.text = selectedItem.Price + " ";
        updateValue.text = (selectedItem.Price * selectedItem.Weapon.CurrentUpdateCount) + " ";
        playerBalance.text = YG.YandexGame.savesData.money + " ";

        //playerBalance.transform.parent.gameObject.SetActive(true);

        Debug.Log("Stackable = " + stackable + " Isbyu = " + isByu);

        if (isByu && stackable) // ������, ���������
        {
            buyBody.gameObject.SetActive(false);
            updateBody.gameObject.SetActive(true);
            stackableBody.gameObject.SetActive(true);
equpBody.SetActive(true);

            stackableValue.text = currentData.counts + " ";
            stackableMaxValue.text = item.MaxWeaponCount + " ";
            

            byuUpdateBTN.gameObject.SetActive(true);
            if (currentData.counts < item.MaxWeaponCount) // ������� �� ������������ ����������
            {
                byuItemBTN.gameObject.SetActive(true);
            }
            else // ������� ������������ ����������
            {
                byuItemBTN.gameObject.SetActive(false);
            }




            VisibilityUpdateItem(selectedItem);
        }
        else if (isByu && !stackable) // ������ �����������
        {
            buyBody.gameObject.SetActive(false);
            updateBody.gameObject.SetActive(true);
            stackableBody.gameObject.SetActive(false);
            equpBody.SetActive(true);

            byuItemBTN.gameObject.SetActive(false);
            byuUpdateBTN.gameObject.SetActive(true);
            VisibilityUpdateItem(selectedItem);

        }
        else if (!isByu && stackable) // ��������, ���������
        {
            stackableBody.gameObject.SetActive(true);
            byuItemBTN.gameObject.SetActive(true);
            byuUpdateBTN.gameObject.SetActive(false);



        }
        else if (!isByu && !stackable)// �������� �����������
        {
            buyBody.gameObject.SetActive(true);
            updateBody.gameObject.SetActive(false);
            stackableBody.gameObject.SetActive(false);
            equpBody.SetActive(false);

            if (selectedItem.Weapon.CurrentUpdateCount == 0)
            {

            }

            byuItemBTN.gameObject.SetActive(true);
            byuUpdateBTN.gameObject.SetActive(false);
        }

        //if (isByu)
        //{
        //    if (item.MaxWeaponCount > 1)
        //    {
        //        // ���� ����� ������ ���
        //        if (currentData.counts <= item.MaxWeaponCount)
        //        {
        //            buyBody.gameObject.SetActive(false);
        //            byuItemBTN.gameObject.SetActive(true);
        //            stackableBody.SetActive(true);
        //            stackableValue.text = currentData.counts + " ";
        //            stackableMaxValue.text = item.MaxWeaponCount + " ";
        //        }
        //        else
        //        {
        //            Debug.Log("������� ������������ ����������");
        //        }
        //    }
        //    else
        //    {
        //        buyBody.gameObject.SetActive(false);
        //        updateBody.gameObject.SetActive(true);

        //        updateValue.text = (selectedItem.Price * selectedItem.Weapon.CurrentUpdateCount) + " ";
        //        byuItemBTN.gameObject.SetActive(false);
        //        byuUpdateBTN.gameObject.SetActive(true);
        //        VisibilityUpdateItem(selectedItem);
        //    }
        //}
        //else
        //{

        //}
    }




    private void VisibilityUpdateItem(WeaponItem item)
    {
        Set(attackSpeed_vT, item.Weapon.AttackSpeed, item.Weapon.FullStat.AttackSpeed);
        Set(attackCount_vT, item.Weapon.AttackCount, item.Weapon.FullStat.AttackCount);
        Set(attackDamage_vT, item.Weapon.AttackDamage, item.Weapon.FullStat.AttackDamage);
        Set(reloadSpeed_vT, item.Weapon.ReloadSpeed, item.Weapon.FullStat.ReloadSpeed);
    }

    private void Set(RectTransform target, float value, float maxValue)
    {
        float precent = value * 100 / maxValue;
        float coof = startWidth / 100f;
        float totalWidth = coof * precent;
        target.sizeDelta = new Vector2(totalWidth, target.sizeDelta.y);
    }
}
