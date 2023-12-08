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
    public static bool pause;
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
        Scene scene = SceneManager.GetActiveScene();
        PauseGame(false);
        if (scene.name == "Title")
        {
            UI_Mng.LoadUI(UI.Title);
        }
        else
        {
            nowStar = 0;
            ControlMng.lnit();
            UI_Mng.LoadUI(UI.InGame);
        }
    }

    public static void PauseGame(bool state)
    {
        if(state)
        {
            pause = true;
            Time.timeScale = 0;
        }
        else
        {
            pause = false;
            Time.timeScale = 1;
        }
    }

    public static void GameOver()
    {
        UI_Mng.LoadUI(UI.GameOver);
    }

    public static void ReStart()
    {
        GameMng.PauseGame(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void GameClear()
    {
        int stageNum = StageMng.GetStageNumber();

        //해당 스테이지별 갱신
        int maxStar = playData.stageStar[stageNum - 1];
        playData.stageStar[stageNum - 1] = Mathf.Max(maxStar, nowStar);

        //클리어 스테이지 갱신
        playData.clearStage = Mathf.Max(playData.clearStage, stageNum);

        SavePlayData();

        UI_Mng.LoadUI(UI.GameClear);
    }
}
