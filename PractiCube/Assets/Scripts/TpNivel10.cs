using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TpNivel10 : MonoBehaviour
{
    public string sceneToLoad;

    public void onInteract()
    {
        StartCoroutine(CargarEscena());
    }

    IEnumerator CargarEscena()
    {
        TransicionEscenasUI.Instance.DisolverSalida();

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene(sceneToLoad);
    }
}
