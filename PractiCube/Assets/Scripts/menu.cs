using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public void Salir()
    {
        Application.Quit();
    }
    public void Jugar()
    {
        TransicionEscenasUI.Instance.DisolverSalida();
    }
}
