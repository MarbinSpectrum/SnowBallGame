using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMng : MonoBehaviour
{
    #region Singleton class: GameMng

    public static GameMng Instance;

    private void Awake()
    {
        Time.timeScale = 1;
        if (Instance == null)
        {
            Instance = this;
            StageData.LoadStageData();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public static void GameOver()
    {
        Time.timeScale = 0;

        GameObject gameOverUI = Resources.Load<GameObject>("UI/GameOver");
        Instantiate(gameOverUI);
    }

    public static void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void GoNext()
    {
        int stageNum = StageData.GetNumber(SceneManager.GetActiveScene().name) + 1;
        string stageName = StageData.GetStageName(stageNum);
        SceneManager.LoadScene(stageName);
    }
}
