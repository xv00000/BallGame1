using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static private AudioSource coinSource;
    static public AudioClip coinClip;
    // Start is called before the first frame update
    void Start()
    {
        coinSource = GetComponent<AudioSource>();
        coinClip = Resources.Load<AudioClip>("audio/coin");
    }


    public static void eatCoin()
    {
        coinSource.clip = coinClip;
        coinSource.Play();
    }
}
