using UnityEngine;

public class TransicionNivel1 : MonoBehaviour
{

    public CanvasGroup disolverCanvasGroup;
    public float tiempoDisolverEntrada;
    
    private void Start()
    {
        DisolverEntrada();
    }

    private void DisolverEntrada()
    {
        LeanTween.alphaCanvas(disolverCanvasGroup, 0f, tiempoDisolverEntrada).setOnComplete(() =>
        {
            disolverCanvasGroup.blocksRaycasts = false;
            disolverCanvasGroup.interactable = false;
        });
    }
}
