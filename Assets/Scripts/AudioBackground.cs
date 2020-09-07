using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBackground : MonoBehaviour
{
    private static AudioBackground instance;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
            AudioBackground.instance = this;

        DontDestroyOnLoad(this.gameObject);

    }

    
}
