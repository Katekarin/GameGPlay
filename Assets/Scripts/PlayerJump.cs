using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModifierSource { Item, Passive, Buff, Debuff }

[System.Serializable]
public struct Modifier
{
    public ModifierSource Source;
    public float Value;
    public string Description;

    public Modifier(ModifierSource source, float value, string description = "")
    {
        Source = source;
        Value = value;
        Description = description;
    }
}

public class PlayerJump : MonoBehaviour
{
    [Header("Jump Stats")]
    public int jumpBase = 1;
    public int jumpAdditional = 0;
    public float jumpForceBase = 5f;

    [Header("Modifiers")]
    public List<Modifier> jumpBaseModifiers = new List<Modifier>();
    public List<Modifier> jumpAdditionalModifiers = new List<Modifier>();
    public List<Modifier> jumpForceModifiers = new List<Modifier>();

    [Header("Physics")]
    public float gravityMultiplier = 2f;
    public float maxGravity = 9.8f;
    public float gravityIncreaseTime = 4f;

    [Header("Jump Cooldown")]
    public float jumpResetTime = 3f; // czas po jakim skoki się odnawiają

    private Rigidbody2D rb;
    private int jumpsLeft;
    private float gravityTimer;
    private bool isJumping;
    private Coroutine resetCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ResetJumps();
    }

    void Update()
    {
        // Skok po spacji
        if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0)
        {
            Jump();
        }

        // Naturalny lot z rosnącą grawitacją
        if (rb.velocity.y < 0)
        {
            gravityTimer += Time.deltaTime;
            float gravityScale = Mathf.Lerp(1f, gravityMultiplier, gravityTimer / gravityIncreaseTime);
            rb.gravityScale = Mathf.Min(gravityScale, maxGravity);
        }
        else
        {
            gravityTimer = 0f;
            rb.gravityScale = 1f;
        }
    }

    void Jump()
    {
        float totalJumpForce = jumpForceBase + GetTotalModifierValue(jumpForceModifiers);
        float randomFactor = Random.Range(0.9f, 1.1f);

        rb.velocity = new Vector2(rb.velocity.x, 0f); // zerowanie prędkości Y
        rb.AddForce(Vector2.up * totalJumpForce * randomFactor, ForceMode2D.Impulse);

        jumpsLeft--;

        // Jeśli nie ma już dostępnych skoków, uruchom odliczanie resetu
        if (jumpsLeft <= 0 && resetCoroutine == null)
        {
            resetCoroutine = StartCoroutine(ResetJumpsAfterDelay());
        }
    }

    IEnumerator ResetJumpsAfterDelay()
    {
        yield return new WaitForSeconds(jumpResetTime);
        ResetJumps();
        resetCoroutine = null;
    }

    public void ResetJumps()
    {
        int totalJumps = jumpBase + jumpAdditional +
                         Mathf.RoundToInt(GetTotalModifierValue(jumpBaseModifiers) + GetTotalModifierValue(jumpAdditionalModifiers));
        jumpsLeft = totalJumps;
    }

    float GetTotalModifierValue(List<Modifier> modifiers)
    {
        float total = 0;
        foreach (var mod in modifiers)
        {
            total += mod.Value;
        }
        return total;
    }
}
