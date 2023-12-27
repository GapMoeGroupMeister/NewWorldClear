using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum LevelUpOption
{
    Dam,
    AtkSpd,
    Spd,
    MaxHp,
    Heal
}

public class LevelManager : MonoBehaviour
{
    public int level = 1;
    public int exp = 0;
    [SerializeField] private int ExpCoefficient = 10;

    private int lvlUpStack = 0;

    [Header("UI")] [SerializeField] private Image ExpGauge;
    [SerializeField] private TextMeshProUGUI ExpAmountText;
    [SerializeField] private TextMeshProUGUI LevelText;
    [SerializeField] private int ExpMax;

    [SerializeField] private UIInfo UI_LevelUp;
    [SerializeField] private TextMeshProUGUI LevelBeforeAfter;

    [SerializeField]
    private LevelUpSelectSlot[] slots;

    public int StatusEnforceLevel_Damage = 0;
    public int StatusEnforceLevel_AttackSpeed = 0;
    public int StatusEnforceLevel_Speed = 0;
    public int StatusEnforceLevel_MaxHp = 0;
    public int StatusEnforceLevel_ = 0;
    
    private void Start()
    {
        ExpMax = CalcExpMax(level);
        RefreshExp();
    }

    public void AddExp(int amount)
    {
        exp += amount;
        RefreshExp();
        IsLevelUp();
    }
    private int CalcExpMax(int level)
    {
        int max = (int)(Mathf.Pow(((level) * 50f / 49f), 2.5f));
        print(max);
        return max;
    }

    private void RefreshExp()
    {
        ExpMax = CalcExpMax(level);
        RefreshGauge();
        RefreshText();
    }
    
    private void RefreshGauge()
    {
        float t = (float)exp / ExpMax;
        ExpGauge.fillAmount = Mathf.Clamp(t, 0f, 1f);
    }

    private void RefreshText()
    {
        LevelText.text = level.ToString();
        ExpAmountText.text = "(" + exp + "/" + ExpMax + ")";
    }

    private void IsLevelUp()
    {
        if (exp >= ExpMax)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        exp -= ExpMax;
        level++;
        RefreshExp();
        IsLevelUp();
    }

    public void OffLevelUpDetailUI()
    {
        UI_LevelUp.MoveOff();
    }
    
    

    



}
