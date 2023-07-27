using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSound : MonoBehaviour
{

    static AudioSource audioSource;
    public static AudioClip audioClip1, audioClip2;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioClip1 = Resources.Load<AudioClip>("item1");
        audioClip2 = Resources.Load<AudioClip>("clear");
        
    }

    public static void GetItemSound()
    {
        audioSource.PlayOneShot(audioClip1);
    }

    public static void FinishSound()
    {
        audioSource.PlayOneShot(audioClip2);
    }

}
