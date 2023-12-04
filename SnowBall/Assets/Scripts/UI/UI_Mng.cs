using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Mng : MonoBehaviour, MngInter
{
    public static UI_Mng Instance;

    [SerializeField] private EventSystem eventSystem;


    public void LoadMng()
    {
        Instance = this;
        eventSystem.enabled = true;
    }



    public static void LoadUI(UI pUI)
    {
        string uiPath = string.Format("UI/{0}", pUI.ToString());
        GameObject uiObj = Resources.Load<GameObject>(uiPath);

        Instantiate(uiObj);
    }
}
