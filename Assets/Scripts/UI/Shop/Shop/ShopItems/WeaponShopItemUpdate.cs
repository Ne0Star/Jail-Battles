using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class WeaponShopItemUpdate : MonoBehaviour
{

    public event System.Action<WeaponShopItemUpdate> updateClick;

    public Sprite fullUpdateIco, updateIco;
    public Image updateImage;

    [SerializeField] private Button updateBTN;
    [SerializeField] private Text label, byuPrice;

    [SerializeField] private GameObject updateParent, ratingParent, fullUpdateParent, valueLabel, valueIco;
    [SerializeField] private RectTransform ratingRect;

    [SerializeField] private float startWidth = 0;

    private void Awake()
    {
        startWidth = ratingRect.GetWidth();

    }

    //private void ReLang(string lang)
    //{
    //    LocalizerData textData = GameManager.Instance.GetValueByKey(nameKey);
    //    label.text = textData.resultText;
    //    label.font = textData.resultFont;
    //}

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


    public void SetItem(float min, float max)
    {
        LocalizerData textData = GameManager.Instance.GetValueByKey(nameKey);
        label.text = textData.resultText;
        label.font = textData.resultFont;
        updateBTN.onClick?.RemoveAllListeners();

        updateBTN.onClick?.AddListener(() =>
        {
            updateClick?.Invoke(this);
        });
        Set(ratingRect, min, max);

    }
    [SerializeField] private string nameKey;
    public void SetStat(WeaponItem item, WeaponData data, ref WeaponData realData, ref WeaponStatInt stat, string n)
    {
        currentIntStat = stat;
        nameKey = n;
        LocalizerData textData = GameManager.Instance.GetValueByKey(nameKey);
        label.text = textData.resultText;
        label.font = textData.resultFont;

        if (stat.UpdateCount == stat.MaxUpdateCount)
        {
            Debug.Log("Улучшено на максимум");

            valueLabel.SetActive(false);
            valueIco.SetActive(false);
            updateBTN.interactable = false;
            updateImage.sprite = fullUpdateIco;

            Set(ratingRect, stat.UpdateCount, stat.MaxUpdateCount);
            return;
        }
        updateBTN.interactable = true;
        updateImage.sprite = updateIco;
        valueLabel.SetActive(true);
        valueIco.SetActive(true);

        updateBTN.onClick?.RemoveAllListeners();
        WeaponStatInt stat_ = stat;
        WeaponData real = realData;
        var result = Mathf.RoundToInt((stat.UpdateCount + 1) * data.updatePrice * stat.updatePriceMultipler);
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
                ApplyUpdate(result);
                SetStat(item, data, ref real, ref stat_, n);
            }
            else
            {
                Debug.Log("Недостаточно средств для покупки улучшения");
            }
        });
        Set(ratingRect, stat.UpdateCount, stat.MaxUpdateCount);
    }

    [SerializeField] private WeaponStatFloat currentFloatStat;
    [SerializeField] private WeaponStatInt currentIntStat;
    private void ApplyUpdate(int price)
    {

        currentFloatStat.Update();
        currentIntStat.Update();
        YG.YandexGame.savesData.money -= price;
        YG.YandexGame.SaveProgress();

        currentIntStat = new WeaponStatInt();
        currentFloatStat = new WeaponStatFloat();
    }

    public void SetStat(WeaponItem item, WeaponData data, ref WeaponData realData, ref WeaponStatFloat stat, string n)
    {
        currentFloatStat = stat;
        nameKey = n;
        LocalizerData textData = GameManager.Instance.GetValueByKey(nameKey);

        label.text = textData.resultText;
        label.font = textData.resultFont;

        if (stat.UpdateCount == stat.MaxUpdateCount)
        {
            Debug.Log("Улучшено на максимум");

            valueLabel.SetActive(false);
            valueIco.SetActive(false);
            updateBTN.interactable = false;
            updateImage.sprite = fullUpdateIco;

            Set(ratingRect, stat.UpdateCount, stat.MaxUpdateCount);
            return;
        }
        updateBTN.interactable = true;
        updateImage.sprite = updateIco;
        valueLabel.SetActive(true);
        valueIco.SetActive(true);

        updateBTN.onClick?.RemoveAllListeners();
        WeaponStatFloat stat_ = stat;
        WeaponData real = realData;
        var result = Mathf.RoundToInt((stat.UpdateCount + 1) * data.updatePrice * stat.updatePriceMultipler);
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
                ApplyUpdate(result);
                SetStat(item, data, ref real, ref stat_, n);
            }
            else
            {
                Debug.Log("Недостаточно средств для покупки улучшения");
            }
        });
        Set(ratingRect, stat.UpdateCount, stat.MaxUpdateCount);
    }

    private void Set(RectTransform target, float value, float maxValue)
    {
        float coof = Mathf.InverseLerp(0f, maxValue, value);
        float total = Mathf.Lerp(value, startWidth, coof);
        target.SetWidth(total);
    }

    public Button UpdateBTN { get => updateBTN; }
    public Text Label { get => label; }
}
