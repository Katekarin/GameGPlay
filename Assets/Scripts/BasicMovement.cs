using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Joystick joystick;
    public float moveSpeed = 5f;

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        if (joystick == null)
            joystick = FindObjectOfType<Joystick>(); // znajdzie pierwszy joystick w scenie
    }

    void Update()
    {
        if (rb == null || joystick == null)
        {
            Debug.LogError("Brakuje referencji! Upewnij się, że Player ma Rigidbody2D i joystick jest w scenie.");
            return;
        }

        Vector2 move = new Vector2(joystick.Horizontal(), joystick.Vertical());
        rb.velocity = move * moveSpeed;

        if (move.x < 0)
            GetComponent<SpriteRenderer>().flipX = true;
        else if (move.x > 0)
            GetComponent<SpriteRenderer>().flipX = false;
    }
}
