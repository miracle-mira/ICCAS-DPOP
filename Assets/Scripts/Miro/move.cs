using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float speed = 3.0f;

    void Update(){
        float speed_delta = speed * Time.deltaTime;

        if(Input.GetKey("right")){
            transform.Translate(speed_delta,0,0);
        }

        if(Input.GetKey("left")){
            transform.Translate(-speed_delta,0,0);
        }

        if(Input.GetKey("up")){
            transform.Translate(0,speed_delta,0);
        }

        if(Input.GetKey("down")){
            transform.Translate(0,-speed_delta,0);
        }
    }
}
