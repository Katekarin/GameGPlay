using System;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public GameObject joystick;
    public GameObject handle;

    public Rigidbody2D rb;
    
    public Vector2 touchStart = Vector2.zero;
	public Vector2 touchCurrent = Vector2.zero;

    public Vector2 touchDelta = Vector2.zero;
    public float sens = 2.5f;

    bool flipped = false;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
			joystick.SetActive(true);
			handle.SetActive(true);


			// Debug.Log("Click");
			touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(touchStart);

            joystick.transform.localPosition = touchStart;
        }
        
        if(Input.GetMouseButton(0)) 
        {
			// Debug.Log("Hold");
			touchCurrent = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			handle.transform.position = touchCurrent;

            touchDelta = touchCurrent - touchStart;
            touchDelta = new Vector2(Math.Clamp(touchDelta.x * sens, -1f, 1f), Math.Clamp(touchDelta.y * sens, -1f, 1f));

            transform.position = new Vector2(transform.position.x + touchDelta.x * Time.deltaTime, transform.position.y);

            if (touchDelta.x < 0) 
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
				GetComponent<SpriteRenderer>().flipX = false;
			}
		}

        if (Input.GetMouseButtonUp(0)) {
			
            joystick.SetActive(false);
			joystick.SetActive(false);
		}
    }
}
