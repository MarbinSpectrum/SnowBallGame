using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Mng : MonoBehaviour, MngInter
{
    public static UI_Mng Instance;
    public void LoadMng() => Instance = this;

    public static void LoadUI(UI pUI)
    {
        string uiPath = string.Format("UI/{0}", pUI.ToString());
        GameObject uiObj = Resources.Load<GameObject>(uiPath);

        Instantiate(uiObj);
    }
}
