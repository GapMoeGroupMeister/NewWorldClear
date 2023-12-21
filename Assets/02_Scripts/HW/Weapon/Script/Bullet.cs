using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 dir;

    private void Update()
    {
        transform.position += dir * 30f * Time.deltaTime;
    }
}
