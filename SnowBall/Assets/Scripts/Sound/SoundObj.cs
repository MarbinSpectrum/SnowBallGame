using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SoundObj : MonoBehaviour
{
    [SerializeField] protected AudioClip clip;
    [SerializeField] protected bool      playOnStart;

    protected virtual void Start() 
    {
        if (playOnStart)
            Play();
    }

    public virtual void Play() { }

}
