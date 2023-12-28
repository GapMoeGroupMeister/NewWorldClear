using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AlphaMaskTrapSkill : BossSkill
{
    [SerializeField] private GameObject trap;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public override bool AttackDesire()
    {
        if (skillCoolTimeDown > 0) return false;
        return true;
    }

    protected override void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            UseSkill();
        }
    }

    public override void UseSkill()
    {
        //애니메이션 실행해주고 애니메이션 끝나는 타이밍에 함정 설치되는 함수 실행
        SetTrap();

        skillCoolTimeDown = skillCoolTime;
    }

    public void SetTrap()
    {
        AlphaBossTrap t = Instantiate(trap).GetComponent<AlphaBossTrap>();

        Vector2 trapPos = new Vector2(Random.Range(-2, 2), Random.Range(-2, 2));
        trapPos += (Vector2)transform.position;

        t.Init(trapPos);
    }
}
