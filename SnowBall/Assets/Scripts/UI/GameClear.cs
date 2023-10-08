using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClear : MonoBehaviour
{
    [SerializeField] private Image[]    starList;
    [SerializeField] private Sprite[]   starImg;

    private void Start()
    {
        SetUI();
    }

    public void SetUI()
    {
        int value = GameMng.nowStar;
        for (int i = 0; i < starList.Length; i++)
        {
            if (i < value)
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
    }

    public void ReStartGame() => GameMng.ReStart();

    public void SelectStage() => UI_Mng.LoadUI(UI.StageSelect);

    public void GoNextStage() => StageMng.GoNextStage();
}
