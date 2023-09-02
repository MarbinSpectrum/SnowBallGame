using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMng : MonoBehaviour, MngInter
{
    public static SoundMng Instance;

    public void LoadMng()
    {
        Instance = this;
        LoadSound();
    }

    private Dictionary<string, AudioClip> seMap = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> bgmMap = new Dictionary<string, AudioClip>();

    [SerializeField] private AudioSource se;
    [SerializeField] private AudioSource bgm;

    private void LoadSound()
    {
        AudioClip[] loadSE = Resources.LoadAll<AudioClip>("Sounds/SE");
        System.Array.ForEach(loadSE, (x) => seMap[x.name] = x);

        AudioClip[] loadBGM = Resources.LoadAll<AudioClip>("Sounds/BGM");
        System.Array.ForEach(loadBGM, (x) => bgmMap[x.name] = x);
    }

    public static void RunSE(string seName) => Instance.PlaySE(seName);
    public static void RunSE(AudioClip clip) => Instance.PlaySE(clip);
    private void PlaySE(string seName)
    {
        if (seMap.ContainsKey(seName) == false)
            return;
        AudioClip clip = seMap[seName];
        PlaySE(clip);
    }
    private void PlaySE(AudioClip clip)
    {
        if (clip == null)
            return;
        se.PlayOneShot(clip);
    }

    public static void RunBGM(string bgmName) => Instance.PlayBGM(bgmName);
    public static void RunBGM(AudioClip clip) => Instance.PlayBGM(clip);
    private void PlayBGM(string bgmName)
    {
        if (bgmMap.ContainsKey(bgmName) == false)
            return;
        AudioClip clip = bgmMap[bgmName];
        PlayBGM(clip);
    }
    private void PlayBGM(AudioClip clip)
    {
        if (clip == null)
            return;
        bgm.clip = clip;
        bgm.Play();
    }
}
