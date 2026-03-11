using UnityEngine;
using System.Collections;

public class timerAparecerDesaparecer : MonoBehaviour
{
    public GameObject[] objetos;
    public float timer;

    void Start()
    {
        StartCoroutine(Ciclo(timer));
    }

    IEnumerator Ciclo(float timer)
    {
        while(true)
        {
            for (int i = 0; i < objetos.Length; i++){
                objetos[i].SetActive(true);
            }
            yield return new WaitForSeconds(timer);

            for (int i = 0; i < objetos.Length; i++){
                objetos[i].SetActive(false);
            }
            yield return new WaitForSeconds(timer);
        }
    }
}
