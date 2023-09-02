using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mng : MonoBehaviour
{
    private static bool loadData = false;

    [SerializeField] private UI_Mng          uiMng;
    [SerializeField] private GameMng         gameMng;
    [SerializeField] private ControlMng      controlMng;
    [SerializeField] private SoundMng        soundMng;
    [SerializeField] private StageMng        stageMng;

    private void Awake()
    {
        if (loadData)
        {
            Destroy(gameObject);
            GameMng.GameStart();
            return;
        }
        else
        {
            loadData = true;

            stageMng.LoadMng();
            uiMng.LoadMng();
            controlMng.LoadMng();
            soundMng.LoadMng();
            gameMng.LoadMng();

            DontDestroyOnLoad(gameObject);

            GameMng.GameStart();
        }

    }
}
