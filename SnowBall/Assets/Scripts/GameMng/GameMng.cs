using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MyLib;

public class GameMng : MonoBehaviour, MngInter
{
    public static GameMng Instance;

    public static PlayData playData { private set; get; }

    public static int nowStar;

    public void LoadMng()
    {
        Instance = this;
        LoadPlayData();
    }

    public static void SavePlayData()
    {
        string jsonData = Json.ObjectToJson(playData);

        if (Application.isEditor)
            Json.CreateJsonFile(Application.dataPath, "Resources/PlayData", jsonData);
        else
            Json.CreateJsonFile(Application.persistentDataPath, "PlayData", jsonData);
    }


    public static void LoadPlayData()
    {
        //�����Ϳ� ��⿡�� ����� ��ΰ� �ٸ���.
        //���� ó���Ѵ�.
        if (Application.isEditor)
        {
            playData = Json.LoadJsonFile<PlayData>(Application.dataPath, "Resources/PlayData");
        }
        else
        {
            playData = Json.LoadJsonFile<PlayData>(Application.persistentDataPath, "PlayData");
        }

        if (playData == null)
        {
            //�ƹ� ������ ������ 
            //�⺻ �����͸� ����
            playData = new PlayData();
        }
    }

    public static void GameStart()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Title")
        {
            UI_Mng.LoadUI(UI.Title);
        }
        else
        {
            nowStar = 0;
            ControlMng.lnit();
            UI_Mng.LoadUI(UI.StarCnt);
        }
    }

    public static void GameOver()
    {
        UI_Mng.LoadUI(UI.GameOver);
    }

    public static void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void GameClear()
    {
        int stageNum = StageMng.GetStageNumber();

        //�ش� ���������� ����
        int maxStar = playData.stageStar[stageNum - 1];
        playData.stageStar[stageNum - 1] = Mathf.Max(maxStar, nowStar);

        //Ŭ���� �������� ����
        playData.clearStage = Mathf.Max(playData.clearStage, stageNum);

        SavePlayData();

        UI_Mng.LoadUI(UI.GameClear);
    }
}
