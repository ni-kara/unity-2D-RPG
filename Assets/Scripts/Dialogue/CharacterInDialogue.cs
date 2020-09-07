using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterInDialogue : MonoBehaviour
{
    public Transform warriorStartPosition;
    public Transform warriorFinishPosition;
    public DialogueManager dialogueManager;
    private float speed =3;

    void Start()=>    
        transform.position = warriorStartPosition.position;

    void Update()
    {
        WarriorStartMove();

        WarriorArriveFinishPosition();
    }

    private void WarriorMove() 
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, warriorFinishPosition.position, step);
    }

    private void WarriorStartMove() 
    {
        if (dialogueManager.GetWarriorCanStart())
            WarriorMove();
    }

    public void StopAnimationWalk() =>
        gameObject.GetComponent<Animator>().enabled = false;           

    private void WarriorArriveFinishPosition() 
    {
        if (transform.position.Equals(warriorFinishPosition.position))
        {
            StopAnimationWalk();
            WarriorScaling();
        }    
    }

    public void WarriorScaling() 
    {
        if (transform.localScale.x > 0.05f)
            transform.localScale -= Vector3.one * Time.deltaTime;
        if (transform.localScale.x < 0.1f)
           SceneManager.LoadScene("Gameplay");
    }
}
