using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] GameObject PowerUpPrefab;
    [SerializeField] GameObject BallPrefab;

    public int Health = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Ball"))
        {
            Health--;
            if (Health < 0)
            {
                var rand = UnityEngine.Random.Range(0, 10);
                if (rand == 0)
                    DropExpandPowerUp();

                if (rand == 1)
                    DropMultiballPowerUp();

                Destroy(this.gameObject);
            }
        }
    }

    public void DropExpandPowerUp()
    {
        var go = Instantiate(PowerUpPrefab);
        go.transform.position = new Vector3(transform.position.x, transform.position.y);
        go.GetComponent<PowerUp>().PowerUpVal = new PowerUpClass(
                async gameObject =>
                {
                    var delta = new Vector3(0.1f, 0.1f);
                    var time = 100f;

                    for (var i = 0; i < time / 5; i++)
                    {
                        gameObject.GetComponent<RectTransform>().localScale += delta / time;
                        await Task.Delay(5);
                    }
                },
                async gameObject =>
                {
                    var delta = new Vector3(0.1f, 0.1f);
                    var time = 100f;

                    for (var i = 0; i < time / 5; i++)
                    {
                        gameObject.GetComponent<RectTransform>().localScale -= delta / time;
                        await Task.Delay(5);
                    }
                }, 3000);
    }

    public void DropMultiballPowerUp()
    {
        var go = Instantiate(BallPrefab);
        go.transform.position = transform.position;
        go.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -13);
    }
}
