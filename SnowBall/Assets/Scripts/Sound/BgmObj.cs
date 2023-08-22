using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmObj : SoundObj
{
    public override void Play()
    {
        SoundMng.RunBGM(clip);
    }
}
