using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScrollMap : MonoBehaviour
{
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;
    [SerializeField] private Transform cameraPos;

    [Space(50)]

    [SerializeField, Range(0, 1)] private float time = 0;
    [SerializeField] private float waitTime = 3f;
    [SerializeField] private float durationTime;

    private void Update()
    {
        if (startPos == null || endPos == null)
            return;


        if(waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }
        else
        {
            time += Time.deltaTime / durationTime;
            time = Mathf.Min(time, 1);
        }

        Vector3 pos = Vector3.Lerp(startPos.position, endPos.position,time);
        cameraPos.position = pos;
    }
}
