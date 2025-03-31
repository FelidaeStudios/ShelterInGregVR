using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] string sceneAI;
    [SerializeField] string sceneVR;

    public void LoadAIScene()
    {
        SceneManager.LoadScene(sceneAI);
    }

    public void LoadVRScene()
    {
        SceneManager.LoadScene(sceneVR);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}