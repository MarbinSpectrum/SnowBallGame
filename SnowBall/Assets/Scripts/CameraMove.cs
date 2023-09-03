using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private bool followX;
    [SerializeField] private bool followY;

    private void Update()
    {
        Vector2 pos = transform.position;
        if (followY)
            pos.y = Camera.main.transform.position.y;
        if(followX)
            pos.x = Camera.main.transform.position.x;
        transform.position = pos;

    }
}
