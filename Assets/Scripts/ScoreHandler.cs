using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    private SceneHandler scene;
    private ScoreTracking tracker;
    GameObject deaths;

    private void Start()
    {
        scene = GetComponent<SceneHandler>();
        deaths = GameObject.Find("Score");
        tracker = deaths.GetComponent<ScoreTracking>();

        if (scene.currentSceneIdx == 5)
        {
            scoreText.text = tracker.deathNumber + " cats perished on duty.";
        }
        else { return; }
    }
}
