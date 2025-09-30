using UnityEngine;

public class Background : MonoBehaviour
{
    public Transform player;       // obiekt gracza (np. kamera albo sam player)
    public float parallaxFactor = 0.2f; // im mniejsza wartoœæ, tym wolniej porusza siê t³o
    public float yOffset = 0;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        Vector3 newPos = new Vector3(
            startPos.x + (player.position.x * parallaxFactor * -1f),
            startPos.y + player.position.y * parallaxFactor + yOffset,
            transform.position.z
        );
        transform.position = newPos;
    }
}
