using UnityEngine;

public class pressButton : MonoBehaviour
{
    public GameObject desactivado;
    public GameObject activado;
    public GameObject[] objetos;

    void Start()
    {
        desactivado.SetActive(true);
        activado.SetActive(false);
        
        for (int i = 0; i < objetos.Length; i++)
        {
            objetos[i].SetActive(true);
        }
    }
    public void PressButton()
    {
        desactivado.SetActive(false);
        activado.SetActive(true);

        for (int i = 0; i < objetos.Length; i++)
        {
            objetos[i].SetActive(false);
        }
    }
}
