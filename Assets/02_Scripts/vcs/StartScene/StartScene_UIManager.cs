using EasyJson;
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
    [SerializeField] private Sprite[] playerImage;
    
    private PlayerStatus _playerStatus;

    private void RefreshStatusGauge()
    {
        _playerStatus = EasyToJson.FromJson<PlayerStatus>("playerStatus");
        Gauge_Health.fillAmount = (float)_playerStatus.health / PlayerStatus.Calc_HealthMax(_playerStatus.levelHealth);
        Gauge_Hungery.fillAmount = (float)_playerStatus.hungry / 100;
        Gauge_Thirsty.fillAmount = (float)_playerStatus.thirsty / 100;
    }
}