using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDroneThirdAttackRangeWarning : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect;


    public void Attack()
    {
       Transform t = Instantiate(explosionEffect).transform;

        t.position = transform.position;
        Destroy(gameObject);//«Æ∏µ«ÿ¡‡
    }
}
