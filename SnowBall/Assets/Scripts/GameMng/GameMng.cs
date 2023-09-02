using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MyLib;

public class GameMng : MonoBehaviour, MngInter
{
    public static GameMng Instance;

    private static PlayData playData;


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
        //에디터와 기기에서 실행시 경로가 다르다.
        //따로 처리한다.
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
            //아무 정보도 없으니 
            //기본 데이터를 생성
            playData = new PlayData();
        }
    }

    public static void GameStart()
    {
        ControlMng.lnit();
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
        playData.clearStage = Mathf.Max(playData.clearStage, StageMng.GetStageNumber());
        SavePlayData();

        UI_Mng.LoadUI(UI.GameClear);
    }
}
