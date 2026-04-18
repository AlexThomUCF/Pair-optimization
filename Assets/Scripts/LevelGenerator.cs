using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    public Vector2Int size;
    public Vector2 offset;
    public GameObject brick;
    public List<GameObject> brickList;
    public int brickTotal;

    public GameObject gameOverPanel;
    public GameObject youWinPanel;
    public GameObject Ball;
    public GameObject Player;

    public BallScript BallScript;
    public BarMovement BarMovement;

    public void Awake()
    {
        brickTotal = 0;
        brickList = new List<GameObject>();
        InitializedPosition();
        brickTotal = transform.childCount;

        BallScript = Ball.GetComponent<BallScript>();
        BarMovement = Player.GetComponent<BarMovement>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        BarMovement.resetPlayer();
        BallScript.resetGame();
        Time.timeScale = 1.0f;

        for (int i = 0; i < brickList.Count; i++)
        {
            if (!brickList[i].activeSelf)
            {
                brickList[i].SetActive(true);
            }
        }

        if (gameOverPanel.activeSelf)
        {
            gameOverPanel.SetActive(false);
        }
        else
        {
            youWinPanel.SetActive(false);
        }

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void InitializedPosition()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                GameObject newBrick = Instantiate(brick, transform);
                newBrick.transform.position = transform.position + new Vector3((float)((size.x - 1) * .5f - i) * offset.x, j * offset.y, 0);
                brickList.Add(newBrick);
            }
        }
    }
}
