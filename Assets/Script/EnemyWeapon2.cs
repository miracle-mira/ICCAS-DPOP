using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon2 : MonoBehaviour
{
    public float speed = 3;
    Vector2 dir; 
    
    // Start is called before the first frame update
    void Start()
    {
        CalcDir();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 deltaPos = dir * speed * Time.deltaTime;
        transform.Translate(deltaPos);
    }

    void CalcDir(){
        GameObject TargetObj = GameObject.FindWithTag("Hero"); //"Hero" 태그를 갖는 오브젝트를 얻음.

        if(TargetObj != null){
            Vector2 goal = TargetObj.transform.position;
            dir = goal - (Vector2)transform.position;
            dir.Normalize();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        Destroy(gameObject);  
    }
}
