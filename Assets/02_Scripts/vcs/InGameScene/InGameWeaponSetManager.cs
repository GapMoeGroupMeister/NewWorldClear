using System.Collections;
using System.Collections.Generic;
using EasyJson;
using UnityEngine;

public class InGameWeaponSetManager : MonoBehaviour
{
    [SerializeField] private WeaponSO[] weapons;

    [SerializeField] private PlayerWeapon playerWeapon;

    public ItemSlot thisWeaponItem;
    // Start is called before the first frame update
    void Awake()
    {
        SetWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetWeapon()
    {
        thisWeaponItem = EasyToJson.FromJson<ItemSlot>("InGameWeapon");
        foreach (WeaponSO weapon in weapons)
        {
            if (weapon.id == thisWeaponItem.item.id)
            {
                playerWeapon.testWeapon = weapon;

            }
        }
    }
}
