using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public Button startButton;
    public string firstLevel;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(startGame); 
    }

    void startGame()
    {
            SceneManager.LoadScene(firstLevel);
    }
}
