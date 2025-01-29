using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenTutorial()
    {
        SceneManager.LoadScene(1);
    }
}
