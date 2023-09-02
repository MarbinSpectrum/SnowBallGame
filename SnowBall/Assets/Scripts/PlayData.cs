using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayData
{
    public int[] stageStar;
    public int   clearStage;

    public PlayData()
    {
        stageStar = new int[StageMng.stageCnt];
        clearStage = 0;
    }
}
