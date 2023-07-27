using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 필요


public class FinishTwo : MonoBehaviour
{
    [SerializeField] Text finText;
    // Start is called before the first frame update



    void Start()
    {
        finText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        finText.gameObject.SetActive(true);
        ItemSound.FinishSound();
        Invoke("NextSceneWithNum", 2f);
    }

    public void NextSceneWithNum()
    {
        // 씬 번호를 이용해서 씬 이동
        SceneManager.LoadScene(1);  // 0 번째 씬 로드
    }

    
}