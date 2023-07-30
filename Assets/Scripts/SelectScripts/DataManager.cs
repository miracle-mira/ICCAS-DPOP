using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    private List<int> gameIDs = new List<int>();
    private List<int> gameLevels = new List<int>();
    private List<string> playDates = new List<string>();

    private string serverURL = "http://localhost:3000/gameData/gamedata";

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

            // ButtonType 스크립트를 가지고 있는 GameObject를 찾습니다.
            // gameID와 gameLevel을 찾습니다.
            GameObject buttonObject = GameObject.Find($"Button_{gameIDs[i]}_{gameLevels[i]}");
            if (buttonObject != null)
            {
                ButtonType buttonType = buttonObject.GetComponent<ButtonType>();
                if (buttonType != null)
                {
                    // 버튼이 어떤 게임 타입인지 지정해줍니다.
                    if (gameIDs[i] == 11)
                    {
                        buttonType.currentType = BTNType.Game1;
                    }
                    else if (gameIDs[i] == 12)
                    {
                        buttonType.currentType = BTNType.Game2;
                    }

                    // ButtonType 스크립트의 gameLevel 값을 설정합니다.
                    buttonType.gameLevel = gameLevels[i];

                    // 버튼에 표시할 Text 컴포넌트를 찾아서 값을 설정합니다.
                    Text buttonText = buttonObject.GetComponentInChildren<Text>();
                    if (buttonText != null)
                    {
                        buttonText.text = $"Game {gameIDs[i]} - Level {gameLevels[i]}";
                    }
                }
            }
        }
    }
}