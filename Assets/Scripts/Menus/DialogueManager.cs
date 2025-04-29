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

    //Test
    public Stack<string> dialogueLines = new Stack<string>(3);

    //Saving keys, all int 
    //MainMenuText
    //StoreText
    //GameText
    //Keeps track of what text the player has seen


    private void Start()
    {
        if(CheckLoad() == 1)
        {
            return;
        }
        // lines = System.IO.File.ReadAllLines(Application.persistentDataPath + "/TestDoc.txt");
        Time.timeScale = 0f;
        Debug.Log(Time.timeScale);
        for (int i = lines.Length - 1; i > -1; i--)
        {
            dialogueLines.Push(lines[i]);
        }


        ShowNextLine();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextLine();
        }

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

    public void ShowNextLine()
    {
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

        currentLine = dialogueLines.Pop();

        StartCoroutine(ShowText());
        //dialogueText.text = currentLine;

        
    } 

    void CheckSceneForSave()
    {
        int _bIdx = SceneManager.GetActiveScene().buildIndex;

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

    }

    int CheckLoad()
    {
        int _bIdx = SceneManager.GetActiveScene().buildIndex;

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
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= currentLine.Length; i++)
        {
            lineCurrentlyDisplayed = currentLine.Substring(0, i);
            dialogueText.text = lineCurrentlyDisplayed;
            yield return new WaitForSecondsRealtime(delay);
        }
    }


}
