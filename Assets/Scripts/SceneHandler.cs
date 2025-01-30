using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField]
    RectTransform fader;
    AudioManager audioManager;

    private int currentSceneIdx;
    public float fadeSpeed;

    private void Start()
    {
        currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        fader.gameObject.SetActive(true);
        LeanTween.alpha(fader, 1, 0);
        LeanTween.alpha(fader, 0, fadeSpeed).setOnComplete(() =>
            {
                fader.gameObject.SetActive(false);
            });

        //LeanTween.scale(fader, new Vector3(1, 1, 1), 0);
        //LeanTween.scale(fader, Vector3.zero, fadeSpeed).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        //{
            
        //});
    }

    public void LoadMenu()
    {
        fader.gameObject.SetActive(true);
        audioManager.playMusic(audioManager.menuMusic);
        LeanTween.alpha(fader, 0, 0);
        LeanTween.alpha(fader, 1, fadeSpeed).setOnComplete(() =>
       {
           SceneManager.LoadScene(0);
       });
        //LeanTween.scale(fader, new Vector3(1, 1, 1), 0);
        //LeanTween.scale(fader, Vector3.zero, fadeSpeed).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        //{
        //    SceneManager.LoadScene(0);
        //});
    }

    public void LoadNextScene()
    {
        fader.gameObject.SetActive(true);

        LeanTween.alpha(fader, 0, 0);
        LeanTween.alpha(fader, 1, fadeSpeed).setOnComplete(() =>
        {
            audioManager.playMusic(audioManager.backgroundMusic);
            Invoke("LoadLevel", 0.5f);
        });

        //LeanTween.scale(fader, new Vector3(1, 1, 1), 0);
        //LeanTween.scale(fader, Vector3.zero, fadeSpeed).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        //{
        //    Invoke("LoadLevel", 0.5f);
        //});
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


