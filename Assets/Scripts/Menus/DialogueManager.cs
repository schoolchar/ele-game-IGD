using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image textBox;
    [SerializeField] private TextMeshProUGUI instructions;

    [SerializeField] private string[] lines;
    [SerializeField] private string currentLine;
    [SerializeField] private string lineCurrentlyDisplayed;

    float delay = 0.02f;

    
    public Stack<string> dialogueLines = new Stack<string>(3);

    //Saving keys, all int 
    //MainMenuText
    //StoreText
    //GameText
    //Keeps track of what text the player has seen


    private void Start()
    {
        //Check if player has played this scene before/has cleared data
        if(CheckLoad() == 1)
        {
            //If so, deactivate dialogue
            return;
        }
        // lines = System.IO.File.ReadAllLines(Application.persistentDataPath + "/TestDoc.txt");
        //Freeze game while dialogue is happening
        Time.timeScale = 0f;
        Debug.Log(Time.timeScale);
        //Push dialogue to the stack
        for (int i = lines.Length - 1; i > -1; i--)
        {
            dialogueLines.Push(lines[i]);
        }

        //Start dialogue
        ShowNextLine();
    }

    private void Update()
    {
        //If space, continue to next line
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextLine();
        }

        //If click, skip
        if(Input.GetMouseButtonDown(0))
        {
            
            dialogueText.enabled = false;
            textBox.enabled = false;
            instructions.enabled = false;
            Time.timeScale = 1;
        }

        if(dialogueText.enabled)
        {
            Time.timeScale = 0f;
        }

    }

    /// <summary>
    /// Move through dialogue lines
    /// </summary>
    public void ShowNextLine()
    {
        //If reached end of dialogue, deactivate text and text box, unfreeze game
        if(dialogueLines.Count == 0)
        {
            //Debug.Log("Stack null");
            dialogueText.enabled = false;
            textBox.enabled = false;
            instructions.enabled = false;
            Time.timeScale = 1;
            CheckSceneForSave();

            return;
        }

        //Fill in next line by popping off the stack
        currentLine = dialogueLines.Pop();

        StartCoroutine(ShowText());
        //dialogueText.text = currentLine;

        
    } //END ShowNextLine()

    /// <summary>
    /// Check which scene the player is in, save accordingly
    /// </summary>
    void CheckSceneForSave()
    {
        //Get scene index
        int _bIdx = SceneManager.GetActiveScene().buildIndex;

        //Depending on the scene, record what dialogue the player has seen already as to not play it again
        if(_bIdx == 0) //Main menu
        {
            PlayerPrefs.SetInt("MainMenuText", 1);
            PlayerPrefs.Save();
        }
        else if(_bIdx == 1) //Game scene
        {
            PlayerPrefs.SetInt("GameText", 1);
            PlayerPrefs.Save();
        }
        else if(_bIdx == 4) //Store
        {
            PlayerPrefs.SetInt("StoreText", 1);
            PlayerPrefs.Save();
        } 

    } //END CheckSceneForSave()

    /// <summary>
    /// Check if player has seen this scene's dialogue yet
    /// </summary>
    int CheckLoad()
    {
        //Get current scene
        int _bIdx = SceneManager.GetActiveScene().buildIndex;

        //Check what is recorded for each scene, if the player has seen this dialogue already
        if (_bIdx == 0) //Main menu
        {
            if(PlayerPrefs.GetInt("MainMenuText") == 1)
            {
                dialogueText.enabled = false;
                textBox.enabled = false;
                instructions.enabled = false;
                Time.timeScale = 1;
                return 1;
            }
        }
        else if (_bIdx == 1) //Game scene
        {
            if (PlayerPrefs.GetInt("GameText") == 1)
            {
                dialogueText.enabled = false;
                textBox.enabled = false;
                instructions.enabled = false;
                Time.timeScale = 1;
                return 1;
            }
        }
        else if (_bIdx == 4) //Store
        {
            if (PlayerPrefs.GetInt("StoreText") == 1)
            {
                dialogueText.enabled = false;
                textBox.enabled = false;
                instructions.enabled = false;
                Time.timeScale = 1;
                return 1;
            }
        }

        return 0;
    } //END CheckLoad()

    /// <summary>
    /// Typewriter effect on text
    /// </summary>
    IEnumerator ShowText()
    {
        for (int i = 0; i <= currentLine.Length; i++)
        {
            lineCurrentlyDisplayed = currentLine.Substring(0, i);
            dialogueText.text = lineCurrentlyDisplayed;
            yield return new WaitForSecondsRealtime(delay);
        }
    } //END ShowText()


}
