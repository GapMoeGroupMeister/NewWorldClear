using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUpSelectSlot : MonoBehaviour
{
    public LevelUpOption currentLevelUpOption;
    
    [SerializeField] private TextMeshProUGUI BtnLevel;
    [SerializeField] private TextMeshProUGUI BtnOption;
    [SerializeField] private GameObject EdgeLight;



    public void Select()
    {
        switch (currentLevelUpOption)
        {
            case LevelUpOption.Dam:
                GameManager.Instance._PlayerController.damage += (8 + Random.Range(1, 3));
                break;
            case LevelUpOption.Heal:
                float max = GameManager.Instance._PlayerController._maxHp;
                GameManager.Instance._PlayerController._currentHp += (int)(max * 0.5f);
                GameManager.Instance._PlayerController._currentHp =
                    Mathf.Clamp(GameManager.Instance._PlayerController._currentHp, 0, max);
                break;
            case LevelUpOption.Spd:
                GameManager.Instance._PlayerController._moveSpeed += 0.3f;
                break;
            case LevelUpOption.AtkSpd:
                GameManager.Instance._PlayerController.attackDelay -= 0.05f;
                break;
            case LevelUpOption.MaxHp:
                GameManager.Instance._PlayerController._maxHp += (8 + Random.Range(1, 9));
                break;
        }

    }

    private void RefreshTxt(LevelUpOption levelUpOption)
    {
        currentLevelUpOption = levelUpOption;
        BtnLevel.text = levelUpOption.ToString();
        BtnOption.text = OptionToString(levelUpOption);
        switch (currentLevelUpOption)
        {
            case LevelUpOption.Dam:
                
                break;
            case LevelUpOption.Heal:
                break;
            case LevelUpOption.Spd:
                break;
            case LevelUpOption.AtkSpd:
                break;
            case LevelUpOption.MaxHp:
                break;
        }
        
    }

    private string OptionToString(LevelUpOption levelUpOption)
    {
        string result = "";
        switch (levelUpOption)
        {
            case LevelUpOption.Dam:
                result = "+ 공격력";
                break;
            case LevelUpOption.Heal:
                result = "체력 50% 즉시회복";
                break;
            case LevelUpOption.Spd:
                result = "+ 이동 속도";
                break;
            case LevelUpOption.AtkSpd:
                result = "+ 공격속도";
                break;
            case LevelUpOption.MaxHp:
                result = "+ 최대체력";
                break;
        }

        return result;
    }
    
}
