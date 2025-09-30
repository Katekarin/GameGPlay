using UnityEngine;

public class BirdAI : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2f;
    public float fleeSpeed = 4f;
    public float fleeDistance = 2f;
    public float floatAmount = 0.3f;
    public float floatSpeed = 2f;

    private Transform targetGround;
    private bool isLanding = false;
    private bool isFleeing = false;
    private float startY;

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        if (player == null) return;

        float distToPlayer = Vector3.Distance(transform.position, player.position);

        if (isLanding)
        {
            FlyToGround();
        }
        else if (isFleeing)
        {
            FleeFromPlayer();
        }
        else
        {
            if (distToPlayer < fleeDistance)
            {
                isFleeing = true;
            }
            else
            {
                if (targetGround == null)
                {
                    FindGround();
                }
                else
                {
                    isLanding = true;
                }
            }
        }
    }

    void FindGround()
    {
        GameObject[] grounds = GameObject.FindGameObjectsWithTag("Ground");

        Transform bestGround = null;
        float bestDist = Mathf.Infinity;

        foreach (GameObject g in grounds)
        {
            float distToPlayer = Vector3.Distance(player.position, g.transform.position);
            if (distToPlayer < fleeDistance) continue;

            float dist = Vector3.Distance(transform.position, g.transform.position);
            if (dist < bestDist)
            {
                bestDist = dist;
                bestGround = g.transform;
            }
        }

        targetGround = bestGround;
    }

    void FlyToGround()
    {
        if (targetGround == null) { isLanding = false; return; }

        float distToPlayer = Vector3.Distance(transform.position, player.position);

        if (distToPlayer < fleeDistance)
        {
            isLanding = false;
            targetGround = null;
            isFleeing = true;
            return;
        }

        Vector3 targetPos = targetGround.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.05f)
        {
            isLanding = false;
            targetGround = null;
        }
    }

    void FleeFromPlayer()
    {
        Vector3 fleeDir = (transform.position - player.position).normalized + Vector3.up;
        fleeDir.Normalize();

        float newY = transform.position.y + Mathf.Sin(Time.time * floatSpeed) * floatAmount * Time.deltaTime;

        transform.position += fleeDir * fleeSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        if (Vector3.Distance(transform.position, player.position) > fleeDistance * 2f)
        {
            isFleeing = false;
        }
    }
}
