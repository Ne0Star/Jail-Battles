
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


        public WeaponPlayerData[] selectedWeapons = new WeaponPlayerData[1]
        {
            new WeaponPlayerData()
            {
                counts = 1,
                updateCount = 0,
                wapon = WeaponType.None
            }
        };

        public WeaponPlayerData[] byuWeapons = new WeaponPlayerData[1]
        {
            new WeaponPlayerData()
            {
                counts = 1,
                updateCount = 0,
                wapon = WeaponType.None
            }
        };
    }

    [System.Serializable]
    public struct WeaponPlayerData
    {
        public int counts;
        public int updateCount;
        public WeaponType wapon;
    }

}
