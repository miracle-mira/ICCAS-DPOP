using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FinishThree : MonoBehaviour
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
    }

  

}
