using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image textBox;

    [SerializeField] private string[] lines;
    [SerializeField] private string currentLine;

    //Test
    public Stack<string> dialogueLines = new Stack<string>(3);


    private void Start()
    {
        lines = System.IO.File.ReadAllLines(Application.persistentDataPath + "/TestDoc.txt");

        for(int i = lines.Length - 1; i > -1; i--)
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
    }

    public void ShowNextLine()
    {
        if(dialogueLines.Count == 0)
        {
            Debug.Log("Stack null");
            dialogueText.enabled = false;
            textBox.enabled = false;
            return;
        }

        currentLine = dialogueLines.Pop();

        dialogueText.text = currentLine;

        
    } 

    
}
