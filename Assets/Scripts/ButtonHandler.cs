using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
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

    public void QuitGame()
    {
        Application.Quit(0);
    }

}
