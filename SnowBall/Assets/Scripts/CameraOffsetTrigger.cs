using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraOffsetTrigger : MonoBehaviour
{
    [SerializeField] private Vector3                    offset;
    [SerializeField] private CinemachineVirtualCamera   vcam;

    private CinemachineTransposer transposer;
    private bool isTrigger = false;
    private bool run = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTrigger == true)
            return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            isTrigger = true;
            run = true;
        }
    }

    private void Start()
    {
        transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
    }

    private void Update()
    {
        if (run == false)
            return;

        Vector3 pos = transposer.m_FollowOffset;
        transposer.m_FollowOffset = Vector3.Lerp(pos, offset, 0.01f);

        if(Vector3.Distance(transposer.m_FollowOffset,offset) < 0.1f)
        {
            transposer.m_FollowOffset = offset;
            run = false;
        }
    }
}
