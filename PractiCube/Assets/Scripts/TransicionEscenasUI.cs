using UnityEngine;
using UnityEngine.SceneManagement;

public class TransicionEscenasUI : MonoBehaviour
{

    public static TransicionEscenasUI Instance;

    public string tipoTransicion;

    [Header("Disolver")]
    public CanvasGroup disolverCanvasGroup;
    public float tiempoDisolverEntrada;
    public float tiempoDisolverSalida;
    public string scene;

    [Header("Bloque")]
    public RectTransform bolqueObject;
    public float tiempoBloqueEntrada;
    public float tiempoBloqueSalida;
    public LeanTweenType bloqueEaseEntrada;
    public LeanTweenType bloqueEaseSalida;
    public float posicionFinalEntrada;
    public float posicionInicialSalida;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (tipoTransicion == "Disolver")
        {
            DisolverEntrada();
        }
        else if (tipoTransicion == "Bloque")
        {
            BloqueEntrada();
        }
    }
    private void DisolverEntrada()
    {
        LeanTween.alphaCanvas(disolverCanvasGroup, 0f, tiempoDisolverEntrada).setOnComplete(() =>
        {
            disolverCanvasGroup.blocksRaycasts = false;
            disolverCanvasGroup.interactable = false;
        });
    }

    public void DisolverSalida()
    {
        disolverCanvasGroup.blocksRaycasts = true;
        disolverCanvasGroup.interactable = true;

        LeanTween.alphaCanvas(disolverCanvasGroup, 1f, tiempoDisolverSalida).setOnComplete(() =>
        {
            SceneManager.LoadScene(scene);
        });
    }

    public void BloqueEntrada()
    {
        LeanTween.moveX(bolqueObject, posicionFinalEntrada, tiempoBloqueEntrada).setEase(bloqueEaseEntrada).setOnComplete(() =>
        {
            bolqueObject.gameObject.SetActive(false);
        });
    }

    public void BloqueSalida()
    {
        bolqueObject.anchoredPosition = new Vector2(posicionInicialSalida, 0f);
        bolqueObject.gameObject.SetActive(true);

        LeanTween.moveX(bolqueObject, 0f, tiempoBloqueSalida).setEase(bloqueEaseSalida);
    }
}
