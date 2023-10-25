using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObj : MonoBehaviour
{
    [SerializeField] private Transform followObj;

    private void Update()
    {
        if (followObj == null)
        {
            gameObject.SetActive(false);
            return;
        }

        if(followObj.gameObject.activeSelf == false)
        {
            gameObject.SetActive(false);
            return;
        }
        Vector2 pos = followObj.position;
        transform.position = pos;
    }
}
