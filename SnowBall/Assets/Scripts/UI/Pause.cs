using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public void Start()
    {
        GameMng.PauseGame(true);
    }

    public void ReStartGame()
    {
        GameMng.ReStart();
    }


    public void PlayGame()
    {
        GameMng.PauseGame(false);
        Destroy(this.gameObject);
    }

    public void SelectStage() => UI_Mng.LoadUI(UI.StageSelect);
}
