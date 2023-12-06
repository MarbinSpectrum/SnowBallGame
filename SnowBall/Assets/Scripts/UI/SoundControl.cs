using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    [SerializeField] private RectTransform[] btn;

    private void Awake()
    {
        SoundMuteBtn(!SoundMng.soundMute);
    }

    public void SoundMuteBtn(bool state)
    {
        if (state)
        {
            SoundMng.SetMute(false);
            btn[0].gameObject.SetActive(true);
            btn[1].gameObject.SetActive(false);
        }
        else
        {
            SoundMng.SetMute(true);
            btn[0].gameObject.SetActive(false);
            btn[1].gameObject.SetActive(true);
        }
    }
}
