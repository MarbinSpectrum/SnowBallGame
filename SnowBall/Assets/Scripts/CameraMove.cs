using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Vector2 pos;
    [SerializeField] private float diffValue;

    private void Awake()
    {
        pos = Camera.main.transform.position;
    }

    private void Update()
    {
        if(pos != (Vector2)Camera.main.transform.position)
        {
            float diffX = pos.x - Camera.main.transform.position.x;
            transform.position = new Vector3(transform.position.x + diffX*diffValue, transform.position.y, transform.position.z);
        }
    }
}
