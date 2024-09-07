using UnityEngine;

public class GameEndProgressVisualizer : MonoBehaviour
{
    [SerializeField] private GameObject _completionIndicator;
    [SerializeField] private GameObject _failureIndicator;
    [SerializeField] private GameObject _targetGameObject;

    public void DisplaySuccessMessage()
    {
        _completionIndicator.SetActive(true);
        _targetGameObject.SetActive(false);
    }

    public void DisplayFailureMessage()
    {
        _failureIndicator.SetActive(true);
        _targetGameObject.SetActive(false);
    }

    public static void GameExitPortal()
    {
        Time.timeScale = 0f;
    }
}