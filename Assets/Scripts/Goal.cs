using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] private Color componentColor = new Color(0.15f, 0.85f, 0.2f);
    private bool triggered;

    private void Awake()
    {
        ComponentColorUtility.Apply(gameObject, componentColor);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;

        triggered = true;
        int cur = SceneManager.GetActiveScene().buildIndex;
        int next = cur + 1;

        if (next < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(next);
        else
            SceneManager.LoadScene(0); // back to Main after Level2
    }
}
