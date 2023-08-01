using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;

public class ButtonType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BTNType currentType;
    public Transform buttonScale;
    Vector3 defaultScale;
    public int gameLevelValue1 = 1; // Set default value for gameLevelValue1
    public int gameLevelValue2 = 1; // Set default value for gameLevelValue2

    private List<int> gameIDs = new List<int>();
    private List<int> gameLevels = new List<int>();
    private List<string> playDates = new List<string>();

    private string serverURL = "http://localhost:3000/gameData/gamedata";

    private void Start()
    {
        defaultScale = buttonScale.localScale;
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

        // Search for the game levels for gameIDs 11 and 12
        for (int i = 0; i < gameIDs.Count; i++)
        {
            if (gameIDs[i] == 11)
            {
                gameLevelValue1 = gameLevels[i];
            }
            else if (gameIDs[i] == 12)
            {
                gameLevelValue2 = gameLevels[i];
            }
        }
    }

    public void OnBtnClick()
    {
        switch (currentType)
        {
            case BTNType.Game1:
                SceneLoad.LoadSceneHandle("ShootingLevel"+ gameLevelValue1);
                break;
            case BTNType.Game2:
                SceneLoad.LoadSceneHandle("Miro_"+ gameLevelValue2);
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
    }
}
