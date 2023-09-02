using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClear : MonoBehaviour
{
    public void ReStartGame() => GameMng.ReStart();

    public void GoNextStage() => StageMng.GoNextStage();
}
