using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpClass PowerUpVal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfBelow();
    }

    void CheckIfBelow()
    {
        var stop = Camera.main.ViewportToWorldPoint(new Vector2(0, -0.2f));
        if (this.transform.position.y < stop.y)
            Destroy(gameObject);       
    }
}
