using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreMng : MonoBehaviour
{
    public static ScoreMng inst;
    public Text scoreText;

    public GameObject restartButton;
    public GameObject levelButton;
    public GameObject finishButton;

    public int score = 0;
    public int level;

    private void Awake(){
        if(inst == null){
            inst = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() 
    {
        
    }

    public void AddScore(int num){
        
        score += num;
        scoreText.text = "점수 " + score;
        
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
            SceneManager.LoadScene("MainScene");
            levelButton.SetActive(false);
        }
    }

    public void Finish(){
        SceneManager.LoadScene("MainScene");
        finishButton.SetActive(false);
    }
}
