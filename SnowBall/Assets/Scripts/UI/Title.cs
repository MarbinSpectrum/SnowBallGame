using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    public void SelectStage() => UI_Mng.LoadUI(UI.StageSelect);
}
