using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JinsoonPlayerForTest : MonoBehaviour
{
    private float speed = 7;

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(x, y) * speed * Time.deltaTime;
    }

}
