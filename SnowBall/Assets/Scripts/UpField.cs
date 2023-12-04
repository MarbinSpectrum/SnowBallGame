using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpField : MonoBehaviour
{
    [SerializeField] private Ball ball;

    private void Update()
    {
        if (ball == null)
            return;

        if(ball.transform.position.y > transform.position.y)
            transform.position = new Vector3(transform.position.x, ball.transform.position.y, transform.position.z);

    }
}
