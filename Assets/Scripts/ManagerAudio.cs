using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerAudio : MonoBehaviour
{
    private GameObject audioSource;
    private bool isPlay;

    // Start is called before the first frame update
    void Start()
    {
        isPlay = true;
        audioSource = GameObject.FindGameObjectWithTag("sound");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnOffAudio()
    {
        isPlay = !isPlay;
        if (isPlay)
            audioSource.GetComponent<AudioSource>().Play();
        else
            audioSource.GetComponent<AudioSource>().Stop();
    }
}
