using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class Description : MonoBehaviour
{
    private Weapon2 weapon2;

    private bool isOn = false;
    public Weapon2 Weapon2 { get; set; }
    public bool IsOn { get { return isOn; } }

    public void Buy()
    {
        if (Weapon2.IsSoldOut) return;

        Weapon2.SoldOut.SetActive(true);
        Weapon2.IsSoldOut = true;

        Cancel();
    }

    public void Cancel()
    {
        transform.DOScale(0f, 0.5f).OnComplete(() => gameObject.SetActive(false));
        Weapon2 = null;
        isOn = false;
    }
    private void OnEnable()
    {
        isOn = true;
    }
    private void OnDisable()
    {
        isOn = false;
    }
}
