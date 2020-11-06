using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float padding;
    [SerializeField] float speed;
    private Vector2 startPos;

    public bool Freeze = true;

    // Start is called before the first frame update
    void Start()
    {
        Camera gameCamera = Camera.main;
        transform.position = gameCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.1f, 10));

    }

    // Update is called once per frame
    void Update()
    {
        if (!Freeze)
            Move();
    }

    private void Move()
    {
        float curr_x = transform.position.x;
        float curr_y = transform.position.y;

        var rectTransform = this.GetComponent<RectTransform>();
        var paddleWidth = rectTransform.rect.width * rectTransform.localScale.x;

        float delta_x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        //float new_x = Mathf.Clamp(curr_x + delta_x, xMin, xMax);
        float new_x = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0)).x;

        var min = Camera.main.ViewportToWorldPoint(new Vector2(0.0f, 0)) + new Vector3(paddleWidth/2, 0);
        var max = Camera.main.ViewportToWorldPoint(new Vector2(1.0f, 0)) - new Vector3(paddleWidth/2, 0);
        new_x = Mathf.Clamp(new_x, min.x , max.x);
        
        transform.position = new Vector2(new_x, curr_y);

        //transform.Rotate(new Vector3(0, 0, delta_x));
        // transform.eulerAngles = new Vector3(0, 0, 1) * delta_x * -15;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
