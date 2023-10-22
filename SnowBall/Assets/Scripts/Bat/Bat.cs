using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        if(Random.Range(0,100) < 50)
            animator.Play("IceBat Idle 0");
        else
            animator.Play("IceBat Idle 1");
    }
}
