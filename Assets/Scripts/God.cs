using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class God : MonoBehaviour
{
    [SerializeField] Text CountdownText;
    [SerializeField] GameObject BlockPrefab;
    [SerializeField] GameObject BallPrefab;
    private bool gameOver = false;
    private EdgeCollider2D collider2D;

    private Camera mainCamera;

    private int level = 1;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        collider2D = GetComponent<EdgeCollider2D>();
        
        AutoGen();
        DoCountDown();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForRestart();
        
        //This code sets the collider edges to the outside of the camera
        this.collider2D.points = new Vector2[] {
            mainCamera.ViewportToWorldPoint(new Vector2(0, 0)),
            mainCamera.ViewportToWorldPoint(new Vector2(0, 1)),
            mainCamera.ViewportToWorldPoint(new Vector2(1, 1)),
            mainCamera.ViewportToWorldPoint(new Vector2(1, 0)),
        };

        if(CheckGameWon()) {
            Console.WriteLine("You win");
        }
    }

    public void CheckForRestart()
    {
        if(CheckGameLost()) {
            SceneManager.LoadScene("GameOver");
        }

        if(CheckGameWon()) {
            SceneManager.LoadScene("LevelCompleted");
        }
    }

    private bool CheckGameWon() {
        var bricks = GameObject.FindGameObjectsWithTag("Brick");
        if(bricks == null || bricks.Length < 1)
            return true;

        return false;
    }

    private bool CheckGameLost() {
        return GameObject.FindGameObjectsWithTag("Ball").Length < 1;
    }

    private async void DoCountDown() {
        for (int i = 4; i > 0; i--)
        {
            CountdownText.text = i.ToString();
            await Task.Delay(1000);
        }

        CountdownText.text = "";
        GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody2D>().velocity = new Vector2(0, -13);
        GameObject.FindGameObjectWithTag("Paddle").GetComponent<Paddle>().Freeze = false;
    }

    /// <summary>This code generates the layout</summary>
    private void AutoGen()
    {
        BlockPrefab.GetComponent<BoxCollider2D>().sharedMaterial = new PhysicsMaterial2D() {
            friction = 0f,
            bounciness = 1f + (level/10),
        };
        var gobject = Instantiate(BlockPrefab);
        
        var width = gobject.GetComponent<RectTransform>().rect.width * gobject.transform.localScale.x;
        var height = gobject.GetComponent<RectTransform>().rect.height * gobject.transform.localScale.y; 

        var start = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0.6f));
        var stop = mainCamera.ViewportToWorldPoint(new Vector3(1f, 1f));
        
        start += new Vector3((stop.x % width), 0); 

        start += new Vector3(width/2, height/2);
        stop -= new Vector3(width/2, height/2);
        
        for (float i = start.y; i < stop.y;)
        {
            for (float j = start.x; j < stop.x;)
            {
                var newPos = new Vector3(j, i, 10f);
                gobject.transform.position = newPos;
                // gobject.transform.Rotate(new Vector3(0, 0, 90));
                gobject.GetComponent<SpriteRenderer>().color = UnityEngine.Random.ColorHSV();
                width = gobject.GetComponent<RectTransform>().rect.width * gobject.transform.localScale.x;
                height = gobject.GetComponent<RectTransform>().rect.height * gobject.transform.localScale.y;
                // gobject.GetComponent<Box>().Health = 2;

                gobject = Instantiate(BlockPrefab);
                j += width;
            }

            i += height;
        }

        Destroy(gobject);
    }
}
