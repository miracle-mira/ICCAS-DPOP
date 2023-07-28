using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shoot(3f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Shoot(float delta){
        while(true){
            GameObject bul = Instantiate(prefab,transform.position,transform.rotation);
            Destroy(bul,3);

            float delay = Random.Range(1f,delta);
            yield return new WaitForSeconds(delay);
            /*test*/
        }
    }
}
