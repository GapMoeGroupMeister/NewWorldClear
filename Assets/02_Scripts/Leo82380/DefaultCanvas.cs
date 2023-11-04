using UnityEngine;

public class DefaultCanvas : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject requestPanel;

    /// <summary>
    /// 상점 들어가기
    /// </summary>
    public void Shop()
    {
        shopPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 의뢰 들어가기
    /// </summary>
    public void Request()
    {
        requestPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
