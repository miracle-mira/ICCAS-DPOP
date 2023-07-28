using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroWeapon : MonoBehaviour
{
    public float speed = 3;
    public float delay = 3;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,delay);
    }

    // Update is called once per frame
    void Update()
    {
        float delta = speed * Time.deltaTime;
        transform.Translate(0,delta,0);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        Destroy(gameObject);  
    }
}
