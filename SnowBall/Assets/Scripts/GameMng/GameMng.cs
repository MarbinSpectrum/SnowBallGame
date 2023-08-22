using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMng : MonoBehaviour
{
    #region Singleton class: GameMng

    public static GameMng Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }

    }

    #endregion

    private Vector3 startPos;
    private Vector3 cameraPos;
    [SerializeField] private  Ball    ball;
    [SerializeField] private  Camera  cam;

    private void Start()
    {
        startPos = ball.transform.position;
        cameraPos = cam.transform.position;
    }

    public static void ReStart() => Instance.ReStartGame();
    private void ReStartGame()
    {
        //공위치 시작점으로
        ball.transform.position = startPos;
        ball.transform.rotation = Quaternion.identity;
        ball.transform.localScale = Vector3.one;

        //카메라 위치 시작점으로
        cam.transform.position = cameraPos;

        ControlMng.lnit();
    }
}
