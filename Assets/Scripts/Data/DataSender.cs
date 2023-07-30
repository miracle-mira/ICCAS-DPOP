using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;

public class DataSender : MonoBehaviour
{
    [System.Serializable]
    public class GameData
    {
        public string userID;
        public int gameID;
        public int gameLevel;
        public string playDate; // Added playDate field
    }

    // 버튼 클릭시 호출될 메서드
    public void OnSendButtonClick()
    {
        StartCoroutine(SendData());
    }


    IEnumerator SendData()
    {
        GameData dataToSend = new GameData
        {
            userID = "q1234",
            gameID = 11,
            gameLevel = 2
        };

        dataToSend.playDate = System.DateTime.Now.ToString("yyyy-MM-dd");

        string jsonData = JsonUtility.ToJson(dataToSend);


        // 요청 URL로 바꿔주세요.
        string url = "http://localhost:3000/sendData/send";
        byte[] postData = Encoding.UTF8.GetBytes(jsonData);

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");

        WWW www = new WWW(url, postData, headers);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log("Data sent successfully!");
            Debug.Log(jsonData);
            
        }
        else
        {
            Debug.Log("Error sending data: " + www.error);
        }
    }
}