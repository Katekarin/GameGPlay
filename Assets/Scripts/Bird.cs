using UnityEngine;

public class BirdAI : MonoBehaviour
{
    public Transform player;
    public float floatSpeed = 2f;
    public float floatAmount = 0.5f;
    public float moveSpeed = 1f;
    public float fleeDistance = 2f;

    private Vector3 startPos;
    private float randomDirection;

    void Start()
    {
        startPos = transform.position;
        randomDirection = Random.Range(-1f, 1f);
    }

    void Update()
    {

        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmount;


        Vector3 move = Vector3.zero;
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance < fleeDistance)
            {
                Vector3 fleeDir = (transform.position - player.position).normalized;
                move = new Vector3(fleeDir.x, 0, 0) * moveSpeed * Time.deltaTime;
            }
            else
            {
                move = new Vector3(randomDirection, 0, 0) * moveSpeed * Time.deltaTime;

                if (Random.value < 0.01f)
                    randomDirection = Random.Range(-1f, 1f);
            }
        }

        transform.position += move;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
