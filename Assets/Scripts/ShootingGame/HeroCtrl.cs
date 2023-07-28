using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroCtrl : MonoBehaviour
{
    /*주인공 속도 조절*/
    public float move_speed = 3;
    public float spin_speed = 180;

    /*주인공 무기*/
    public GameObject HeroWeapon;

    /*주인공 목숨*/
    public int HP = 20;
    public Image gauage;
    int maxHP;


    /*효과음*/
    AudioSource audSrc;
    public AudioClip attackSound;
    public AudioClip deadSound;
   

    /*게임종료*/
    public GameObject restartButton;
    public GameObject levelButton;
    public GameObject finishButton;

    /*효과*/
    public GameObject effect;
    public SpriteRenderer sprPlayer; 
    private bool hasSpawned = false;
    

    void Start(){
        maxHP = HP;
        audSrc = GetComponent<AudioSource>();
    }

    void Update()
    {   
        /*move*/
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
            GameObject temp1;
            Vector3 pos = gameObject.transform.position;
            Quaternion rot = gameObject.transform.rotation;
            

            pos += transform.forward * 300;
            temp1 = Instantiate(HeroWeapon,pos,rot); //무기 등장
            temp1.transform.Translate(0,1,0);
            //temp1 = transform.Translate(0,10,0);

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
            //Destroy(gameObject);
            sprPlayer.color = new Color(255,255,255,0);
            
            if (!hasSpawned)
            {
                audSrc.PlayOneShot(deadSound);
                Instantiate(effect,transform.position,transform.rotation);
                hasSpawned = true;
            }

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
