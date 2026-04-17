using UnityEngine;
using TMPro;

public class BallScript : MonoBehaviour
{
    public float minY = -5.5f;
    public float maxVel = 15f;

    private int score = 0;
    private int lives = 5;
    private int brickCount;

    public TextMeshProUGUI scoreTxT;
    public GameObject[] livesImage;

    public GameObject gameOverPanel;
    public GameObject youWinPanel;

    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        brickCount = FindObjectOfType<LevelGenerator>().transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < minY)
        {
            if(lives <= 0)
            {
                GameOver();
            }
            else
            {
                transform.position = Vector3.zero;
                rb.linearVelocity = Vector3.zero;
                lives--; //If ball goes below min y deduct life 
                livesImage[lives].SetActive(false);
            }
            
        }

        if(rb.linearVelocity.magnitude > maxVel)
        {
            rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxVel);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject);
            score += 10;
            scoreTxT.text = score.ToString("00000");
            brickCount--;
            if(brickCount <= 0)
            {
                YouWin();
            }
        }
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        Destroy(gameObject);
    }

    void YouWin()
    {
        youWinPanel.SetActive(true);
        Time.timeScale = 0;
        
    }

}
