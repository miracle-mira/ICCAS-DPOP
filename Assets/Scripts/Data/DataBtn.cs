using UnityEngine;
using UnityEngine.UI;

public class DataBtn : MonoBehaviour
{
    public DataLoad dataLoad;
    public int gameId;
    public int gameLevel;
    public string userId;
    public string playDate;

    private void Start()
    {
        dataLoad = FindObjectOfType<DataLoad>();
    }

    public void OnButtonClick()
    {
        // 원하는 값을 설정합니다.
        gameId = 11;
        gameLevel = 1;
        userId = "q1234";
        playDate = System.DateTime.Now.ToString("yyyy-MM-dd");

        // SendGameData 메서드를 호출하여 데이터를 서버로 보냅니다.
        dataLoad.SendGameData(gameId, gameLevel);
    }
}
