
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


        public WeaponData[] enemuWeapons = null;

        public WeaponData[] selectedWeapons = new WeaponData[1]
        {
            new WeaponData()
            {
                weaponCount = 1,
                weaponType = WeaponType.None
            }
        };

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
    public struct WeaponData
    {
        public int weaponCount;



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
    }
}
