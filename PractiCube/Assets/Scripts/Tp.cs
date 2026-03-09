using UnityEngine;
using UnityEngine.SceneManagement;

public class Tp : MonoBehaviour
{
    public string sceneToLoad;

    public void onInteract()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}