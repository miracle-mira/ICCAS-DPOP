using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DataReceiver : MonoBehaviour
{
    // 서버의 URL
    private string serverURL = "http://localhost:3000/loadData/load";

    void Start()
    {
        StartCoroutine(GetDataFromServer());
    }

    IEnumerator GetDataFromServer()
    {
        // HTTP GET 요청을 생성합니다.
        UnityWebRequest www = UnityWebRequest.Get(serverURL);

        // 요청을 보냅니다.
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            // 서버에서 받은 응답을 JSON 형식으로 파싱합니다.
            string jsonResponse = www.downloadHandler.text;
            DataModel data = JsonUtility.FromJson<DataModel>(jsonResponse);

            // 데이터를 출력합니다.
            Debug.Log("Received data from server - userID: " + data.userID + ", gameID: " + data.gameID + ", gameLevel: " + data.gameLevel + ", playDate: " + data.playDate);
        }
        else
        {
            // 요청이 실패했을 때 에러 메시지를 출력합니다.
            Debug.LogError("Error getting data from server: " + www.error);
        }
    }

    // 서버로부터 받을 데이터를 담을 데이터 모델 클래스
    [System.Serializable]
    public class DataModel
    {
        public string userID;
        public string gameID;
        public int gameLevel;
        public string playDate;
    }
}
