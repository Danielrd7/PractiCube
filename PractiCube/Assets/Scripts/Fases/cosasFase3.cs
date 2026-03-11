using UnityEngine;

public class cosasFase3 : MonoBehaviour
{
    public GameObject[] cosas;

    void Update()
    {
        if (fases.fase == 3)
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
