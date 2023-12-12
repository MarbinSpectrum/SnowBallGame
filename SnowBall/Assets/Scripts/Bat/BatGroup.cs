using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatGroup : MonoBehaviour
{
    [SerializeField] private Bat_SoundTrigger soundTrigger;

    public void AnimationEnd()
    {
        soundTrigger.InitEvent();
    }
}
