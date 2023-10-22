using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObj : MonoBehaviour
{
    [SerializeField] private Transform followObj;

    private void Update()
    {
        if (followObj == null)
            return;
        Vector2 pos = followObj.position;
        transform.position = pos;
    }
}
