using UnityEngine;

public class cosasFase5 : MonoBehaviour
{
    public GameObject[] cosas;

    void Update()
    {
        if (fases.fase == 5)
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
