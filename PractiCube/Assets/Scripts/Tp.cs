using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Tp : MonoBehaviour
{
    public string sceneToLoad;

    public void onInteract()
    {
        StartCoroutine(CargarEscena());
    }

    IEnumerator CargarEscena()
    {
        TransicionEscenasUI.Instance.BloqueSalida();

        yield return new WaitForSeconds(.5f);

        SceneManager.LoadScene(sceneToLoad);
    }
}