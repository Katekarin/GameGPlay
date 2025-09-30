using System;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer rend;
    public GameObject joystick;
    public GameObject handle;
    public float moveSpeed = 1f;
    public float sens = 2.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveInput = 0f;

        // --- Sterowanie WSAD / strzałkami ---
        moveInput = Input.GetAxisRaw("Horizontal"); // -1 lewo, 1 prawo

        // --- Sterowanie joystickiem ---
        if (Input.GetMouseButtonDown(0))
        {
            joystick.SetActive(true);
            joystick.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            handle.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 touchDelta = handle.transform.localPosition;
            Vector2 touchDeltaClamped = new Vector2(Math.Clamp(touchDelta.x * sens, -1f, 1f), Math.Clamp(touchDelta.y * sens, -1f, 1f));

            moveInput = touchDeltaClamped.x; // joystick nadpisuje moveInput
			//rb.linearVelocityX = touchDeltaClamped.x * moveSpeed;

			if (touchDelta.x < 0)
                rend.flipX = true;
            else if (touchDelta.x > 0)
                rend.flipX = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            joystick.SetActive(false);
        }

        // --- Przemieszczanie gracza ---
		rb.linearVelocityX = moveInput * moveSpeed;
	}
}
