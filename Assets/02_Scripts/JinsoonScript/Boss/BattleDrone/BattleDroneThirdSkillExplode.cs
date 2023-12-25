using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDroneThirdSkillExplode : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("쳐맞음ㅠ");
        }
    }

    public void OnEndAnimation()
    {
        Destroy(gameObject);    //풀링해야디
    }
}
