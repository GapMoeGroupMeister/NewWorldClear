using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodManager : MonoSingleton<FoodManager>
{
    [SerializeField] private List<FoodSO> foodList = new();
    [SerializeField] private TextMeshProUGUI foodDescription;
    private ItemSlot slot;

    public void FoodDescription_Update()
    {
        foreach (var item in foodList)
        {
            if (item.foodID == slot.item.id)
            {
                foodDescription.text = "HP + " + item.HP + "\n" +
                                       "허기 + " + item.hunger + "\n" +
                                       "목마름 + " + item.thirsty + "\n";
            }
        }
    }

    public ItemSlot Slot
    {
        get => slot;
        set => slot = value;
    }

    public void Eat()
    {
        foreach (var item in foodList)
        {
            if (item.foodID == slot.item.id)
            {
                StatusManager.Instance.LoadPlayerStatus();
                StatusManager.Instance.PlayerStatus.health += item.HP;
                StatusManager.Instance.PlayerStatus.hungry += item.hunger;
                StatusManager.Instance.PlayerStatus.thirsty += item.thirsty;
                StatusManager.Instance.SavePlayerStatus();
                slot.durability--;
            }
        }
    } 
}
