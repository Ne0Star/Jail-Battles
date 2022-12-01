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
                stat.hidenPlayerStat.Add(false);
                stat.allowWeapons.Add(false);
                stat.chances.Add(50f);
                WeaponData data = new WeaponData();
                data.weaponType = (WeaponType)Enum.GetValues(typeof(WeaponType)).GetValue(i);

                datas.Add(new WeaponData());
                datas[i] = new WeaponData()
                {

                };
                Debug.Log("����");
            }
            else
            {
                WeaponData data = datas[i];
                data.weaponType = (WeaponType)Enum.GetValues(typeof(WeaponType)).GetValue(i);

                datas[i] = data;
            }
        }
    }

    private bool edit;

    private ref List<WeaponData> GetData()
    {
        return ref stat.data.datas;
    }

    private void Awake()
    {
        stat = (WeaponStat)target;
    }

    public override void OnInspectorGUI()
    {
        GUI.skin = Resources.Load<GUISkin>("skin");
        serializedObject.Update();
        SetWeaponsList(ref stat.data.datas);
        if (editMode)
        {
            WeaponStatDraw(editIndex);
            GUILayout.BeginHorizontal(new GUIStyle(GUI.skin.box));
            WeaponType ty = GetData()[editIndex].weaponType;

            if (GUILayout.Button("������� " + @"""" + ty.ToString() + @""""))
            {

                editIndex = 0;
                editMode = false;
                edit = true;
            }
            //else if (GUILayout.Button("�������� ��������� "))
            //{

            //}
            GUILayout.EndHorizontal();
        }
        else
        {
            if (edit)
            {
                GUILayout.Label("������ ��� �������������:");

                for (int i = 0; i < GetData().Count; i++)
                {
                    if (stat.allowWeapons[i])
                    {
                        GUILayout.BeginHorizontal(new GUIStyle(GUI.skin.box));


                        GUILayout.Label("����: " +  (stat.chances[i].ToString()  + "    ").Substring(0, 4));
                        //GUILayout.BeginHorizontal(new GUIStyle(GUI.skin.box));




                        stat.chances[i] = GUILayout.HorizontalSlider(stat.chances[i], 0f, 100f, GUILayout.Width(100));

                        //stat.chances[i] = EditorGUILayout.FloatField(stat.data.datas[i].weaponType.ToString(), stat.chances[i], GUILayout.Width(80));
                        //EditorGUILayout.LabelField("���� ");
                        //GUILayout.EndHorizontal();
                        if (GUILayout.Button("�������: " + stat.data.datas[i].weaponType.ToString()))
                        {
                            editIndex = i;
                            editMode = true;
                            edit = false;
                            break;
                        }
                        if (GUILayout.Button("������"))
                        {
                            stat.allowWeapons[i] = false;
                        }
                        GUILayout.EndHorizontal();
                    }
                    else
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.Label(stat.data.datas[i].weaponType.ToString() + "->");
                        if (GUILayout.Button("��������"))
                        {
                            stat.allowWeapons[i] = true;
                        }
                        GUILayout.EndHorizontal();
                    }
                }
            }
            else
            {
                if (GUILayout.Button("������������� ������"))
                {
                    edit = true;
                }
            }

            if (edit)
            {
                if (GUILayout.Button("<--- ��������"))
                {
                    edit = false;
                }
            }
        }

        if (GUILayout.Button("�������� ����������"))
        {
            foreach (WeaponData data in stat.data.datas)
            {
                data.attackDistance.UpdateCount = 0;
                data.attackSpeed.UpdateCount = 0;
                data.reloadSpeed.UpdateCount = 0;
                data.attackCount.UpdateCount = 0;
                data.attackDamage.UpdateCount = 0;
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
        item.AllowUpdate = EditorGUILayout.ToggleLeft("���������� ? ", item.AllowUpdate);
        if (item.AllowUpdate)
        {

            item.updatePriceMultipler = EditorGUILayout.FloatField("��������� ��������� ��������� ", item.updatePriceMultipler);
            item.MinValue = EditorGUILayout.FloatField("����������� �������� ", item.MinValue);
            item.MaxValue = EditorGUILayout.FloatField("������������ �������� ", item.MaxValue);
            item.MaxUpdateCount = EditorGUILayout.IntField("������������ ���������� ��������� ", item.MaxUpdateCount);
            item.UpdateCount = EditorGUILayout.IntField("���������� ��������� ", Mathf.Clamp(item.UpdateCount, 0, item.MaxUpdateCount));
            item.UpdateValue();
            EditorGUILayout.LabelField("�������� �������� ���������: " + item.Value);
        }
        else
        {
            item.MaxValue = EditorGUILayout.FloatField("�������� �� ��������� ", item.MaxValue);
            item.MinValue = item.MaxValue;
        }
    }
    private void DrawItem(ref WeaponStatInt item, string name)
    {
        item.AllowUpdate = EditorGUILayout.ToggleLeft("���������� ? ", item.AllowUpdate);
        if (item.AllowUpdate)
        {
            item.updatePriceMultipler = EditorGUILayout.FloatField("��������� ��������� ��������� ", item.updatePriceMultipler);
            item.MinValue = EditorGUILayout.IntField("����������� �������� ", item.MinValue);
            item.MaxValue = EditorGUILayout.IntField("������������ �������� ", item.MaxValue);
            item.MaxUpdateCount = EditorGUILayout.IntField("������������ ���������� ��������� ", item.MaxUpdateCount);
            item.UpdateCount = EditorGUILayout.IntField("���������� ��������� ", Mathf.Clamp(item.UpdateCount, 0, item.MaxUpdateCount));
            item.UpdateValue();
            EditorGUILayout.LabelField("�������� �������� ���������: " + item.Value);
        }
        else
        {
            item.MaxValue = EditorGUILayout.IntField("�������� �� ��������� ", item.MaxValue);
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

    private bool copy;
    private WeaponData copyData;

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
        DropItem(ref attackCount, "���������� ������");
        DropItem(ref patronCount, "�������� � ��������");
        DropItem(ref patronStorage, "�������� � �����");
        DropItem(ref shootingAccuracy, "��������");

        if (GUILayout.Button("���������� ��� �����"))
        {
            copyData = GetData()[index];
            copy = true;
        }
        else if (copy && GUILayout.Button("�������� ��� �����"))
        {
            attackDistance = copyData.attackDistance;
            reloadSpeed = copyData.reloadSpeed;
            attackSpeed = copyData.attackSpeed;
            attackDamage = copyData.attackDamage;
            attackCount = copyData.attackCount;
            patronCount = copyData.patronCount;
            patronStorage = copyData.patronStorage;
            shootingAccuracy = copyData.shootingAccuracy;

        }


        stat.data.datas[index] = new WeaponData()
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








[CustomEditor(typeof(WeaponShopStat))]
public class WeaponStatShopEditor : Editor
{
    WeaponShopStat stat;
    private void SetWeaponsList(ref List<WeaponData> datas)
    {
        if (datas == null) datas = new List<WeaponData>();

        for (int i = 0; i < Enum.GetValues(typeof(WeaponType)).Length; i++)
        {

            if (datas.Count == i)
            {
                stat.hidenPlayerStat.Add(false);
                WeaponData data = new WeaponData();
                data.weaponType = (WeaponType)Enum.GetValues(typeof(WeaponType)).GetValue(i);

                datas.Add(new WeaponData());
                datas[i] = new WeaponData()
                {

                };
                Debug.Log("����");
            }
            else
            {
                WeaponData data = datas[i];
                data.weaponType = (WeaponType)Enum.GetValues(typeof(WeaponType)).GetValue(i);

                datas[i] = data;
            }
        }
    }

    private bool edit;

    private ref List<WeaponData> GetData()
    {
        return ref stat.data.datas;
    }

    private void Awake()
    {
        stat = (WeaponShopStat)target;
    }

    public override void OnInspectorGUI()
    {
        GUI.skin = Resources.Load<GUISkin>("skin");
        serializedObject.Update();
        SetWeaponsList(ref stat.data.datas);
        if (editMode)
        {
            WeaponStatDraw(editIndex);
            GUILayout.BeginHorizontal(new GUIStyle(GUI.skin.box));
            WeaponType ty = GetData()[editIndex].weaponType;

            if (GUILayout.Button("������� " + @"""" + ty.ToString() + @""""))
            {

                editIndex = 0;
                editMode = false;
                edit = true;
            }
            //else if (GUILayout.Button("�������� ��������� "))
            //{

            //}
            GUILayout.EndHorizontal();
        }
        else
        {
            if (edit)
            {
                GUILayout.Label("������ ��� �������������:");

                for (int i = 0; i < GetData().Count; i++)
                {

                    GUILayout.BeginHorizontal(new GUIStyle(GUI.skin.box));
                    if (GUILayout.Button("�������: " + stat.data.datas[i].weaponType.ToString()))
                    {
                        editIndex = i;
                        editMode = true;
                        edit = false;
                        break;
                    }

                    GUILayout.EndHorizontal();
                }
            }
            else
            {
                if (GUILayout.Button("������������� ������"))
                {
                    edit = true;
                }
            }

            if (edit)
            {
                if (GUILayout.Button("<--- ��������"))
                {
                    edit = false;
                }
            }
        }

        if (GUILayout.Button("�������� ����������"))
        {
            foreach (WeaponData data in stat.data.datas)
            {
                data.attackDistance.UpdateCount = 0;
                data.attackSpeed.UpdateCount = 0;
                data.reloadSpeed.UpdateCount = 0;
                data.attackCount.UpdateCount = 0;
                data.attackDamage.UpdateCount = 0;
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
        item.AllowUpdate = EditorGUILayout.ToggleLeft("���������� ? ", item.AllowUpdate);
        if (item.AllowUpdate)
        {

            item.updatePriceMultipler = EditorGUILayout.FloatField("��������� ��������� ��������� ", item.updatePriceMultipler);
            item.MinValue = EditorGUILayout.FloatField("����������� �������� ", item.MinValue);
            item.MaxValue = EditorGUILayout.FloatField("������������ �������� ", item.MaxValue);
            item.MaxUpdateCount = EditorGUILayout.IntField("������������ ���������� ��������� ", item.MaxUpdateCount);
            item.UpdateCount = EditorGUILayout.IntField("���������� ��������� ", Mathf.Clamp(item.UpdateCount, 0, item.MaxUpdateCount));
            item.UpdateValue();
            EditorGUILayout.LabelField("�������� �������� ���������: " + item.Value);
        }
        else
        {
            item.MaxValue = EditorGUILayout.FloatField("�������� �� ��������� ", item.MaxValue);
            item.MinValue = item.MaxValue;
        }
    }
    private void DrawItem(ref WeaponStatInt item, string name)
    {
        item.AllowUpdate = EditorGUILayout.ToggleLeft("���������� ? ", item.AllowUpdate);
        if (item.AllowUpdate)
        {
            item.updatePriceMultipler = EditorGUILayout.FloatField("��������� ��������� ��������� ", item.updatePriceMultipler);
            item.MinValue = EditorGUILayout.IntField("����������� �������� ", item.MinValue);
            item.MaxValue = EditorGUILayout.IntField("������������ �������� ", item.MaxValue);
            item.MaxUpdateCount = EditorGUILayout.IntField("������������ ���������� ��������� ", item.MaxUpdateCount);
            item.UpdateCount = EditorGUILayout.IntField("���������� ��������� ", Mathf.Clamp(item.UpdateCount, 0, item.MaxUpdateCount));
            item.UpdateValue();
            EditorGUILayout.LabelField("�������� �������� ���������: " + item.Value);
        }
        else
        {
            item.MaxValue = EditorGUILayout.IntField("�������� �� ��������� ", item.MaxValue);
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

    private bool copy;
    private WeaponData copyData;

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
        DropItem(ref attackCount, "���������� ������");
        DropItem(ref patronCount, "�������� � ��������");
        DropItem(ref patronStorage, "�������� � �����");
        DropItem(ref shootingAccuracy, "��������");

        if (GUILayout.Button("���������� ��� �����"))
        {
            copyData = GetData()[index];
            copy = true;
        }
        else if (copy && GUILayout.Button("�������� ��� �����"))
        {
            attackDistance = copyData.attackDistance;
            reloadSpeed = copyData.reloadSpeed;
            attackSpeed = copyData.attackSpeed;
            attackDamage = copyData.attackDamage;
            attackCount = copyData.attackCount;
            patronCount = copyData.patronCount;
            patronStorage = copyData.patronStorage;
            shootingAccuracy = copyData.shootingAccuracy;

        }


        stat.data.datas[index] = new WeaponData()
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





#endif