using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartScene_ItemDivider : MonoBehaviour
{
    private Slider slider;
    private Button plusBtn;
    private Button minusBtn;
    private TextMeshProUGUI amountTxt;

    private int maxAmount = 0;
    private int curAmount = 0;

    public int CurAmount => curAmount;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        plusBtn = transform.Find("Plus").GetComponent<Button>();
        minusBtn = transform.Find("Minus").GetComponent<Button>();
        amountTxt = transform.Find("Amount").GetComponent<TextMeshProUGUI>();
    }

    public void SetDivide(int maxAmount)
    {
        this.maxAmount = maxAmount;
        slider.maxValue = maxAmount;
        slider.value = maxAmount;
        curAmount = maxAmount;
        amountTxt.SetText($"({curAmount}/{maxAmount})");
    }

    public void OnSliderValueChange()
    {
        curAmount = (int)slider.value;

        slider.value = curAmount;
        amountTxt.SetText($"({curAmount}/{maxAmount})");
    }

    public void OnPlus()
    {
        curAmount++;
        curAmount = Mathf.Clamp(curAmount, 0, maxAmount);

        slider.value = curAmount;
        amountTxt.SetText($"({curAmount}/{maxAmount})");
    }
    public void OnMinus()
    {
        curAmount--;
        curAmount = Mathf.Clamp(curAmount, 0, maxAmount);

        slider.value = curAmount;
        amountTxt.SetText($"({curAmount}/{maxAmount})");
    }
}
