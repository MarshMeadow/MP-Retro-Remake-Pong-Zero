using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;

    public float baseSpeed = 5f;
    public float maxSpeed = Mathf.Infinity;
    public float currentSpeed { get; set; }
    public int score1, score2;
    public TMP_Text test1, test2;

    private void Awake()

    {
        score1 = 0;
        score2 = 0;
        rb = GetComponent<Rigidbody2D>();
    }

    public void ResetPosition()
    {
        rb.velocity = Vector2.zero;
        rb.position = Vector2.zero;
    }

    public void AddStartingForce()
    {
        // Flip a coin to determine if the ball starts left or right
        float x = Random.value < 0.5f ? -1f : 1f;

        // Flip a coin to determine if the ball goes up or down. Set the range
        // between 0.5 -> 1.0 to ensure it does not move completely horizontal.
        float y = Random.value < 0.5f ? Random.Range(-1f, -0.5f)
                                      : Random.Range(0.5f, 1f);

        // Apply the initial force and set the current speed
        Vector2 direction = new Vector2(x, y).normalized;
        rb.AddForce(direction * baseSpeed, ForceMode2D.Impulse);
        currentSpeed = baseSpeed;
    }

    private void FixedUpdate()
    {
        // Clamp the velocity of the ball to the max speed
        Vector2 direction = rb.velocity.normalized;
        currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
        rb.velocity = direction * currentSpeed;
    }
    void Start()
    {
        AddStartingForce();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("leftGoal"))
        {
            score2++;
            test2.SetText(score2.ToString());
            ResetPosition();
            AddStartingForce();
        }

        if (collision.gameObject.CompareTag("rightGoal"))
        {
            score1++;
            test1.SetText(score1.ToString());
            ResetPosition();
            AddStartingForce();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Pong");
        }
    }
}