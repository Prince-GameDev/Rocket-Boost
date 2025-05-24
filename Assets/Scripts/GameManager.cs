
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void PlayBtn(string Scenens)
    {
        SceneManager.LoadScene(Scenens);

    }
    public void QuitGame()
    {
        Debug.Log("The Game is Quit");
        Application.Quit();
        Debug.Log("The Game is Quit");

    }
}
