using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering.Universal;

public class ShadowMapTrigger : MonoBehaviour
{
    [SerializeField] private Ball ball;
    [SerializeField] private Light2D light2D;
    [SerializeField] private Light2D followrLight;
    [SerializeField] private Color lightColor;


    private bool isTrigger = false;
    private bool run = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTrigger == true)
            return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            ball.shadowStage = true;
            isTrigger = true;
            run = true;
        }
    }

    private void Update()
    {
        if (run == false)
            return;

        light2D.color = Color.Lerp(light2D.color, lightColor, 0.01f);
        followrLight.color = Color.Lerp(followrLight.color, Color.white, 0.01f);
        float rDis = Mathf.Abs(light2D.color.r - lightColor.r);
        float gDis = Mathf.Abs(light2D.color.g - lightColor.g);
        float bDis = Mathf.Abs(light2D.color.b - lightColor.b);

        if (Vector3.Distance(Vector3.zero, new Vector3(rDis,gDis,bDis)) < 0.01f)
        {
            light2D.color = lightColor;
            followrLight.color = Color.white;
            run = false;
        }
    }
}
