using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SceneHandler : MonoBehaviour
{

    private Button[] buttons;

    private void Awake()
    {

        buttons = FindObjectsOfType<Button>();

        buttons[0].Select();

    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }



}
