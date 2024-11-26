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
    public GameObject fpsController;        // Reference to the FPS controller object
    private bool typing = false;
     private string currentSentence;

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
            nextStep.SetActive(false);
        }
        if (playerNearby && Input.GetKeyDown(KeyCode.Space) && typing == false) // Detect player interaction
        {
            DisplayNextSentenceBook();
        }
        if(playerNearby){
            nextStep.SetActive(true);
        }
        if(typing == true && Input.GetKeyDown(KeyCode.Space)){
            printSentence();
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
        if (typing) {
        // Immediately complete the current sentence
        StopAllCoroutines(); // Stop the typing coroutine
        dialogueText.text = sentences.Peek(); // Display the current sentence fully
        typing = false; 
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