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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public int stageValue;

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
        Instance.stageValue++;
        string stageName = string.Format("Stage{0}", Instance.stageValue);
        SceneManager.LoadScene(stageName);
    }
}
