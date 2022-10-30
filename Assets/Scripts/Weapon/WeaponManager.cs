using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct BatchObject<T>
{
    public int count;
    public T prefab;
}

public class WeaponManager : MonoBehaviour
{

    [SerializeField] private List<BatchObject<Weapon>> weaponsBathces = new List<BatchObject<Weapon>>();
    [SerializeField] private List<Weapon> allWeapons;

    private void Awake()
    {
        foreach (BatchObject<Weapon> list in weaponsBathces)
        {
            for (int i = 0; i < list.count; i++)
            {
                Weapon weapon = Instantiate(list.prefab, transform);
                allWeapons.Add(weapon);
            }
        }
    }

    public Weapon GetRandomWeaponByType(WeaponType type, bool ignoreCount)
    {
        //List<WeaponType> weaponTypes = new List<WeaponType>();
        //weaponTypes.Add(WeaponType.None);
        //weaponTypes.Add(WeaponType.Gun);
        //weaponTypes.Add(WeaponType.Machine);
        //weaponTypes.Add(WeaponType.Mele);

        List<Weapon> weapons = GetFreeWeapons(type, ignoreCount);

        if (weapons == null || weapons.Count == 0) return null;
        int index = Random.Range(0, weapons.Count - 1);
        return weapons[index];
    }

    public List<Weapon> GetFreeWeapons(WeaponType type, bool ignoreCount)
    {
        List<Weapon> result = new List<Weapon>();
        if (ignoreCount)
        {
            foreach (Weapon weapon in allWeapons)
            {
                if (weapon.gameObject.activeSelf)
                    if (weapon.WeaponType == type)
                    {
                        result.Add(weapon);
                    }
            }
        }
        else
        {
            int index = 0;
            for (int i = 1; i < allWeapons.Count; i++)
            {
                Weapon weapon = allWeapons[i];
                if (weapon.gameObject.activeSelf)
                    if (weapon.WeaponType == type)
                    {
                        if (index >= 1)
                        {
                            result.Add(weapon);
                        }
                        index++;
                    }
            }
        }
        return result;
    }

    /// <summary>
    /// ����� ������ �� ��� ����, ���� ������������ ����������, �� ������� �� ������ ������� ������������� ��� ������
    /// </summary>
    /// <param name="type"></param>
    /// <param name="ignoreCount"></param>
    /// <returns></returns>
    public Weapon GetFreeWeapon(WeaponType type, bool ignoreCount)
    {
        Weapon result = null;
        if (ignoreCount)
        {
            foreach (Weapon weapon in allWeapons)
            {
                if (weapon.gameObject.activeSelf)
                    if (weapon.WeaponType == type)
                    {
                        result = weapon;
                        break;
                    }
            }
        }
        else
        {
            int index = 0;
            for (int i = 1; i < allWeapons.Count; i++)
            {
                Weapon weapon = allWeapons[i];
                if (weapon.gameObject.activeSelf)
                    if (weapon.WeaponType == type)
                    {
                        if (index >= 1)
                        {
                            result = weapon;
                            break;
                        }
                        index++;
                    }
            }
        }
        return result;
    }

}
