using UnityEngine;

public enum BattleDroneState
{
    ENTER = 0,
    MOVING = 1,
    ATTACKING = 2,
    EXIT = 3
}

public class BattleDroneBrain : MonoBehaviour
{
    private BattleDroneState state = BattleDroneState.ENTER;

    public BattleDroneState State
    {
        get { return state; }
        set
        {
            state = value;
            if (state == BattleDroneState.MOVING && curSkill == null)
            {
                curSkill = battleDroneSkills[Random.Range(0, battleDroneSkills.Length)];
            }
        }
    }

    private BossSkill[] battleDroneSkills;

    [SerializeField] private float maxHp = 100;
    [SerializeField] private float attack = 10;
    [SerializeField] private float moveSpeed = 5;
    private float curHp = 0;

    private Rigidbody2D rb;
    private Vector2 curMovingDir;

    private BossSkill curSkill;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        battleDroneSkills = GetComponents<BossSkill>();
    }

    private void Update()
    {
        if (state == BattleDroneState.MOVING && curSkill.isAttacking == false)
        {
            //rb.velocity = curMovingDir * moveSpeed; 

            for (int i = 0; i < battleDroneSkills.Length; i++)
            {
                if (battleDroneSkills[i].AttackDesire() == true && battleDroneSkills[i] != curSkill)
                {
                    battleDroneSkills[i].UseSkill();
                    curSkill = battleDroneSkills[i];
                    State++;
                    break;
                }
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            GoNextState();
        }
    }

    public void GoNextState()
    {
        State++;
    }
    public void Init()
    {
        state = BattleDroneState.ENTER;
        curSkill = battleDroneSkills[0];
        curHp = maxHp;
    }
}
