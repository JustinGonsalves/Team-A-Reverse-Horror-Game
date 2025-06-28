using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneWithDelay : MonoBehaviour
{
    public string sceneToLoad;
    public float delay = 5f;

    private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        //Count up the timer in seconds
        timer += Time.deltaTime;

        //If enough time has passed
        if(timer >= delay)
        {
            //Load the next scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
