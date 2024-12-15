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

    public Text next;
    public GameObject nextStep; 
    public Text dialogueText;  
    private Queue<string> sentences;           // Dialogue for the book (set via Inspector)
    private bool playerNearby = false;       // Flag to check if the player is nearby
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
            next.gameObject.SetActive(true);
        }
        if (playerNearby && Input.GetKeyDown(KeyCode.Return)) // Detect player interaction
        {
            DisplayNextSentenceBook();
        }
        if(playerNearby && !bookStory){
            nextStep.SetActive(true);
        }
        if(!playerNearby && !bookStory){
            nextStep.SetActive(false);
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
        dialogueText.text = sentence;
    }
   
    public void EndBookDialogue()
    {
        // Hide the book-specific dialogue panel
        bookDialoguePanel.SetActive(false);
        dialogueText.text = "";
        next.gameObject.SetActive(false);
        GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().SetTimeStamp();
        SceneManager.LoadScene("learn_controls");
    }
       
}