using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public Button playButton;
    public Button settingsButton;
    public Button quitButton;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        // get buttons
        playButton = root.Q<Button>("PlayButton");
        settingsButton = root.Q<Button>("SettingsButton");
        quitButton = root.Q<Button>("QuitButton");

        // set events to buttons
        playButton.clicked += PlayButtonPressed;
        settingsButton.clicked += SettingsButtonPressed;
        quitButton.clicked += QuitButtonPressed;

    }

    void PlayButtonPressed()
    {
        Debug.Log("Navigate to game");
    }

    void SettingsButtonPressed()
    {
        Debug.Log("Navigate to settings menu");
    }

    void QuitButtonPressed()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }
}
