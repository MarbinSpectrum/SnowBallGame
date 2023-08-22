using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeObj : SoundObj
{
    public override void Play()
    {
        SoundMng.RunSE(clip);
    }
}
