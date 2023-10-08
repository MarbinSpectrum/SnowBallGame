using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect : MonoBehaviour
{
    [SerializeField] private StageSelect_Btn    stageBtn;
    [SerializeField] private RectTransform      stagePanel;

    private void Start()
    {
        SetUI();
    }

    public void SetUI()
    {
        for (int i = 1; i <= StageMng.stageCnt; i++)
        {
            StageSelect_Btn stageSlot = Instantiate(stageBtn, stagePanel);
            stageSlot.Set_Data(i);
        }
    }
}
