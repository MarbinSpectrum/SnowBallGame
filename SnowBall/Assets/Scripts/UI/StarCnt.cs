using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarCnt : MonoBehaviour
{
    [SerializeField] private Image[]     starList;
    [SerializeField] private Sprite[]    starImg;

    public void UpdateUI()
    {
        int value = GameMng.nowStar;
        for(int i = 0; i < starList.Length; i++)
        {
            if(i < value)
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
    private void Update()
    {
        UpdateUI();
    }
}
