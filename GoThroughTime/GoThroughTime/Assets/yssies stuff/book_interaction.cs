using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class book_interaction : MonoBehaviour
{ 
    public GameObject bookDialoguePanel; 
    public GameObject bookGlow;    // UI panel for the book dialogue
    public Dialogue bookDialogue;
    public Dialogue dialogue; 
    public GameObject nextStep; 
    public Text dialogueText;  
    private Queue<string> sentences;           // Dialogue for the book (set via Inspector)
    private bool playerNearby = false;       // Flag to check if the player is nearby
    public GameObject fpsController; 
           // Reference to the FPS controller object
    private bool bookStory = false;

    void Start()
    {
        sentences = new Queue<string>();
        nextStep.SetActive(false);
        
    }  

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E)) // Detect player interaction
        {
            StartBookDialogue(bookDialogue);
            bookGlow.SetActive(false);
            bookStory = true;
            nextStep.SetActive(false);
        }
        if (playerNearby && Input.GetKeyDown(KeyCode.Return)) // Detect player interaction
        {
            DisplayNextSentenceBook();
        }
        if(playerNearby && !bookStory){
            nextStep.SetActive(true);
        }

    }
    private void OnTriggerEnter(Collider book)
    {
        if (book.CompareTag("Player")) // Ensure it's the player interacting
        {
            Debug.Log("Book touched!");
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider book)
    {
        if (book.CompareTag("Player"))  // When the player exits the trigger zone
        {
            playerNearby = false;  // Set playerNearby to false
        }
    }

    public void StartBookDialogue(Dialogue dialogue)
    {
        bookDialoguePanel.SetActive(true); // Show dialogue panel
        DisableFPSControls();

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentenceBook();
    }
    public void DisplayNextSentenceBook()
    {
        if (sentences.Count == 0)
        {
            EndBookDialogue();
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
    public void EndBookDialogue()
    {
        // Hide the book-specific dialogue panel
        bookDialoguePanel.SetActive(false);
        dialogueText.text = "";
        EnableFPSControls();
        SceneManager.LoadScene("learn_controls");
    }

    public void DisableFPSControls()
    {
       
    }

    public void EnableFPSControls()
    {}
       
}