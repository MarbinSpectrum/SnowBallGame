using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : MonoBehaviour
{
    public void PauseStage() => UI_Mng.LoadUI(UI.Pause);
}
