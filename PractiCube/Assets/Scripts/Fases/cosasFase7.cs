using UnityEngine;

public class cosasFase7 : MonoBehaviour
{
    public GameObject[] cosas;

    void Update()
    {
        if (fases.fase == 7)
        {
            for (int i = 0; i < cosas.Length; i++){
                cosas[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < cosas.Length; i++){
                cosas[i].SetActive(false);
            }
        }
    }
}
