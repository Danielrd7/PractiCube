using UnityEngine;

public class cosasFase8 : MonoBehaviour
{
    public GameObject[] cosas;

    void Update()
    {
        if (fases.fase == 8)
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
