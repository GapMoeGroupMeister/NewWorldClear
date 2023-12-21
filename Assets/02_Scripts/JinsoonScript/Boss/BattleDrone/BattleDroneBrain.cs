using UnityEditor.Rendering;
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
                Debug.Log(battleDroneSkills.Length);
                curSkill = battleDroneSkills[Random.Range(0, battleDroneSkills.Length)];
            }
        }
    }

    private BossSkill[] battleDroneSkills;
    private BattleDroneBossMove movement;
 
    [SerializeField] private float maxHp = 100;
    [SerializeField] private float attack = 10;
    [SerializeField] private float moveSpeed = 5;
    private float curHp = 0;
    private float skillCool = 5;
    private float skillCoolDown = 0;

    private Rigidbody2D rb;
    private Vector2 curMovingDir;

    private BossSkill curSkill;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<BattleDroneBossMove>();
        battleDroneSkills = GetComponents<BossSkill>();
    }

    private void Update()
    {
        if (skillCoolDown > 0) skillCoolDown -= Time.deltaTime;

        if (state == BattleDroneState.MOVING && curSkill.isAttacking == false)
        {
            int i = Random.Range(0, battleDroneSkills.Length);

            if (battleDroneSkills[i].AttackDesire() == true && battleDroneSkills[i] != curSkill && skillCoolDown <= 0)
            {
                movement.MoveStop();
                curSkill = battleDroneSkills[i];
                curSkill.UseSkill();
                skillCoolDown = skillCool;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            GoNextState();
            movement.MoveStart();
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
