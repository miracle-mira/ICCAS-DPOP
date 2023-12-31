using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Text;


public class Finish : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BTNType currentType;
    public Transform buttonScale;
    Vector3 defaultScale;

    public enum BTNType
    {
        Btn1,
        Btn2,
        Btn3,
        Btn4,
        Btn5
    }

    [SerializeField] GameObject SelectPanel; // 수정: SelectPanel 필드 추가

    [System.Serializable]
    public class GameData
    {
        public string userID;
        public int gameID;
        public int gameLevel;
        public string playDate; // Added playDate field
    }

    void Start()
    {
        defaultScale = buttonScale.localScale;
        SelectPanel.SetActive(false); // 판넬을 비활성화 상태로 시작합니다.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemSound.FinishSound();
        Invoke("ShowSelectPanel", 1f); // 2초 후에 판넬을 활성화하는 메서드를 호출합니다.
    }

    // 판넬을 활성화하는 메서드
    private void ShowSelectPanel()
    {
        SelectPanel.SetActive(true);
    }

    private void CloseSelectPanel()
    {
        SelectPanel.SetActive(false);
    }

    public void OnBtnClick()
    {
        switch (currentType)
        {
            case BTNType.Btn1:
                StartCoroutine(SendData());
                SceneLoad.LoadSceneHandle("jelly");
                break;
            case BTNType.Btn2:
                SceneLoad.LoadSceneHandle("Miro_2");
                break;
            case BTNType.Btn3:
                Invoke("ShowSelectPanel", 0f);
                break;
            case BTNType.Btn4:
                SceneLoad.LoadSceneHandle("jelly");
                break;
            case BTNType.Btn5:
                Invoke("CloseSelectPanel", 0f);
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

    IEnumerator SendData()
    {
        GameData dataToSend = new GameData
        {
            userID = "q1234",
            gameID = 12,
            gameLevel = 1
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
        }
        else
        {
            Debug.Log("Error sending data: " + www.error);
        }
    }
}
