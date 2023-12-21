using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusManager : MonoBehaviour
{

    [SerializeField] private PlayerStatus _playerStatus;

    public int Health
    {
        get
        {
            return _playerStatus.health;
        }
        private set { }
    }

    public int Hungry
    {
        get
        {
            return _playerStatus.hungry;
        }
        private set {}
    }

    public int Thirsty
    {
        get
        {
            return _playerStatus.thirsty;
        }
        private set {}
    }
    
    

    public void FillHealth(int amount)
    {
        _playerStatus.health += amount;
        int max = PlayerStatus.Calc_HealthMax(_playerStatus.levelHealth);
        _playerStatus.health = Mathf.Clamp(_playerStatus.health, 1, max);
    }

    public void FillHungry(int amount)
    {
        _playerStatus.hungry += amount;
        Clamp0100(ref _playerStatus.hungry);

    }

    public void FillThirsty(int amount)
    {
        _playerStatus.thirsty += amount;
        Clamp0100(ref _playerStatus.thirsty);
    }

    private void Clamp0100(ref int num)
    {
        num = Mathf.Clamp(num, 0, 100);
    }

    /**
     * <summary>
     * playerStatus를 json으로 저장함
     * </summary>
     */
    public void SavePlayerStatus()
    {
        // DBManager에 추가해야함
    }
}
