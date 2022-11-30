
using System;
using UnityEditor;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        public bool isFirstSession = true;
        public string language = "ru";
        public bool feedbackDone;
        public bool promptDone;


        public int money = 300;

        /// <summary>
        /// Скин тело и руки
        /// </summary>
        public string body = "1";
        /// <summary>
        /// Скин голова
        /// </summary>
        public string head = "1";
        /// <summary>
        /// Скин ноги
        /// </summary>
        public string leggs = "1";
        /// <summary>
        /// Скин глаза
        /// </summary>
        public string blink = "1";

        public string[] byuBlinks = new string[1]
        {
            "1"
        };
        public string[] byuBodies = new string[1]
        {
            "1"
        };
        public string[] byuHead = new string[1]
        {
            "1"
        };
        public string[] byuLeggs = new string[1]
        {
            "1"
        };

        public PlayerEqupData equpData;

        public WeaponData[] byuWeapons = new WeaponData[1]
        {
            new WeaponData()
            {
                weaponCount = 1,
                weaponType = WeaponType.None
            }
        };
    }

    [System.Serializable]
    public struct PlayerEqupData
    {
        /// <summary>
        /// Ближний бой
        /// </summary>
        public WeaponData mele;
        /// <summary>
        /// Пистолет
        /// </summary>
        public WeaponData gun;
        /// <summary>
        /// Стрелковое двуручное
        /// </summary>
        public WeaponData machine;
        /// <summary>
        /// Метательное
        /// </summary>
        public WeaponData missile;
    }


    [System.Serializable]
    public struct WeaponData
    {
        public int price;
        public int updatePrice;
        public int weaponCount;
        public int maxWeaponCount;


        /// <summary>
        /// Дальность оружия
        /// </summary>
        public WeaponStatFloat attackDistance;
        /// <summary>
        /// Скорость перезарядки/ колдовн оружия
        /// </summary>
        public WeaponStatFloat reloadSpeed;
        /// <summary>
        /// Скорось ударов/выстрелов
        /// </summary>
        public WeaponStatFloat attackSpeed;
        /// <summary>
        /// Наносимый урон
        /// </summary>
        public WeaponStatFloat attackDamage;
        /// <summary>
        /// Количество патрон в магазине
        /// </summary>
        public WeaponStatInt patronCount;
        /// <summary>
        /// Количество атак ближним оружием
        /// </summary>
        public WeaponStatInt attackCount;
        /// <summary>
        /// Количество патрон которые можно хранить
        /// </summary>
        public WeaponStatInt patronStorage;
        /// <summary>
        /// Точность стрельбы
        /// </summary>
        public WeaponStatFloat shootingAccuracy;

        public WeaponType weaponType;
        public WeaponCategory weaponCategory;
    }
}
