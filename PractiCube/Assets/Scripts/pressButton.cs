using UnityEngine;

public class pressButton : MonoBehaviour
{
    public GameObject desactivado;
    public GameObject activado;
    public GameObject lazer;

    void Start()
    {
        desactivado.SetActive(true);
        activado.SetActive(false);
        lazer.SetActive(true);
    }
    public void PressButton()
    {
        desactivado.SetActive(false);
        activado.SetActive(true);
        lazer.SetActive(false);
    }
}
