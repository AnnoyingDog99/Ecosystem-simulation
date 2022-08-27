using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

using UnityEditor.SceneTemplate;


public class SceneNavigationController : MonoBehaviour
{
    public Button createNewWorld;
    public GroupBox scenes;
    public VisualTreeAsset sceneUITemplate;


    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        scenes = root.Q<GroupBox>("Scenes");
        scenes.Clear();

        createNewWorld = root.Q<Button>("Create");

        createNewWorld.clicked += CreateNewWorldPressed;
    }

    void CreateNewWorldPressed()
    {
        sceneUITemplate.CloneTree(scenes);
        //https://docs.unity3d.com/Manual/scenes-working-with.html
    }
}
