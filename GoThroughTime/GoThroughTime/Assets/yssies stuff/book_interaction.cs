using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class book_interaction : MonoBehaviour
{ 
    public GameObject bookDialoguePanel;     // UI panel for the book dialogue
    public Dialogue bookDialogue;
    public Dialogue dialogue; 
    public Text dialogueText;  
    private Queue<string> sentences;           // Dialogue for the book (set via Inspector)
    private bool playerNearby = false;       // Flag to check if the player is nearby
    public GameObject fpsController;  

    void Start()
    {
        sentences = new Queue<string>();
    }  

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E)) // Detect player interaction
        {
            StartBookDialogue(bookDialogue);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure it's the player interacting
        {
            Debug.Log("Book touched!");
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))  // When the player exits the trigger zone
        {
            playerNearby = false;  // Set playerNearby to false
        }
    }

    public void StartBookDialogue(Dialogue dialogue)
    {
        bookDialoguePanel.SetActive(true); // Show dialogue panel
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
        fpsController.GetComponent<CharacterController>().enabled = true;
    }
}
