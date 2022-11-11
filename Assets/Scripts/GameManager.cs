using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class GameManager : OneSingleton<GameManager>
{
    [SerializeField] private string currentLang = "ru";
    [SerializeField] private TextAsset scvLanguages;
    [SerializeField] private InfoYG infoYg;

    private void Awake()
    {
        GameManager.Instance = this;
        scvLanguages = Resources.Load<TextAsset>("TranslateCSV");
        YandexGame.SwitchLanguage(currentLang);
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
        string[] values = CSVManager.ImportTransfersByKey("TranslateCSV", 27, key);

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

        return new LocalizerData
        {
            resultFont = resultFont,
            resultText = result
        };
    }


#if UNITY_EDITOR

    public void ScreenShoot()
    {
        ScreenCapture.CaptureScreenshot("ScreenShoot.jpg");
    }

#endif

}
public struct LocalizerData
{
    public string resultText;
    public Font resultFont;
}