using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove2 : MonoBehaviour
{
    public float speed = 3;
    Vector2 dir; 
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeDir(3f));
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 deltaPos = dir * speed * Time.deltaTime;
        transform.Translate(deltaPos);
    }

    IEnumerator ChangeDir(float deltaTime){
        GameObject TargetObj = GameObject.FindWithTag("Hero"); //"Hero" 태그를 갖는 오브젝트를 얻음.

        while(TargetObj != null){
            Vector2 goal = TargetObj.transform.position;
            dir = goal - (Vector2)transform.position;
            dir.Normalize();

            float changeTime = Random.Range(1f,deltaTime);
            yield return new WaitForSeconds(changeTime);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision){
        string cgtag = collision.gameObject.tag;
        if(cgtag == "HeroWeapon"){
            Destroy(gameObject);
            ScoreMng.inst.AddScore(5);
        }
    }
}
