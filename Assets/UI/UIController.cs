using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("Game");
    }

    void SettingsButtonPressed()
    {
        SceneManager.LoadScene("Settings");
    }

    void QuitButtonPressed()
    {
        Application.Quit();
    }
}
