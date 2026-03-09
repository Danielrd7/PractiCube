using UnityEngine;

public class rotateObj : MonoBehaviour
{
    public float velocidadRotacion = 50f;

    void Update()
    {
        transform.Rotate(0, 0, velocidadRotacion * Time.deltaTime);
    }
}
