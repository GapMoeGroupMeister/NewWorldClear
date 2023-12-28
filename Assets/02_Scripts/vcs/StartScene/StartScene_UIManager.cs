using System;
using EasyJson;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartScene_UIManager : MonoBehaviour
{
    [Header("Status Gauge")]
    [SerializeField] private Image Gauge_Health;
    [SerializeField] private Image Gauge_Hungery;
    [SerializeField] private Image Gauge_Thirsty;
    
    [Space]
    [Header("Player Image")]
    [SerializeField] private Image playerImage;
    [SerializeField] private Sprite[] playerImages;
    
    [Space]
    [SerializeField] private SaveInfo saveInfo;
    [SerializeField] TMP_Text dayText;

    private void RefreshStatusGauge()
    {
        StatusManager.Instance.LoadPlayerStatus();
        Gauge_Health.fillAmount = (float)StatusManager.Instance.PlayerStatus.health / PlayerStatus.Calc_HealthMax(StatusManager.Instance.PlayerStatus.levelHealth);
        Gauge_Hungery.fillAmount = (float)StatusManager.Instance.PlayerStatus.hungry / 100;
        Gauge_Thirsty.fillAmount = (float)StatusManager.Instance.PlayerStatus.thirsty / 100;
    }

    private void RefreshPlayerImage()
    {
        // Injured
        if (Gauge_Health.fillAmount < 0.4f)
        {
            playerImage.sprite = playerImages[2];
        }
        // Despise
        else if (Gauge_Hungery.fillAmount < 0.4f || Gauge_Thirsty.fillAmount < 0.4f)
        {
            playerImage.sprite = playerImages[1];
        }
        // default
        else
        {
            playerImage.sprite = playerImages[0];
        }
    }

    private void DayUpdate()
    {
        saveInfo = DBManager.Get_UserInfo();
        dayText.text = "생존 " + saveInfo.day + "일";
    }
}