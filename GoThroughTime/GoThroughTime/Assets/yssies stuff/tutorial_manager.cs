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
    public Dialogue dialogue; 
    private Queue<string> sentences; // Queue to manage dialogue lines
    public GameObject fpsController;
    private bool typing = false;
    private string currentSentence;

    void Start()
    {
        sentences = new Queue<string>();
        StartDialogue(dialogue);  // Automatically start dialogue on scene load
    }
    void Update()
    {
        // Progress dialogue when the space bar is pressed
        if (!typing && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence();
        }
        if(typing && Input.GetKeyDown(KeyCode.Space)){
            printSentence();
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

        currentSentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = ""; // Clear the text
        typing = true; // Mark typing as active

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter; // Add one letter at a time
            yield return new WaitForSeconds(0.05f); // Wait before adding the next letter
        }

        typing = false; // Reset typing flag when finished
    }

    public void printSentence(){
        if (typing)
        {
            StopAllCoroutines(); // Stop the typing coroutine
            dialogueText.text = currentSentence; // Display the full current sentence
            typing = false; // Reset typing flag
        }
        
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        fpsController.GetComponent<CharacterController>().enabled = true;
       
    }

}

