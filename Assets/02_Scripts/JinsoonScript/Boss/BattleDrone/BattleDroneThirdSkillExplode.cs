using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDroneThirdSkillExplode : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("�ĸ�����");
        }
    }

    public void OnEndAnimation()
    {
        Destroy(gameObject);    //Ǯ���ؾߵ�
    }
}
