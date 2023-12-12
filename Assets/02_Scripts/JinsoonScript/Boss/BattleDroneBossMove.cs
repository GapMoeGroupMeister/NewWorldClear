using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDroneBossMove : MonoBehaviour
{
    private Transform player; //�÷��̾� Transform �޾ƿ� ���߿� �ٲ��״ϱ�
    private BattleDroneBrain battleDroneBrain;
    private Animator anim;

    private bool isMoving = false;
    private Vector3 moveDir;
    [SerializeField] private float moveSpeeed = 5;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        battleDroneBrain = GetComponent<BattleDroneBrain>();
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if (isMoving && battleDroneBrain.State != BattleDroneState.ATTACKING)
        {
            transform.position += moveDir.normalized * moveSpeeed * Time.deltaTime;
            MoveAnimation();

            if ((player.transform.position - transform.position).magnitude > 30)
            {
                isMoving = false;
                battleDroneBrain.State = BattleDroneState.ENTER;
            }
        }
        else
        {
            anim.SetBool("MoveLeft", false);
            anim.SetBool("MoveRight", false);
        }
    }

    public void MoveStart()
    {
        moveDir = player.transform.position - transform.position;
        isMoving = true;
    }

    private void MoveAnimation()
    {
        if (moveDir.x > 0.1f)
        {
            anim.SetBool("MoveLeft", true);
            anim.SetBool("MoveRight", false);
        }
        else if (moveDir.x < -0.1f)
        {
            anim.SetBool("MoveLeft", false);
            anim.SetBool("MoveRight", true);
        }
        else
        {
            anim.SetBool("MoveLeft", false);
            anim.SetBool("MoveRight", false);
        }
    }
}
