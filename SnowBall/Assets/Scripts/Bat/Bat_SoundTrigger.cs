using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_SoundTrigger : SoundObj
{
    [SerializeField] private bool actEvent = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(actEvent)
        {
            return;
        }

        int layer = collision.transform.gameObject.layer;
        
        if (LayerMask.LayerToName(layer) == "Ball")
        {
            actEvent = true;
            Play();
        }
    }

    public override void Play()
    {
        SoundMng.RunSE(clip);
    }

    public void InitEvent()
    {
        actEvent = false;
    }
}
