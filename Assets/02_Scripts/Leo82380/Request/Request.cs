using UnityEngine;

public class Request : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;

    /** <summary>
     * ���ư���
     * </summary>
     */
    public void Main()
    {
        mainPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
