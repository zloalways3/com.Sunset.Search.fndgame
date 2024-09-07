using System.Collections;
using Other;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _loadingText;
    private float dotTimer = 0f;
    private int dotCount = 0; 
    private const float dotInterval = 0.5f;

    private void Start()
    {
        StartCoroutine(LoadMainScene(ConstVault.MAIN_SCENE));
    }

    private IEnumerator LoadMainScene(string nameScene)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nameScene);

        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            UpdateLoadingText();
                
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    private void UpdateLoadingText()
    {
        dotTimer += Time.deltaTime;

        if (dotTimer >= dotInterval)
        {
            dotCount = (dotCount + 1) % 4;
            string dots = new string('.', dotCount);
            _loadingText.text = "Loading" + dots;
            dotTimer = 0f;
        }
    }
}