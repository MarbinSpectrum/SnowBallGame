using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageSelect_Btn : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI    stageText;
    [SerializeField] private Image[]            starList;
    [SerializeField] private Sprite[]           starImg;
    [SerializeField] private Image              lockImg;
    private int stageNum;

    public void Set_Data(int pStageNum)
    {
        stageNum = pStageNum;
        Set_UI();
    }

    public void Set_UI()
    {
        gameObject.SetActive(true);

        stageText.text = stageNum.ToString();

        int starCnt = GameMng.playData.stageStar[stageNum - 1];
        for (int i = 0; i < starList.Length; i++)
        {
            if (i < starCnt)
            {
                //Ȱ��ȭ
                starList[i].sprite = starImg[1];
            }
            else
            {
                //��Ȱ��ȭ
                starList[i].sprite = starImg[0];
            }
        }

        //�������� ���
        //�ذ��� �������� + 1��ŭ ǥ��
        lockImg.gameObject.SetActive(GameMng.playData.clearStage + 1 < stageNum);
    }

    public void GoStage()
    {
        if (GameMng.playData.clearStage + 1 < stageNum)
            return;

        StageMng.GoStage(stageNum);
    }
}
