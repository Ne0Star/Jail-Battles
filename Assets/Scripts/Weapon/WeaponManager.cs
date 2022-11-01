using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BatchObject<T>
{
    public int count;
    public T prefab;
}
[System.Serializable]
public class BatchWeapon : BatchObject<Weapon>
{
    public int chance;
}


public class WeaponManager : MonoBehaviour
{



    [SerializeField] private List<BatchWeapon> weaponsBathces = new List<BatchWeapon>();
    [SerializeField] private List<Weapon> allWeapons;

    private void OnEnable()
    {
        foreach (BatchWeapon list in weaponsBathces)
        {
            for (int i = 0; i < list.count; i++)
            {
                Weapon weapon = Instantiate(list.prefab);
                weapon.Free = true;
                weapon.gameObject.SetActive(false);
                allWeapons.Add(weapon);
            }
        }
    }


    public Weapon GetRandomWeaponByType(WeaponType type, bool ignoreCount)
    {
        List<Weapon> weapons = GetFreeWeapons(type, ignoreCount);
        if (weapons == null || weapons.Count == 0) return null;
        int index = Random.Range(0, weapons.Count - 1);
        Weapon result = weapons[index];
        return result;
    }

    /// <summary>
    /// ≈сли игнорировать количество, то проход по всем
    /// </summary>
    /// <param name="type"></param>
    /// <param name="ignoreCount"></param>
    /// <returns></returns>
    public List<Weapon> GetFreeWeapons(WeaponType type, bool ignoreCount)
    {
        List<Weapon> result = new List<Weapon>();
        if (ignoreCount)
        {
            foreach (Weapon weapon in allWeapons)
            {
                if (weapon.Free && weapon.WeaponType == type)
                    result.Add(weapon);
            }
        }
        else
        {
            int index = 0;
            for (int i = 1; i < allWeapons.Count; i++)
            {
                Weapon weapon = allWeapons[i];
                bool add = false;
                foreach (BatchWeapon bw in weaponsBathces)
                {
                    if (bw.prefab.GetType() == weapon.GetType())
                    {
                        add = Random.Range(0, 100) >= bw.chance;
                        break;
                    }
                }
                if (add)
                {
                    Debug.Log("ќружие есть");
                    if (weapon.Free && weapon.WeaponType == type)
                    {
                        if (index >= 1)
                        {
                            result.Add(weapon);
                        }
                        index++;
                    }
                }
            }
        }
        return result;
    }

    ///// <summary>
    ///// ¬ернЄт оружие по его типу, если игнорировать количество, то получит то оружие которое предназначено дл€ игрока
    ///// </summary>
    ///// <param name="type"></param>
    ///// <param name="ignoreCount"></param>
    ///// <returns></returns>
    //public Weapon GetFreeWeapon(WeaponType type, bool ignoreCount)
    //{
    //    Weapon result = null;
    //    if (ignoreCount)
    //    {
    //        foreach (Weapon weapon in allWeapons)
    //        {
    //            if (weapon.gameObject.activeSelf)
    //                if (weapon.WeaponType == type)
    //                {
    //                    result = weapon;
    //                    break;
    //                }
    //        }
    //    }
    //    else
    //    {
    //        int index = 0;
    //        for (int i = 1; i < allWeapons.Count; i++)
    //        {
    //            Weapon weapon = allWeapons[i];
    //            if (weapon.gameObject.activeSelf)
    //                if (weapon.WeaponType == type)
    //                {
    //                    if (index >= 1)
    //                    {
    //                        result = weapon;
    //                        break;
    //                    }
    //                    index++;
    //                }
    //        }
    //    }
    //    return result;
    //}

}
