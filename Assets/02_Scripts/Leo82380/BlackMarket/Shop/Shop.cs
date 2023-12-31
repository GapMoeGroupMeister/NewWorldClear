using UnityEngine;
using DG.Tweening;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject weaponPanel;
    [SerializeField] private GameObject description;
    [SerializeField] private Ease ease;

    [SerializeField] private float duration;

    private bool isMoving = false;
    public bool IsMoving => isMoving;
    
    private void OnEnable()
    {
        isMoving = true;
        weaponPanel.transform.position = new Vector3(-15f, 0f, 0f);
        weaponPanel.transform.DOMoveX(-7.5f, duration).SetEase(ease).OnComplete(() => { isMoving = false; });
        description.SetActive(false);
    }

    /// <summary>
    /// ���ư���
    /// </summary>
    public void Main()
    {
        mainPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
