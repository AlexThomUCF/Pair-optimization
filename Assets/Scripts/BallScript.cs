using UnityEngine;
using TMPro;

public class BallScript : MonoBehaviour
{
    public float minY = -5.5f;
    public float maxVel = 15f;

    private int score = 0;
    private int lives = 5;
    public int brickCount;

    public Vector3 spawnLocation;

    public TextMeshProUGUI scoreTxT;
    public GameObject[] livesImage;

    public GameObject gameOverPanel;
    public GameObject youWinPanel;
    public GameObject LevelGenerator;

    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnLocation = transform.position;
        rb = GetComponent<Rigidbody2D>();
        
        brickCount = LevelGenerator.transform.childCount;

        
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.linearVelocity.sqrMagnitude > maxVel * maxVel)
        {
            rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxVel);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Brick"))
        {
            collision.gameObject.SetActive(false);
            score += 10;
            scoreTxT.SetText("{0:00000}", score);
            brickCount--;
            if(brickCount <= 0)
            {
                YouWin();
            }
        }
        else if (collision.collider.CompareTag("Death"))
        {
            if (lives <= 0)
            {
                GameOver();
                //return;
            }
            else
            {
                transform.position = spawnLocation;
                rb.linearVelocity = Vector3.zero;
                lives--; //If ball goes below min y deduct life
                livesImage[lives].SetActive(false);
            }
        }
    }


    public void resetGame()
    {
        score = 0;
        lives = 5;
        transform.position = spawnLocation;
        scoreTxT.SetText("{0:00000}", score);
        brickCount = LevelGenerator.transform.childCount;
        gameObject.SetActive(true);
        for (int i = 0; i < lives; i++)
        {
            livesImage[i].SetActive(true);
        }
    }


    void GameOver()
    {
        gameObject.SetActive(false);
        transform.position = spawnLocation;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        
        //Destroy(gameObject);
    }

    void YouWin()
    {
        youWinPanel.SetActive(true);
        Time.timeScale = 0;
    }



}
