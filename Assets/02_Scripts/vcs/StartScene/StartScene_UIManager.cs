using System;
using System.Collections;
using EasyJson;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    [SerializeField] private GameObject adventureButton;
    [SerializeField] private GameObject restButton;
    [SerializeField] private Image fadeImage;
    [SerializeField] private TMP_Text dayFadeText;

    private void OnEnable()
    {
        StatusManager.Instance.LoadPlayerStatus();
        RefreshStatusGauge();
    }

    private void OnDestroy()
    {
        SceneOutSave();
    }

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
        dayText.text = "<size=42>생존</size> " + saveInfo.day + "일";
        if (saveInfo.adventureCount > 0)
        {
            adventureButton.SetActive(true);
            restButton.SetActive(false);
        }
        else
        {
            adventureButton.SetActive(false);
            restButton.SetActive(true);
        }
    }

    public void Rest()
    {
        StartCoroutine(RestCoroutine());
    }

    private IEnumerator RestCoroutine()
    {
        RestSet(true);
        fadeImage.DOFade(1f, 0.5f);
        dayFadeText.DOFade(1f, 0.5f);
        
        dayFadeText.text = "생존 " + saveInfo.day + "일차";
        saveInfo.day++;
        yield return new WaitForSeconds(1f);
        dayFadeText.text = "생존 " + saveInfo.day + "일차";
        
        yield return new WaitForSeconds(1.5f);
        fadeImage.DOFade(0f, 0.5f);
        dayFadeText.DOFade(0f, 0.5f);
        StatusManager.Instance.PlayerStatus.hungry -= 10;
        StatusManager.Instance.PlayerStatus.thirsty -= 10;
        StatusManager.Instance.SavePlayerStatus();
        RefreshStatusGauge();
        yield return new WaitForSeconds(0.5f);
        
        RestSet(false);
        saveInfo.adventureCount = 1;
        DayUpdate();
        
    }

    private void RestSet(bool set)
    {
        if (set)
        {
            fadeImage.gameObject.SetActive(true);
            dayFadeText.gameObject.SetActive(true);
            fadeImage.color = Color.clear;
            dayFadeText.color = new Color(1, 1, 1, 0f);
        }
        else
        {
            fadeImage.gameObject.SetActive(false);
            dayFadeText.gameObject.SetActive(false);
            fadeImage.color = new Color(0, 0, 0, 225f);
        }
    }

    public void SceneOutSave()
    {
        StatusManager.Instance.SavePlayerStatus();
        DBManager.Save_userInfo(saveInfo);        
    }
}