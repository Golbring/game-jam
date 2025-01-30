using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField]
    RectTransform fader;

    private int currentSceneIdx;
    public float fadeSpeed;

    private void Start()
    {
        currentSceneIdx = SceneManager.GetActiveScene().buildIndex;

        fader.gameObject.SetActive(true);

        LeanTween.scale(fader, new Vector3(1, 1, 1), 0);
        LeanTween.scale(fader, Vector3.zero, fadeSpeed).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            fader.gameObject.SetActive(false);
        });
    }

    public void LoadMenu()
    {
        fader.gameObject.SetActive(true);

        LeanTween.scale(fader, new Vector3(1, 1, 1), 0);
        LeanTween.scale(fader, Vector3.zero, fadeSpeed).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            SceneManager.LoadScene(0);
        });
    }

    public void LoadNextScene()
    {
        fader.gameObject.SetActive(true);

        LeanTween.scale(fader, new Vector3(1, 1, 1), 0);
        LeanTween.scale(fader, Vector3.zero, fadeSpeed).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            Invoke("LoadLevel", 0.5f);
        });
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    void LoadLevel()
    {
        if (currentSceneIdx <= 5)
        {
            SceneManager.LoadScene(currentSceneIdx + 1);
        }
        else { return ; }
    }
}


