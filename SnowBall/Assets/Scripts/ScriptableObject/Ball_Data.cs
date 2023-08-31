using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ball Data", menuName = "Scriptable Object/Ball Data", order = int.MaxValue)]
public class Ball_Data : ScriptableObject
{
    [Header("회전속도")]
    public float rotateValue;

    [Header("녹는속도")]
    public float meltValue;

    [Header("커지는속도")]
    public float erectionValue;

    [Header("바운스")]
    public float bounciness;

    [Header("마찰계수")]
    public float friction;
}
