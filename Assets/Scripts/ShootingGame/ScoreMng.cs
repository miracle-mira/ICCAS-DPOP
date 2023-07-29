using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Text;


public class ScoreMng : MonoBehaviour
{
    public static ScoreMng inst;
    public Text scoreText;

    public GameObject restartButton;
    public GameObject levelButton;
    public GameObject finishButton;

    public int score = 0;
    public int level;

    [System.Serializable]
    public class GameData
    {
        public string userID;
        public int gameID;
        public int gameLevel;
        public string playDate; // Added playDate field
    }


    private void Awake(){
        if(inst == null){
            inst = this;
        }
    }


    public void AddScore(int num){
        
        score += num;
        scoreText.text = "Score " + score;
        
    }

    public void Restart(){
        if(level == 1){
            SceneManager.LoadScene("ShootingLevel1");
            restartButton.SetActive(false);
        }

        if(level == 2){
            SceneManager.LoadScene("ShootingLevel2");
            restartButton.SetActive(false);
        }

        if(level == 3){
            SceneManager.LoadScene("ShootingLevel3");
            restartButton.SetActive(false);
        }
        
    }

    public void LevelUp(){
        
        if(level == 1){
            SceneManager.LoadScene("ShootingLevel2");
            levelButton.SetActive(false);
        }

        if(level == 2){
            SceneManager.LoadScene("ShootingLevel3");
            levelButton.SetActive(false);
        }

        if(level == 3){
            SceneLoad.LoadSceneHandle("jelly");
            levelButton.SetActive(false);
        }
    }

    public void Finish(){
        StartCoroutine(SendData());
        SceneLoad.LoadSceneHandle("jelly");
        finishButton.SetActive(false);
    }


    IEnumerator SendData()
    {
        GameData dataToSend = new GameData
        {
            userID = "q1234",
            gameID = 11,
            gameLevel = level
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
