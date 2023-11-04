using UnityEngine;

public class DefaultCanvas : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject requestPanel;

    /// <summary>
    /// ���� ����
    /// </summary>
    public void Shop()
    {
        shopPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// �Ƿ� ����
    /// </summary>
    public void Request()
    {
        requestPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
