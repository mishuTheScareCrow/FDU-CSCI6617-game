using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayLevel1() => SceneManager.LoadScene(1);
    public void PlayLevel2() => SceneManager.LoadScene(2);
    public void Quit() => Application.Quit();
}
