using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ball Data", menuName = "Scriptable Object/Ball Data", order = int.MaxValue)]
public class Ball_Data : ScriptableObject
{
    [Header("ȸ���ӵ�")]
    public float rotateValue;

    [Header("��¼ӵ�")]
    public float meltValue;

    [Header("Ŀ���¼ӵ�")]
    public float erectionValue;

    [Header("�ٿ")]
    public float bounciness;

    [Header("�������")]
    public float friction;
}
