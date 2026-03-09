using UnityEngine;

public class camaraMovement : MonoBehaviour
{
    public Transform player;

    public float minX, maxX;
    public float minY, maxY;

    void LateUpdate()
    {
        Vector3 pos = player.position;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }
}
