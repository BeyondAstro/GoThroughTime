using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TutorialManager : MonoBehaviour
{
	public GameObject dialoguePanel; 
    public GameObject bookPanel;// UI panel for dialogue
    public Text dialogueText; // Dialogue text/
    public Text next; 
    public Dialogue dialogue; 
    private Queue<string> sentences; // Queue to manage dialogue lines
    public GameObject fpsController;

    void Start()
    {
        sentences = new Queue<string>();
        StartDialogue(dialogue);  // Automatically start dialogue on scene load
        next.gameObject.SetActive(true);
    }
    void Update()
    {
        // Progress dialogue when the space bar is pressed
        if (Input.GetKeyDown(KeyCode.Return))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialoguePanel.SetActive(true); // Show dialogue panel
        bookPanel.SetActive(false);
        fpsController.GetComponent<CharacterController>().enabled = false;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
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

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        fpsController.GetComponent<CharacterController>().enabled = true;
        next.gameObject.SetActive(false);
       
    }

}

