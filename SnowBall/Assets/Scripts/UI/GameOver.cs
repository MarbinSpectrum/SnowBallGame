using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void SelectStage() => UI_Mng.LoadUI(UI.StageSelect);

    public void ReStartGame() => GameMng.ReStart();
}
