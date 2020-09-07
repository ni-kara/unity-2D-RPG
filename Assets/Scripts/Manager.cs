using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public GameObject warrior;
    public GameObject demon;
    public GameObject lizard;

    public GameObject backgroundUI;
    public GameObject winLabelUI;
    public GameObject loseLabelUI;

    void Start()
    {
        if (PlayerPrefs.GetInt("level") == 0)
            lizard.SetActive(false);
        else
            lizard.SetActive(true);
    }

    void Update()
    {
        demon.GetComponent<DemonBehaviour>().SetWarriorPosition(warrior.transform.position);
        if (lizard.activeSelf)
            lizard.GetComponent<DemonBehaviour>().SetWarriorPosition(warrior.transform.position);

        EnableUI();
    }

    private void EnableUI() 
    {
        if (lizard.activeSelf)
        {
            if (demon.GetComponent<DemonBehaviour>().isDead() && lizard.GetComponent<DemonBehaviour>().isDead())
            {
                backgroundUI.SetActive(true);
                winLabelUI.SetActive(true);
            }
        }
        else
          if (demon.GetComponent<DemonBehaviour>().isDead())
        {
            backgroundUI.SetActive(true);
            winLabelUI.SetActive(true);
        }

        if (warrior.GetComponent<WarriorBehaviour>().isDead())
        {
            backgroundUI.SetActive(true);
            loseLabelUI.SetActive(true);
            demon.GetComponent<DemonBehaviour>().SetWarriorIsDead(true);
        }
    }   

    public void TryAgain()=>    
        SceneManager.LoadScene(0);    
}
