using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;

public class DataLoad : MonoBehaviour
{
    private string serverUrl = "http://localhost:3000/game1/load"; // node.js 서버의 엔드포인트 URL
    private string userId = "q1234"; // 사용자의 user_id를 미리 설정
    private int gameId = 11;
    private int gameLevel = 1;


public void SendGameData(int gameId, int gameLevel)
    {
        DateTime playDate = DateTime.Now; // 현재 시간을 가져와서 playDate에 저장
        StartCoroutine(PostGameData(gameId, gameLevel, playDate));
    }

    private IEnumerator PostGameData(int gameId, int gameLevel, DateTime playDate)
    {
        // 게임 데이터를 JSON 형식으로 변환
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("userId", userId); // 미리 설정한 user_id 사용
        data.Add("gameId", gameId.ToString()); // 매개변수로 받은 gameId를 사용
        data.Add("gameLevel", gameLevel.ToString()); // 매개변수로 받은 gameLevel을 사용
        data.Add("playDate", playDate.ToString("yyyy-MM-dd")); // 날짜 형식을 지정하여 전송

            // 보내는 데이터를 로그로 출력
        Debug.Log("Sending data: " + JsonUtility.ToJson(data));

        string jsonData = JsonUtility.ToJson(data);

        // HTTP POST 요청 생성
        byte[] byteData = Encoding.UTF8.GetBytes(jsonData);
        WWWForm form = new WWWForm();
        form.AddBinaryData("data", byteData);

        // 요청 보내기
        WWW www = new WWW(serverUrl, form);

        yield return www;

        // 응답 처리
        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log("게임 데이터 전송 성공");
        }
        else
        {
            Debug.LogError("게임 데이터 전송 실패: " + www.error);
        }
    }
}
