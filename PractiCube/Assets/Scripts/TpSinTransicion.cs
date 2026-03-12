using UnityEngine;
using UnityEngine.SceneManagement;

public class TpSinTransicion : MonoBehaviour
{
    public string sceneToLoad;

    public void onInteract()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

}
