using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageMng : MonoBehaviour, MngInter
{
    public static StageMng Instance;

    public static int stageCnt { private set; get; }

    public void LoadMng()
    {
        Instance = this;

        LoadStageData();
    }

    private static Dictionary<int, string> stageData = new Dictionary<int, string>();
    private static Dictionary<string, int> stageNum  = new Dictionary<string, int>();

    public static void LoadStageData()
    {
        //,자 형식으로 저장된 csv파일을 읽는다.
        TextAsset textAsset = Resources.Load<TextAsset>("StageData/stage");
        if (textAsset == null)
            return;

        //줄을 나눈다.
        string[] rows = textAsset.text.Split('\n');
        List<string> rowList = new List<string>();
        for (int i = 0; i < rows.Length; i++)
        {
            if (string.IsNullOrEmpty(rows[i]))
            {
                //아무것도 없는 객체
                continue;
            }
            string row = rows[i].Replace("\r", string.Empty);
            row = row.Trim();
            rowList.Add(row);
        }

        for (int r = 1; r < rowList.Count; r++)
        {
            string[] values = rowList[r].Split(',');

            int keyValue = int.Parse(values[0].Replace('\r', ' ').Trim());
            string stageName = values[1].Replace('\r', ' ').Trim();
            stageData[keyValue] = stageName;
            stageNum[stageName] = keyValue;
            stageCnt++;
        }
    }

    public static int GetStageNumber()
    {
        string stageName = SceneManager.GetActiveScene().name;
        return GetStageNumber(stageName);
    }

    public static int GetStageNumber(string stageName)
    {
        if (stageNum.ContainsKey(stageName))
            return stageNum[stageName];
        return 0;
    }

    public static string GetStageName(int idx)
    {
        if (stageData.ContainsKey(idx))
            return stageData[idx];
        return "";
    }

    public static void GoNextStage()
    {
        int stageNum = GetStageNumber() + 1;
        string stageName = GetStageName(stageNum);
        SceneManager.LoadScene(stageName);
    }

    public static void GoStage(int idx)
    {
        if (stageData.ContainsKey(idx) == false)
            return;
        string stageName = GetStageName(idx);
        SceneManager.LoadScene(stageName);
    }
}
