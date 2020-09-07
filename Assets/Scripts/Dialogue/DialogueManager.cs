using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    public GameObject StartBtn;
    private Queue<string> sentences;
    public Image bgDialogueImage;
    private bool dialogueFinish;
    private float timerDelayFadeOut;
    private bool warriorCanStart;
    public Dropdown levelGame;
    public Toggle audioSound;

    //private void Awake() =>
     //audioSound = GameObject.FindGameObjectWithTag("sound").GetComponent<Toggle>();
        
    
    void Start()=>    
        sentences = new Queue<string>();

    
    private void Update()=>    
        FadeOutColor();    

    public void StartDialogue(Dialogue dialogue) 
    {
        animator.SetBool("IsOpen", true);
        StartBtn.SetActive(false);
        PlayerPrefs.SetInt("level", levelGame.value);
        levelGame.gameObject.SetActive(false);
        audioSound.gameObject.SetActive(false);
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence  in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() 
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence) 
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    private void EndDialogue() 
    {
        animator.SetBool("IsOpen", false);
        dialogueFinish = true;

        Debug.Log("End of conversation");
    }
    private void FadeOutColor()
    {
        if (dialogueFinish)
        {
            timerDelayFadeOut += Time.deltaTime;
            if (timerDelayFadeOut > 2f)            
                bgDialogueImage.color = Color.Lerp(bgDialogueImage.GetComponent<Image>().color, new Color(0f, 0f, 0f, 0f), 0.1f/*Mathf.PingPong(Time.time * 0.1f, 1f)*/);
            if (timerDelayFadeOut > 3f)
                warriorCanStart = true;            
        }
    }
    public bool GetWarriorCanStart() => 
        warriorCanStart;       
}
