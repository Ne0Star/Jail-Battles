using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class WeaponShopItemUpdate : MonoBehaviour
{
    [SerializeField] private Button updateBTN;
    [SerializeField] private Text label, byuPrice;

    [SerializeField] private GameObject updateParent, ratingParent, fullUpdateParent;
    [SerializeField] private RectTransform ratingRect;

    [SerializeField] private float startWidth = 0;

    private void Awake()
    {
        startWidth = ratingRect.GetWidth();
    }

    private void Start()
    {

        Invoke(nameof(SetStartWidth), 1f);

    }

    private void OnDisable()
    {

    }

    private void SetStartWidth()
    {
        
    }

    public void SetStat(WeaponItem item, ref WeaponStatInt stat, string name)
    {
        WeaponStatInt stat_ = stat;
        updateBTN.onClick?.RemoveAllListeners();

        LocalizerData textData = GameManager.Instance.GetValueByKey(name);
        label.text = textData.resultText;
        label.font = textData.resultFont;

        var total = item.Price * (stat.UpdateCount + 1);
        var result = Mathf.RoundToInt((total - item.Price) * stat.MaxUpdateCount * stat.PriceFactor);
        byuPrice.text = result + " ";

        updateBTN.onClick?.AddListener(() =>
        {
            if (stat_.UpdateCount == stat_.MaxUpdateCount)
            {
                Debug.Log("Улучшено на максимум");
                return;
            }
            if (YG.YandexGame.savesData.money - result >= 0)
            {
                stat_.Update();
                YG.YandexGame.savesData.money -= result;
                SetStat(item, ref stat_, name);
                YG.YandexGame.SaveProgress();
            }
            else
            {
                Debug.Log("Недостаточно средств для покупки улучшения");
            }
        });
        Set(ratingRect, stat_.Value, stat_.MaxValue);
    }

    public void SetStat(WeaponItem item, ref WeaponStatFloat stat, string name)
    {
        WeaponStatFloat stat_ = stat;
        updateBTN.onClick?.RemoveAllListeners();

        LocalizerData textData = GameManager.Instance.GetValueByKey(name);
        label.text = textData.resultText;
        label.font = textData.resultFont;

        var total = item.Price * (stat.UpdateCount + 1);
        var result = Mathf.RoundToInt((total - item.Price) * stat.MaxUpdateCount * stat.PriceFactor);
        byuPrice.text = result + " ";

        updateBTN.onClick?.AddListener(() =>
        {
            if (stat_.UpdateCount == stat_.MaxUpdateCount)
            {
                Debug.Log("Улучшено на максимум");
                return;
            }
            if (YG.YandexGame.savesData.money - result >= 0)
            {
                stat_.Update();
                YG.YandexGame.savesData.money -= result;
                SetStat(item, ref stat_, name);
                YG.YandexGame.SaveProgress();
            }
            else
            {
                Debug.Log("Недостаточно средств для покупки улучшения");
            }
        });
        Set(ratingRect, stat.Value, stat.MaxValue);
    }

    private void Set(RectTransform target, float value, float maxValue)
    {
        float precent = value * 100 / maxValue;
        float coof = startWidth / 100f;
        float totalWidth = coof * precent;
        Debug.Log(precent + " " + coof + " " + totalWidth + " " + value + " " + maxValue);
        target.SetWidth(totalWidth);
    }

    public Button UpdateBTN { get => updateBTN; }
    public Text Label { get => label; }
}
