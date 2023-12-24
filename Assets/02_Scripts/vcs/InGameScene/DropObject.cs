using System;
using System.Collections;
using UnityEngine;
using EaseFunc;
using UnityEngine.Events;


public abstract class DropObject : MonoBehaviour
{
    
    private bool PlayerTargeted = false;

    [Range(0.5f, 5f)] [SerializeField] protected float detectRange = 0.5f;
    [SerializeField] protected bool isStatic;
    [SerializeField] protected float followSpeed = 1f;
    [Range(1f, 5f)] [SerializeField] protected float curveSensitive;
    protected Transform followTarget;
    protected Vector2 currentDirection;

    [SerializeField]
    protected bool isTargeted;
    [Space(20f)]
    [Tooltip("아이템을 주울때 실행되는 이벤트 액션")]
    public UnityEvent PickUpAction;

    

    protected void Update_Check()
    {
        CheckCollisionPlayer();
        if (!isStatic)
        {
            Trace();

        }
    }

    private void Trace()
    {
        if (!isTargeted)
        {
            DetectPlayer();
        }
        else
        {
            Follow();
        }
        

    }
    
    /**
     * <summary>
     * 플레이어가 추적범위안에 들어왔는지 확인
     * </summary>
     */
    protected void DetectPlayer()
    {
        // 플레이어 레이어 추가되면 설정
        Collider2D[] hits =  Physics2D.OverlapCircleAll(transform.position, detectRange);

        foreach (Collider2D collider in hits)
        {
            if (collider.CompareTag("Player"))
            {
                followTarget = collider.transform;
                isTargeted = true;
            }
        }
    }

    protected void Follow()
    {
        Vector2 targetDir = (followTarget.position - transform.position).normalized;
        currentDirection = (currentDirection * curveSensitive + targetDir).normalized;
        transform.position += (Vector3)currentDirection * Time.deltaTime * followSpeed;
    }

    /**
     * <summary>
     * 획득기능 수행
     * </summary>
     */
    public abstract void Get();

    /**
     * <summary>
     * 플레이어와 충돌감지, 충돌시 Get메서드 실행
     * </summary>
     */
    protected void CheckCollisionPlayer()
    {
        Collider2D[] detected = Physics2D.OverlapCircleAll(transform.position, 0.12f);
        foreach (Collider2D target in detected)
        {
            if (target.CompareTag("Player"))
            {
                PickUpAction?.Invoke();
                Get();
            }
        }
    }
    
    /**
     * <summary>
     * 입력된 방향으로 날아가게 한다
     * </summary>
     */
    public void AddForce(Vector2 dir, float power)
    {
        StartCoroutine(ForceRoutine(dir, power));
    }

    private IEnumerator ForceRoutine(Vector2 dir, float power)
    {
        float before = power;
        while (power > 0)
        {
            transform.Translate(dir * power * Time.deltaTime);
            power -= before * Time.deltaTime;
            yield return null;
        }
        
    }
}