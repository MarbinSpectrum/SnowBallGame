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
    [SerializeField] private Button             btn;
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
                //활성화
                starList[i].sprite = starImg[1];
            }
            else
            {
                //비활성화
                starList[i].sprite = starImg[0];
            }
        }

        //스테이지 잠금
        //해결한 스테이지 + 1만큼 표시
        if(GameMng.playData.clearStage + 1 < stageNum)
        {
            lockImg.gameObject.SetActive(true);
            btn.enabled = false;

        }
        else
        {
            lockImg.gameObject.SetActive(false);
            btn.enabled = true;
        }
    }

    public void GoStage()
    {
        if (GameMng.playData.clearStage + 1 < stageNum)
            return;

        StageMng.GoStage(stageNum);
    }
}
