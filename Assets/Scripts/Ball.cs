using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameManager gameManager;
    public Rigidbody2D rb2d;
    public float maxInitialangle = 0.67f;
    public float moveSpeed = 1f;
    public float maxStartY = 4f;
    public float speedMultiplier = 1.1f; 

    private float startX = 0f;

    private void Start() 
    {
        InitialPush();
    }

    private void InitialPush()     
    {
        Vector2 dir = Vector2.left;

        if(Random.value < 0.5f)
           dir = Vector2.right;
       
        dir.y = Random.Range(-maxInitialangle, maxInitialangle);
        rb2d.linearVelocity = dir * moveSpeed;
    }

    private void ResetBall()
    {
       float posY = Random.Range(-maxStartY, maxStartY);
       Vector2 position = new Vector2(startX, posY);
       transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        ScoreZone scoreZone = collision.GetComponent<ScoreZone>();
        if(scoreZone)
        {
            gameManager.OnScoreZoneReached(scoreZone.id);
            ResetBall();
            InitialPush();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        Paddle paddle = collision.collider.GetComponent<Paddle>();
        if(paddle)
        {
            rb2d.linearVelocity *= speedMultiplier;
        }
    }
}
