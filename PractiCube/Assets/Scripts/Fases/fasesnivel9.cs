using UnityEngine;
using UnityEngine.SceneManagement;

public class fasesnivel9 : MonoBehaviour
{
    public void CambioFases()
    {

        if (fases.fase == 1)
        {
            fases.fase = 2;
            return;
        }
        
        if (fases.fase == 2)
        {
            fases.fase = 3;
            return;
        }

        if (fases.fase == 3)
        {
            fases.fase = 4;
            return;
        }

        if (fases.fase == 4)
        {
            fases.fase = 5;
            return;
        }

        if (fases.fase == 5)
        {
            fases.fase = 6;
            return;
        }

        if (fases.fase == 6)
        {
            fases.fase = 7;
            return;
        }

        if (fases.fase == 7)
        {
            fases.fase = 8;
            return;
        }

        if (fases.fase == 8)
        {
            SceneManager.LoadScene("Desconocido1");
        }

    }
}
