using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class GameManager : OneSingleton<GameManager>
{
    [SerializeField] private Font resultFont;
    [SerializeField] private List<YG.WeaponData> defaultWeaponDatas = new List<YG.WeaponData>();

    [SerializeField] private string currentLang = "ru";
    [SerializeField] private TextAsset scvLanguages;
    [SerializeField] private InfoYG infoYg;

    public List<WeaponData> DefaultWeaponDatas { get => defaultWeaponDatas; }
    public Font ResultFont { get => resultFont; set => resultFont = value; }

    private void Awake()
    {
        GameManager.Instance = this;
        if (!scvLanguages)
            scvLanguages = Resources.Load<TextAsset>("TranslateCSV");

        YandexGame.SwitchLanguage(currentLang);
        YandexGame.SwitchLangEvent += ReLang;

    }

    private void ReLang(string s)
    {
        Font[] fonts = infoYg.fonts.GetFontsByLanguageName(currentLang);
        Font resultFont = null;
        bool font = false;
        if (fonts != null)
            foreach (Font f in fonts)
            {
                if (f != null)
                {
                    resultFont = f;
                    font = true;
                    break;
                }
            }
        if (!font)
        {
            foreach (Font f in infoYg.fonts.defaultFont)
            {
                if (f != null)
                {
                    resultFont = f;
                    break;
                }
            }
        }
        this.resultFont = resultFont;
    }

    public WeaponData GetDefaultDataByType(WeaponType type)
    {
        WeaponData result = new WeaponData();
        foreach (WeaponData data in defaultWeaponDatas)
        {
            if (data.weaponType == type)
            {
                result = data;
                break;
            }
        }
        return result;
    }

    [SerializeField] private YG.WeaponData stat;
    [SerializeField] bool setAllStat;
    private void OnDrawGizmos()
    {
        if (setAllStat)
        {
            for (int i = 0; i < defaultWeaponDatas.Count; i++)
            {
                YG.WeaponData data = defaultWeaponDatas[i];

                WeaponType type = data.weaponType;
                data = stat;
                data.patronCount.SetUpdate(Random.Range(0, 9));
                data.patronStorage.SetUpdate(Random.Range(0, 9));
                data.attackDamage.SetUpdate(Random.Range(0, 9));
                data.weaponType = type;

                defaultWeaponDatas[i] = data;
            }
            setAllStat = false;
        }
    }

    private int GetLangIndex(string lang)
    {
        switch (lang)
        {
            case "ru": return 0;
            case "en": return 1;
            case "tr": return 2;
            case "az": return 3;
            case "be": return 4;
            case "he": return 5;
            case "hy": return 6;
            case "ka": return 7;
            case "et": return 8;
            case "fr": return 9;
            case "kk": return 10;
            case "ky": return 11;
            case "lt": return 12;
            case "lv": return 13;
            case "ro": return 14;
            case "tg": return 15;
            case "tk": return 16;
            case "uk": return 17;
            case "uz": return 18;
            case "es": return 19;
            case "pt": return 20;
            case "ar": return 21;
            case "id": return 22;
            case "ja": return 23;
            case "it": return 24;
            case "de": return 25;
            case "hi": return 26;
        }
        return -1;

    }
    public LocalizerData GetValueByKey(string key)
    {
        string[] values = CSVManager.ImportTransfersByKey(scvLanguages, 30, key);

        if (values == null)
        {
            return new LocalizerData
            {
                resultFont = null,
                resultText = "null :)"
            };
        };
        int index = GetLangIndex(currentLang);
        string result = values[index];
        if (!infoYg || infoYg.fonts == null)
        {
            return new LocalizerData
            {
                resultFont = null,
                resultText = "null :)"
            };
        }

        return new LocalizerData
        {
            resultFont = this.resultFont,
            resultText = result
        };
    }




#if UNITY_EDITOR

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ScreenShoot();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            YandexGame.savesData.money += 1000;
            YandexGame.SaveProgress();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Text[] texts = FindObjectsOfType<Text>();
            foreach (Text text in texts)
            {
                text.text = text.text;
            }
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Text[] texts = FindObjectsOfType<Text>();
            foreach (Text text in texts)
            {
                text.font = text.font;
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            YandexGame.ResetSaveProgress();
        }
    }

    public void ScreenShoot()
    {
        ScreenCapture.CaptureScreenshot("ScreenShoot.jpg");
    }
#else

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            YandexGame.ResetSaveProgress();
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            YandexGame.savesData.money += 1000;
            YandexGame.SaveProgress();
        }
                if (Input.GetKeyDown(KeyCode.T))
        {
            Text[] texts = FindObjectsOfType<Text>();
            foreach(Text text in texts)
            {
                text.text = text.text;
            }
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Text[] texts = FindObjectsOfType<Text>();
            foreach (Text text in texts)
            {
                text.font = text.font;
            }
        }
    }

#endif

}
public class LocalizerData
{
    public string resultText;
    public Font resultFont;
}