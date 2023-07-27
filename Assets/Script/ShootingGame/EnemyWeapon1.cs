using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon1 : MonoBehaviour
{
    public float speed = 3;

    Vector2 hs;  //half screen: 원점 초기화
    Vector2 dir; //direction: 원점 초기화

    // Start is called before the first frame update
    void Start()
    {
        hs.x = Camera.main.orthographicSize; //카메라 위쪽에서 아래쪽 바라보는 가로의  길이
        hs.y = Camera.main.aspect * hs.x;    //가로세로 비율 * 가로 길이 = 세로 길이

        CalcDir();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 delta = dir * speed * Time.deltaTime;
        transform.Translate(delta);
    }

    void CalcDir(){
        
        Vector2 goal;
        goal.x = Random.Range(-hs.x,hs.x); //전체 가로 길이
        goal.y = Random.Range(-hs.y,hs.y); //전체 세로 길이

        dir = goal - (Vector2) transform.position;   //이동방향 = 목표지점 - 현재위치(Vector3)
        dir.Normalize();

    }

    private void OnCollisionEnter2D(Collision2D collision){
        Destroy(gameObject);  
    }
}
