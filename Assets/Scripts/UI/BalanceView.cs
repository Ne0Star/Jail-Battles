using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalanceView : MonoBehaviour
{
    [SerializeField] private Text balanceText;

    private void Update()
    {
        balanceText.text = YG.YandexGame.savesData.money + " ";
    }
}
