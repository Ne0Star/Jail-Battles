#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using YG;

[CustomEditor(typeof(WeaponStat))]
public class WeaponStatEditor : Editor
{
    WeaponStat stat;
    private void SetWeaponsList(ref List<WeaponData> datas)
    {
        if (datas == null) datas = new List<WeaponData>();

        for (int i = 0; i < Enum.GetValues(typeof(WeaponType)).Length; i++)
        {

            if (datas.Count == i)
            {
                stat.hidenEnemiesStat.Add(false);
                stat.hidenPlayerStat.Add(false);

                WeaponData data = new WeaponData();
                data.weaponType = (WeaponType)Enum.GetValues(typeof(WeaponType)).GetValue(i);

                datas.Add(new WeaponData());
                datas[i] = new WeaponData()
                {

                };
                Debug.Log("текс");
            }
            else
            {
                WeaponData data = datas[i];
                data.weaponType = (WeaponType)Enum.GetValues(typeof(WeaponType)).GetValue(i);



                datas[i] = data;
            }
        }
    }

    private bool playerEdit, enemuEdit;

    private ref List<WeaponData> GetData()
    {
        if (playerEdit)
        {
            return ref stat.data.playerWeapons;
        }
        else if (enemuEdit)
        {
            return ref stat.data.enemuWeapons;
        }
        else
        {
            throw new Exception("Сука");
        }
    }

    private void Awake()
    {

        stat = (WeaponStat)target;

    }

    public override void OnInspectorGUI()
    {
        GUI.skin = Resources.Load<GUISkin>("skin");
        serializedObject.Update();
        SetWeaponsList(ref stat.data.playerWeapons);
        SetWeaponsList(ref stat.data.enemuWeapons);
        if (editMode)
        {
            WeaponStatDraw(editIndex);
            GUILayout.BeginHorizontal(new GUIStyle(GUI.skin.box));
            WeaponType ty = GetData()[editIndex].weaponType;

            if (GUILayout.Button("Принять " + @"""" + ty.ToString() + @""""))
            {

                editIndex = 0;
                editMode = false;
            }
            else if (GUILayout.Button("Отменить изменения "))
            {

            }
            GUILayout.EndHorizontal();
        }
        else
        {
            if (playerEdit)
            {
                GUILayout.Label("Оружие игрока");

                for (int i = 0; i < GetData().Count; i++)
                {
                    GUILayout.BeginHorizontal(new GUIStyle(GUI.skin.box));

                    if (GUILayout.Button("Редактировать: -" + stat.data.playerWeapons[i].weaponType.ToString() + "->"))
                    {
                        editIndex = i;
                        editMode = true;
                        enemuEdit = false;
                        break;
                    }
                    GUILayout.EndHorizontal();
                }
            }
            else if (enemuEdit)
            {
                GUILayout.Label("Оружие врагов");

                for (int i = 0; i < GetData().Count; i++)
                {
                    GUILayout.BeginHorizontal(new GUIStyle(GUI.skin.box));

                    if (GUILayout.Button("Редактировать: -" + stat.data.enemuWeapons[i].weaponType.ToString() + "->"))
                    {
                        editIndex = i;
                        editMode = true;
                        playerEdit = false;
                        break;
                    }
                    GUILayout.EndHorizontal();
                }

            }
            else
            {
                if (GUILayout.Button("Редактировать оружие игрока"))
                {
                    playerEdit = true;
                    enemuEdit = false;
                }
                if (GUILayout.Button("Редактировать оружие врагов"))
                {
                    enemuEdit = true;
                    playerEdit = false;
                }
            }

            if (playerEdit || enemuEdit)
            {
                if (GUILayout.Button("<--- Вернутся"))
                {

                    playerEdit = false;
                    enemuEdit = false;
                }
            }
        }

        if (GUILayout.Button("Сбросить улуучшения"))
        {
            foreach(WeaponData data in stat.data.playerWeapons)
            {
                data.attackDistance.UpdateCount = 0;
                data.attackSpeed.UpdateCount = 0;
                data.reloadSpeed.UpdateCount = 0;
                data.attackCount.UpdateCount = 0;
                data.patronStorage.UpdateCount = 0;
                data.shootingAccuracy.UpdateCount = 0;
                data.attackDistance.UpdateCount = 0;
                data.patronCount.UpdateCount = 0;
            }
            foreach(WeaponData data in stat.data.playerWeapons)
            {
                data.attackDistance.UpdateCount = 0;
                data.attackSpeed.UpdateCount = 0;
                data.reloadSpeed.UpdateCount = 0;
                data.attackCount.UpdateCount = 0;
                data.patronStorage.UpdateCount = 0;
                data.shootingAccuracy.UpdateCount = 0;
                data.attackDistance.UpdateCount = 0;
                data.patronCount.UpdateCount = 0;
            }
        }

        serializedObject.ApplyModifiedProperties();
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }

    private void DrawItem(ref WeaponStatFloat item, string name)
    {
        item.AllowUpdate = EditorGUILayout.ToggleLeft("Улучшаемый ? ", item.AllowUpdate);
        if (item.AllowUpdate)
        {

            item.updatePriceMultipler = EditorGUILayout.FloatField("Множитель стоимости улучшений ", item.updatePriceMultipler);
            item.MinValue = EditorGUILayout.FloatField("Минимальное значение ", item.MinValue);
            item.MaxValue = EditorGUILayout.FloatField("Максимальное значение ", item.MaxValue);
            item.MaxUpdateCount = EditorGUILayout.IntField("Максимальное количество улучшенйи ", item.MaxUpdateCount);
            item.UpdateCount = EditorGUILayout.IntField("Количество улучшений ", Mathf.Clamp(item.UpdateCount, 0, item.MaxUpdateCount));
            item.UpdateValue();
            EditorGUILayout.LabelField("Значение учитывая улучшения: " + item.Value);
        }
        else
        {
            item.MaxValue = EditorGUILayout.FloatField("Значение по умолчанию ", item.MaxValue);
            item.MinValue = item.MaxValue;
        }
    }
    private void DrawItem(ref WeaponStatInt item, string name)
    {
        item.AllowUpdate = EditorGUILayout.ToggleLeft("Улучшаемый ? ", item.AllowUpdate);
        if (item.AllowUpdate)
        {
            item.updatePriceMultipler = EditorGUILayout.FloatField("Множитель стоимости улучшений ", item.updatePriceMultipler);
            item.MinValue = EditorGUILayout.IntField("Минимальное значение ", item.MinValue);
            item.MaxValue = EditorGUILayout.IntField("Максимальное значение ", item.MaxValue);
            item.MaxUpdateCount = EditorGUILayout.IntField("Максимальное количество улучшенйи ", item.MaxUpdateCount);
            item.UpdateCount = EditorGUILayout.IntField("Количество улучшений ", Mathf.Clamp(item.UpdateCount, 0, item.MaxUpdateCount));
            item.UpdateValue();
            EditorGUILayout.LabelField("Значение учитывая улучшения: " + item.Value);
        } else
        {
            item.MaxValue = EditorGUILayout.IntField("Значение по умолчанию ", item.MaxValue);
            item.MinValue = item.MaxValue;
        }
    }

    private void DropItem(ref WeaponStatFloat stat, string name)
    {
        stat.hiden = GUILayout.Toggle(stat.hiden, name);

        if (stat.hiden)
        {
            GUILayout.BeginVertical(new GUIStyle(GUI.skin.window));
            DrawItem(ref stat, name);
            GUILayout.EndVertical();
        }

    }
    private void DropItem(ref WeaponStatInt stat, string name)
    {
        stat.hiden = GUILayout.Toggle(stat.hiden, name);

        if (stat.hiden)
        {
            GUILayout.BeginVertical(new GUIStyle(GUI.skin.window));
            DrawItem(ref stat, name);
            GUILayout.EndVertical();
        }

    }

    private int editIndex = 0;
    private bool editMode = false;

    private void WeaponStatDraw(int index)
    {
        WeaponStatFloat attackDistance = GetData()[index].attackDistance;
        WeaponStatFloat reloadSpeed = GetData()[index].reloadSpeed;
        WeaponStatFloat attackSpeed = GetData()[index].attackSpeed;
        WeaponStatFloat attackDamage = GetData()[index].attackDamage;
        WeaponStatInt patronCount = GetData()[index].patronCount;
        WeaponStatInt attackCount = GetData()[index].attackCount;
        WeaponStatInt patronStorage = GetData()[index].patronStorage;
        WeaponStatFloat shootingAccuracy = GetData()[index].shootingAccuracy;
        WeaponType weaponType = GetData()[index].weaponType;
        int price = EditorGUILayout.IntField("Цена в магазине ", GetData()[index].price);
        int updatePrice = EditorGUILayout.IntField("Цена улучшений в магазине ", GetData()[index].updatePrice);
        int weaponCount = EditorGUILayout.IntField("Количество оружия (шт)", GetData()[index].weaponCount);
        int maxWeaponCount = EditorGUILayout.IntField("Максимальное количество оружия (шт)", GetData()[index].maxWeaponCount);


        switch (GetData()[index].weaponCategory)
        {
            case WeaponCategory.None:
                DropItem(ref attackDistance, "Дальность атаки");
                DropItem(ref reloadSpeed, "Скорость перезарядки");
                DropItem(ref attackSpeed, "Скорость атаки");
                DropItem(ref attackDamage, "Урон");
                DropItem(ref patronCount, "Патронов в магазине");
                DropItem(ref patronStorage, "Патронов в сумке");
                DropItem(ref shootingAccuracy, "Точность");
                break;
            case WeaponCategory.Стрелковое_Легкое:
                DropItem(ref attackDistance, "Дальность атаки");
                DropItem(ref reloadSpeed, "Скорость перезарядки");
                DropItem(ref attackSpeed, "Скорость атаки");
                DropItem(ref attackDamage, "Урон");
                DropItem(ref patronCount, "Патронов в магазине");
                DropItem(ref patronStorage, "Патронов в сумке");
                DropItem(ref shootingAccuracy, "Точность");
                break;
            case WeaponCategory.Стрелковое_Тяжелое:
                DropItem(ref attackDistance, "Дальность атаки");
                DropItem(ref reloadSpeed, "Скорость перезарядки");
                DropItem(ref attackSpeed, "Скорость атаки");
                DropItem(ref attackDamage, "Урон");
                DropItem(ref patronCount, "Патронов в магазине");
                DropItem(ref patronStorage, "Патронов в сумке");
                DropItem(ref shootingAccuracy, "Точность");
                break;
            case WeaponCategory.Ближний_Бой_Одноручное:
                DropItem(ref reloadSpeed, "Скорость перезарядки");
                DropItem(ref attackSpeed, "Скорость атаки");
                DropItem(ref attackCount, "Количество ударов");
                DropItem(ref attackDamage, "Урон");
                break;
            case WeaponCategory.Ближний_Бой_Двуручное:
                DropItem(ref reloadSpeed, "Скорость перезарядки");
                DropItem(ref attackSpeed, "Скорость атаки");
                DropItem(ref attackCount, "Количество ударов");
                DropItem(ref attackDamage, "Урон");
                break;
            case WeaponCategory.Только_Метательное:
                DropItem(ref attackDistance, "Дальность атаки");
                DropItem(ref reloadSpeed, "Скорость перезарядки");
                DropItem(ref attackSpeed, "Скорость атаки");
                DropItem(ref attackDamage, "Урон");
                DropItem(ref patronCount, "Патронов в магазине");
                DropItem(ref patronStorage, "Патронов в сумке");
                DropItem(ref shootingAccuracy, "Точность");
                break;
        }

        if (playerEdit)
        {
            stat.data.playerWeapons[index] = new WeaponData()
            {
                weaponCount = weaponCount,
                updatePrice = updatePrice,
                price = price,
                weaponType = weaponType,
                maxWeaponCount = maxWeaponCount,
                attackDistance = attackDistance,
                reloadSpeed = reloadSpeed,
                attackSpeed = attackSpeed,
                attackDamage = attackDamage,
                patronCount = patronCount,
                attackCount = attackCount,
                patronStorage = patronStorage,
                shootingAccuracy = shootingAccuracy
            };
        }
        else
        {
            stat.data.enemuWeapons[index] = new WeaponData()
            {
                weaponCount = weaponCount,
                updatePrice = updatePrice,
                price = price,
                weaponType = weaponType,
                maxWeaponCount = maxWeaponCount,
                attackDistance = attackDistance,
                reloadSpeed = reloadSpeed,
                attackSpeed = attackSpeed,
                attackDamage = attackDamage,
                patronCount = patronCount,
                attackCount = attackCount,
                patronStorage = patronStorage,
                shootingAccuracy = shootingAccuracy
            };
        }

    }
}


#endif