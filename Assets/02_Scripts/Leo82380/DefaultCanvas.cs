using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCanvas : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject requestPanel;

    public void Shop()
    {
        shopPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Request()
    {
        requestPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
