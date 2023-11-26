using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WindField : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcam;
    [SerializeField] private Animation[] cloudAni0;
    [SerializeField] private Animation[] cloudAni1;
    [SerializeField] private float aniSpeed;
    [SerializeField] private float waitTime;
    [SerializeField] private float windTime;
    [SerializeField] private float windPower;

    private CinemachineTransposer transposer;

    private void Start()
    {
        transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
        StartCoroutine(WindCycle());
    }


    private IEnumerator WindCycle()
    {
        yield return new WaitForSeconds(waitTime);

        foreach (Animation ani in cloudAni0)
            ani["Mountain_Cloud_0"].speed = aniSpeed;
        foreach (Animation ani in cloudAni1)
            ani["Mountain_Cloud_1"].speed = aniSpeed;

        Vector3 baseOffset = transposer.m_FollowOffset;
        float t = 0;
        while(t < windTime)
        {
            yield return null;

            t += Time.deltaTime;
            ControlMng.ball.rb.AddForce(new Vector2(-15, 0), ForceMode2D.Force);
        }

        transposer.m_FollowOffset = baseOffset;

        foreach (Animation ani in cloudAni0)
            ani["Mountain_Cloud_0"].speed = 1;
        foreach (Animation ani in cloudAni1)
            ani["Mountain_Cloud_1"].speed = 1;

        StartCoroutine(WindCycle());
    }
}
