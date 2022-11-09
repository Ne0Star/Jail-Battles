
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        public bool isFirstSession = true;
        public string language = "ru";
        public bool feedbackDone;
        public bool promptDone;

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
    }
}
