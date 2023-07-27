using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroCtrl : MonoBehaviour
{
    public float move_speed = 3;
    public float spin_speed = 180;
    public GameObject HeroWeapon;
    public int HP = 20;

    public Image gauage;
    public GameObject button;

    AudioSource audSrc;
    public AudioClip attackSound;
    public AudioClip deadSound;

    public GameObject restartButton;
    public GameObject levelButton;
    public GameObject finishButton;

    int maxHP;

    void Start(){
        maxHP = HP;
        audSrc = GetComponent<AudioSource>();
    }

    void Update()
    {
        float speed_delta = move_speed * Time.deltaTime;
        if(Input.GetKey("up")){
            transform.Translate(0,speed_delta,0);
        }

        if(Input.GetKey("down")){
            transform.Translate(0,-speed_delta,0);
        }


        float degree_delta = spin_speed * Time.deltaTime;
        if(Input.GetKey("left")){
            transform.Rotate(0,0,degree_delta); //반시계방향
        }

        if(Input.GetKey("right")){
            transform.Rotate(0,0,-degree_delta);
        }

        if(Input.GetKeyDown("space")){
            Vector3 pos = gameObject.transform.position;
            Quaternion rot = gameObject.transform.rotation;
            
            Instantiate(HeroWeapon,pos,rot); //무기 등장
            audSrc.PlayOneShot(attackSound);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        string cgtag = collision.gameObject.tag;

        if(cgtag == "Enemy" ){
            HP -= 2;
        }

        if(cgtag == "EnemyWeapon"){
            HP--;
        }

        gauage.fillAmount = (float)HP / maxHP;

        if(HP <= 0){
            Destroy(gameObject);

            int Level = ScoreMng.inst.level;
            int Score = ScoreMng.inst.score;

            if(Level == 1){
                if(Score <  20 * Level){restartButton.SetActive(true);}
                else {levelButton.SetActive(true);}
            }

            if(Level == 2){
                if(Score <  20 * Level){restartButton.SetActive(true);}
                else {levelButton.SetActive(true);}
            }

            if(Level == 3){
                if(Score <  20 * Level){restartButton.SetActive(true);}
                else {levelButton.SetActive(true);}
            }

            finishButton.SetActive(true);
        }
    }
}
