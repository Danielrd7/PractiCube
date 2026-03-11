using UnityEngine;

public class fase1Fase2 : MonoBehaviour
{
    public void CambioFase()
    {
        if (fases.fase == 1)
        {
            fases.fase = 2;
        }
    }
}
