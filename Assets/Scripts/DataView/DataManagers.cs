using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine.UI;

public class DataManagers : MonoBehaviour
{
    private List<int> gameIDs = new List<int>();
    private List<int> gameLevels = new List<int>();
    private List<string> playDates = new List<string>();

    private string serverURL = "http://localhost:3000/dateLoad/load";

    void Start()
    {
        StartCoroutine(GetGameData());
    }

    IEnumerator GetGameData()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(serverURL))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("데이터를 가져오는 데 실패하였습니다: " + www.error);
            }
            else
            {
                string jsonData = www.downloadHandler.text;
                ProcessGameData(jsonData);
            }
        }
    }

    void ProcessGameData(string jsonData)
    {
        List<Dictionary<string, object>> dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);

        foreach (var data in dataList)
        {
            int gameID = int.Parse(data["gameID"].ToString());
            int gameLevel = int.Parse(data["gameLevel"].ToString());
            string playDate = data["playDate"].ToString();

            gameIDs.Add(gameID);
            gameLevels.Add(gameLevel);
            playDates.Add(playDate);
        }

        for (int i = 0; i < gameIDs.Count; i++)
        {
            Debug.Log($"Game {i + 1}: ID={gameIDs[i]}, Level={gameLevels[i]}, Date={playDates[i]}");
        }
    }
}

