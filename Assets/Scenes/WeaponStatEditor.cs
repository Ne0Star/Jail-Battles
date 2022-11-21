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
        for (int i = 0; i < Enum.GetValues(typeof(WeaponType)).Length; i++)
        {

            if (datas.Count == i)
            {
                stat.hidenEnemiesStat.Add(false);
                stat.hidenPlayerStat.Add(false);
                datas.Add(new WeaponData());
                datas[i] = new WeaponData()
                {

                    weaponType = (WeaponType)Enum.GetValues(typeof(WeaponType)).GetValue(i)
                };
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
            return ref stat.playerWeapons;
        }
        else if (enemuEdit)
        {
            return ref stat.enemuWeapons;
        }
        else
        {
            throw new Exception("����");
        }
    }

    private void Awake()
    {

        stat = (WeaponStat)target;
        SetWeaponsList(ref stat.playerWeapons);
        SetWeaponsList(ref stat.enemuWeapons);
    }

    public override void OnInspectorGUI()
    {
        GUI.skin = Resources.Load<GUISkin>("skin");
        serializedObject.Update();

        if (editMode)
        {
            WeaponStatDraw(editIndex);

            GUILayout.BeginHorizontal(new GUIStyle(GUI.skin.box));
            WeaponType ty = GetData()[editIndex].weaponType;

            if (GUILayout.Button("������� " + @"""" + ty.ToString() + @""""))
            {

                editIndex = 0;
                editMode = false;
            }
            else if (GUILayout.Button("�������� ��������� "))
            {

            }
            GUILayout.EndHorizontal();
        }
        else
        {
            if (playerEdit)
            {
                GUILayout.Label("������ ������");

                for (int i = 0; i < GetData().Count; i++)
                {
                    GUILayout.BeginHorizontal(new GUIStyle(GUI.skin.box));

                    if (GUILayout.Button("�������������: -" + stat.playerWeapons[i].weaponType.ToString() + "->"))
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
                GUILayout.Label("������ ������");

                for (int i = 0; i < GetData().Count; i++)
                {
                    GUILayout.BeginHorizontal(new GUIStyle(GUI.skin.box));

                    if (GUILayout.Button("�������������: -" + stat.enemuWeapons[i].weaponType.ToString() + "->"))
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
                if (GUILayout.Button("������������� ������ ������"))
                {
                    playerEdit = true;
                    enemuEdit = false;
                }
                if (GUILayout.Button("������������� ������ ������"))
                {
                    enemuEdit = true;
                    playerEdit = false;
                }
            }

            if (playerEdit || enemuEdit)
            {
                if (GUILayout.Button("<--- ��������"))
                {

                    playerEdit = false;
                    enemuEdit = false;
                }
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
        item.AllowUpdate = EditorGUILayout.ToggleLeft("���������� ? ", item.AllowUpdate);
        if (item.AllowUpdate)
        {
            item.MaxValue = EditorGUILayout.FloatField("������������ �������� ", item.MaxValue);
            item.MaxUpdateCount = EditorGUILayout.IntField("������������ ���������� ��������� ", item.MaxUpdateCount);
            item.UpdateCount = EditorGUILayout.IntField("���������� ��������� ", Mathf.Clamp(item.UpdateCount, 0, item.MaxUpdateCount));
        }
    }
    private void DrawItem(ref WeaponStatInt item, string name)
    {
        item.AllowUpdate = EditorGUILayout.ToggleLeft("���������� ? ", item.AllowUpdate);
        if (item.AllowUpdate)
        {
            item.MaxValue = EditorGUILayout.IntField("������������ �������� ", item.MaxValue);
            item.MaxUpdateCount = EditorGUILayout.IntField("������������ ���������� ��������� ", item.MaxUpdateCount);
            item.UpdateCount = EditorGUILayout.IntField("���������� ��������� ", Mathf.Clamp(item.UpdateCount, 0, item.MaxUpdateCount));
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
        int price = EditorGUILayout.IntField("���� � �������� ", GetData()[index].price);
        int updatePrice = EditorGUILayout.IntField("���� ��������� � �������� ", GetData()[index].updatePrice);
        int weaponCount = EditorGUILayout.IntField("���������� ������ (��)", GetData()[index].weaponCount);
        int maxWeaponCount = EditorGUILayout.IntField("������������ ���������� ������ (��)", GetData()[index].maxWeaponCount);

        DropItem(ref attackDistance, "��������� �����");
        DropItem(ref reloadSpeed, "�������� �����������");
        DropItem(ref attackSpeed, "�������� �����");
        DropItem(ref attackDamage, "����");
        DropItem(ref patronCount, "�������� � ��������");
        DropItem(ref attackCount, "���������� ������");
        DropItem(ref patronStorage, "�������� � �����");
        DropItem(ref shootingAccuracy, "��������");

        if (playerEdit)
        {
            stat.playerWeapons[index] = new WeaponData()
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
            stat.enemuWeapons[index] = new WeaponData()
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