using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;

public class login : MonoBehaviour
{
    // 서버 주소
    private string serverUrl = "http://localhost:3000/user/login";

    private UnityWebRequest request;

    public void Login(string id)
    {
        StartCoroutine(SendLoginRequest(id));
    }

    public IEnumerator SendLoginRequest(string id)
    {
        // 이전 요청이 아직 진행 중인지 확인하고, 있다면 취소
        if (request != null && !request.isDone)
        {
            request.Abort();
        }

        // 요청할 JSON 데이터 생성
        string jsonData = "{\"id\": \"" + id + "\"}";

        // UnityWebRequest 생성
        request = new UnityWebRequest(serverUrl, "POST");
        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(jsonBytes);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // 요청 보내기
        yield return request.SendWebRequest();

        // 요청 결과 확인
        if (request.result == UnityWebRequest.Result.Success)
        {
            // 요청 성공
            string responseJson = request.downloadHandler.text;
            // 서버 응답 데이터 파싱
            LoginResponseData response = JsonUtility.FromJson<LoginResponseData>(responseJson);

            if (response.message == "success")
            {
                // "Jelly" 씬으로 이동
                SceneLoad.LoadSceneHandle("Jelly");
            }
            else
            {
                Debug.LogError(response.message); // 서버에서 보낸 메시지를 출력
                Debug.LogError("로그인 실패");
            }
        }
        // else
        // {
        //     // 요청 실패
        //     Debug.LogError("로그인 실패: " + request.error);
        // }
    }

    // 씬이 비활성화될 때 호출되는 메서드
    private void OnDisable()
    {
        // 이전 요청이 아직 진행 중인지 확인하고, 있다면 취소
        if (request != null && !request.isDone)
        {
            request.Abort();
        }

        // UnityWebRequest 객체를 해제
        if (request != null)
        {
            request.Dispose();
            request = null;
        }
    }

    // 서버 응답 데이터를 파싱하기 위한 클래스
    [System.Serializable]
    private class LoginResponseData
    {
        public string message;
    }
}
