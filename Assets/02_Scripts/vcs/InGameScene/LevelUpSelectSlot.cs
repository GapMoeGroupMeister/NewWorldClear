using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelUpSelectSlot : MonoBehaviour
{
    public LevelUpOption currentLevelUpOption;
    
    [SerializeField] private TextMeshProUGUI BtnLevel;
    [SerializeField] private TextMeshProUGUI BtnOption;
    [SerializeField] private GameObject EdgeLight;

    private void Awake()
    {
        BtnLevel = transform.Find("Level").GetComponent<TextMeshProUGUI>();
        BtnOption = transform.Find("OptionName").GetComponent<TextMeshProUGUI>();
        EdgeLight = transform.Find("MaskColor").gameObject;

    }


    public void Select()
    {
        switch (currentLevelUpOption)
        {
            case LevelUpOption.Dam:
                if (GameManager.Instance._LevelManager.StatusEnforceLevel_Damage >= 20)
                {
                    break;
                }
                GameManager.Instance._PlayerController.levelUpDamage += (8 + Random.Range(1, 3));
                GameManager.Instance._LevelManager.StatusEnforceLevel_Damage++;
                break;
            case LevelUpOption.Heal:
                float max = GameManager.Instance._PlayerController._maxHp;
                GameManager.Instance._PlayerController._currentHp += (int)(max * 0.3f);
                GameManager.Instance._PlayerController._currentHp =
                    Mathf.Clamp(GameManager.Instance._PlayerController._currentHp, 0, max);
                break;
            case LevelUpOption.Spd:
                if (GameManager.Instance._LevelManager.StatusEnforceLevel_Speed >= 20)
                {
                    break;
                }
                GameManager.Instance._PlayerController._moveSpeed += 0.3f;
                GameManager.Instance._LevelManager.StatusEnforceLevel_Speed++;

                break;
            case LevelUpOption.AtkSpd:
                if (GameManager.Instance._LevelManager.StatusEnforceLevel_AttackSpeed >= 20)
                {
                    break;
                }
                GameManager.Instance._PlayerController.levelUpDelay -= 0.02f;
                GameManager.Instance._LevelManager.StatusEnforceLevel_AttackSpeed++;

                break;
            case LevelUpOption.MaxHp:
                if (GameManager.Instance._LevelManager.StatusEnforceLevel_MaxHp >= 20)
                {
                    break;
                }
                GameManager.Instance._PlayerController._maxHp += (8 + Random.Range(1, 9));
                GameManager.Instance._LevelManager.StatusEnforceLevel_MaxHp++;

                break;
            case LevelUpOption.EpdRg:
                if (GameManager.Instance._LevelManager.StatusEnforceLevel_ExpandRange >= 20)
                {
                    break;
                }
                GameManager.Instance._ItemGetRange.ExpandRange(0.05f);
                GameManager.Instance._LevelManager.StatusEnforceLevel_ExpandRange++;

                break;
        }

        GameManager.Instance._LevelManager.OffLevelUpDetailUI();
    }

    /**
     * <summary>
     * 옵션을 랜덤으로 선택한다
     * </summary>
     */
    public void SetOption()
    {
        LevelUpOption levelUpOption = (LevelUpOption)Random.Range(0,6);
        currentLevelUpOption = levelUpOption;
        EdgeLight.SetActive(false);
        BtnLevel.text = LevelToString();
        BtnOption.text = OptionToString();
        
    }

    private string OptionToString()
    {
        string result = "";
        switch (currentLevelUpOption)
        {
            case LevelUpOption.Dam:
                result = "+ 공격력";
                break;
            case LevelUpOption.Heal:
                result = "<size=54>체력 30% 즉시회복</size>";
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
            
            case LevelUpOption.EpdRg:
                result = "+ 습득범위";
                break;
        }

        return result;
    }
    
    private string LevelToString()
    {
        int level = 0;
        
        switch (currentLevelUpOption)
        {
            case LevelUpOption.Dam:
                level = GameManager.Instance._LevelManager.StatusEnforceLevel_Damage;
                break;
            case LevelUpOption.Heal:
                level = -1;
                break;
            case LevelUpOption.Spd:
                level = GameManager.Instance._LevelManager.StatusEnforceLevel_Speed;
                break;
            case LevelUpOption.AtkSpd:
                level = GameManager.Instance._LevelManager.StatusEnforceLevel_AttackSpeed;
                break;
            case LevelUpOption.MaxHp:
                level = GameManager.Instance._LevelManager.StatusEnforceLevel_MaxHp;
                break;
            
            case LevelUpOption.EpdRg:
                level = GameManager.Instance._LevelManager.StatusEnforceLevel_ExpandRange;
                break;
        }

        string result = level.ToString();
        if (level >= 20)
        {
            result += "<color=\"orange\">Max</color>";
            EdgeLight.SetActive(true);
        }else if (level == -1)
        {
            result = "<size=54>즉시사용</size>";
        }
        return result;
    }
    
}
