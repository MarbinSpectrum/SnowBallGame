using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SunEffect : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField, Range(0, 1)] private float lerpValue;


    private float thisValue;
    private float targetValue;
    private MaterialPropertyBlock mpb;
    private static SunEffect sunEffect;

    private void Awake()
    {
        sunEffect = this;
    }

    private void Update()
    {
        SetLerp();
        SetLerpValue();
    }

    private void SetLerp()
    {
        //러프값 수정
        if (mpb == null)
            mpb = new MaterialPropertyBlock();
        if (renderer == null)
            return;
        if (thisValue == lerpValue)
            return;
        thisValue = lerpValue;
        mpb.SetFloat("_LerpValue", lerpValue);
        renderer.SetPropertyBlock(mpb);
    }

    private void SetLerpValue()
    {
        if (Mathf.Abs(lerpValue - targetValue) < 0.001f)
            lerpValue = targetValue;
        else
            lerpValue = Mathf.Lerp(lerpValue, targetValue, 0.01f);
    }

    public static void RunEffect()
    {
        if (sunEffect == null)
            return;
        sunEffect.targetValue = 0.6f;
    }

    public static void OffEffect()
    {
        if (sunEffect == null)
            return;
        sunEffect.targetValue = 0;
    }
}
