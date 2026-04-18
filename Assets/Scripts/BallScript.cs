using UnityEngine;
using TMPro;

public class BallScript : MonoBehaviour
{
    public float minY = -5.5f;
    public float maxVel = 15f;

    private int score = 0;
    private int lives = 5;
    private int brickCount;

    public Vector3 spawnLocation;

    public TextMeshProUGUI scoreTxT;
    public GameObject[] livesImage;

    public GameObject gameOverPanel;
    public GameObject youWinPanel;
    public GameObject LevelGenerator;

    public bool isGameOver = false;

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
        if(transform.position.y < minY)
        {
            if(lives <= 0 && !isGameOver)
            {
                isGameOver = true;
                GameOver();
                return;
            }
            transform.position = spawnLocation;
            rb.linearVelocity = Vector3.zero;
            lives--; //If ball goes below min y deduct life 

            if(lives >= 0)
            {
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
            collision.gameObject.SetActive(false);
            score += 10;
            scoreTxT.text = score.ToString("00000");
            brickCount--;
            if(brickCount <= 0)
            {
                YouWin();
            }
        }
    }



    public void resetGame()
    {
        score = 0;
        lives = 5;
        isGameOver = false;
        for (int i = 0; i < lives; i++)
        {
            livesImage[i].SetActive(true);
        }
    }


    void GameOver()
    {
        isGameOver=true;
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
