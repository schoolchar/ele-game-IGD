using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTextOnStart : MonoBehaviour
{
    // Public GameObject that can be assigned in the Unity Inspector
    public GameObject vnText;

    // Start is called before the first frame update
    void Start()
    {
        vnText.SetActive(true);

        Time.timeScale = 0;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && vnText.activeSelf)
        {
            startGame();
        }
    }
    public void startGame()
    {
        vnText.SetActive(false);
        Time.timeScale = 1;
    }
}