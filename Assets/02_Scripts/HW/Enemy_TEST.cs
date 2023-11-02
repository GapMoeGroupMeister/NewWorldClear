using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_TEST : MonoBehaviour
{
    [SerializeField]
    Transform _playerTrm;
    float hp = 10;
    private void Awake()
    {
        _playerTrm = FindObjectOfType<PlayerController>().transform;
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _playerTrm.position, Time.deltaTime);
    }

    public void Damage(float damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
