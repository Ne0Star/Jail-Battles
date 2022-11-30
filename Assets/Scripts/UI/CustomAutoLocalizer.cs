using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CustomAutoLocalizer : MonoBehaviour
{
    private Text textUIComponent;

    string last;

    private void FixedUpdate()
    {
        if(last != textUIComponent.text)
        {
            textUIComponent.text = TranslateGoogle(GameManager.Instance.CurrentLang);
        last = textUIComponent.text;
        }
    }

    string TranslateGoogle(string translationTo = "en")
    {
        string text;

        if (textUIComponent)
            text = textUIComponent.text;
        else
        {
            Debug.LogError("(ruСообщение)Текст для перевода не найден!\n(enMessage)The text for translation was not found!");
            return null;
        }

        var url = String.Format("https://translate.google.com.hk/translate_a/single?client=gtx&dt=t&sl={0}&tl={1}&q={2}", "auto", translationTo, WebUtility.UrlEncode(text));
        UnityWebRequest www = UnityWebRequest.Get(url);
        www.SendWebRequest();
        while (!www.isDone)
        {

        }
        string response = www.downloadHandler.text;

        try
        {
            JArray jsonArray = JArray.Parse(response);
            response = jsonArray[0][0][0].ToString();
        }
        catch
        {
            response = "process error";
            StopAllCoroutines();

            Debug.LogError("(ruСообщение) Процесс не завершён! Вероятно, Вы делали слишком много запросов. В таком случае, API Google Translate блокирует доступ к переводу на некоторое время.  Пожалуйста, попробуйте позже. Не переводите текст слишком часто, чтобы Google не посчитал Ваши действия за спам" +
                        "\n" + "(enMessage) The process is not completed! Most likely, you made too many requests. In this case, the Google Translate API blocks access to the translation for a while.  Please try again later. Do not translate the text too often, so that Google does not consider your actions as spam");
        }

        return response;
    }
}
