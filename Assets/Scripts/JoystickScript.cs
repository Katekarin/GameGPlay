using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private RectTransform background;
    [SerializeField] private RectTransform handle;

    private Vector2 inputVector;

    void Start()
    {
        background = GetComponent<RectTransform>();
        handle = transform.GetChild(0).GetComponent<RectTransform>();
    }

    void Update()
    {
        // Obs³uga klawiatury
        Vector2 keyInput = Vector2.zero;

        keyInput.x = Input.GetAxisRaw("Horizontal"); // A/D lub strza³ki
        keyInput.y = Input.GetAxisRaw("Vertical");   // W/S lub strza³ki

        if (keyInput != Vector2.zero)
        {
            inputVector = keyInput.normalized; // zapewnia, ¿e d³ugoœæ nie > 1
            handle.localPosition = new Vector3(
                inputVector.x * (background.sizeDelta.x / 2),
                inputVector.y * (background.sizeDelta.y / 2),
                handle.localPosition.z);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background,
            eventData.position,
            eventData.pressEventCamera,
            out pos))
        {
            pos.x = (pos.x / background.sizeDelta.x);
            pos.y = (pos.y / background.sizeDelta.y);

            inputVector = new Vector2(pos.x * 2, pos.y * 2);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            // ruszaj r¹czk¹
            handle.localPosition = new Vector3(
                inputVector.x * (background.sizeDelta.x / 2),
                inputVector.y * (background.sizeDelta.y / 2),
                handle.localPosition.z);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        handle.localPosition = Vector3.zero;
    }

    public float Horizontal() => inputVector.x;
    public float Vertical() => inputVector.y;
    public Vector2 Direction() => inputVector;
}
