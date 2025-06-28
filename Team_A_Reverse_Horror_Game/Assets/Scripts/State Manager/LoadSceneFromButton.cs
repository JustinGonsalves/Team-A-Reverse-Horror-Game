using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneFromButton : MonoBehaviour
{
    public string sceneToLoad;

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
