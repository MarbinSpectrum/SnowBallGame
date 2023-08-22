using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SoundObj : MonoBehaviour
{
    [SerializeField] protected AudioClip clip;
    [SerializeField] protected bool      playOnAwake;

    protected virtual void Awake() 
    {
        if (playOnAwake)
            Play();
    }

    public virtual void Play() { }

}
