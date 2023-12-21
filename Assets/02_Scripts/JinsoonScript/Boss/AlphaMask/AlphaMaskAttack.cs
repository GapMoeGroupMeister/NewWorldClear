using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AlphaMaskAttack : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private bool isAttacking = false;
    private Transform playerTrm;

    [SerializeField] private float normalAttackCoolTime = 5;
    private float normalAttackCoolTimeDown = 0;
    private float rot = 0;
    private Quaternion rotation;
    private Vector2 targetPosion;

    [SerializeField] private GameObject bullet;

    [ColorUsage(true, true)]
    [SerializeField] private Color rColor, wColor;




    private void Awake()
    {
        playerTrm = GameObject.Find("Player").transform;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (isAttacking)
        {
            Vector2 dir = playerTrm.position - transform.position;
            rot = Mathf.LerpAngle(rot, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg, Time.deltaTime * 5);
            rotation = Quaternion.AngleAxis(rot, Vector3.forward);

            Vector3 a = transform.position + (rotation * transform.right) * 15;

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, a);
        }
        else
        {
            Vector2 dir = playerTrm.position - transform.position;
            rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Attack();
        }
    }


    public void Attack()
    {
        isAttacking = true;
        StartCoroutine("AttackRoutine");
    }

    IEnumerator AttackRoutine()
    {
        lineRenderer.enabled = true;
        for (int i = 1; i <= 10; i++)
        {
            lineRenderer.material.SetColor("_EmissionColor", rColor);
            yield return new WaitForSeconds(1f / i / 2);
            lineRenderer.material.SetColor("_EmissionColor", wColor);
            yield return new WaitForSeconds(1f / i / 2);
        }

        Instantiate(bullet, transform.position, rotation);
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
        lineRenderer.enabled = false;
        isAttacking = false;
    }
}
