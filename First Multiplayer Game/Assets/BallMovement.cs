using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponent<Transform>().position.x < -9)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 5, ForceMode2D.Impulse);
        }
        if (gameObject.GetComponent<Transform>().position.x > 9)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 5, ForceMode2D.Impulse);
        }
        if (gameObject.GetComponent<Transform>().position.y > 5)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 5, ForceMode2D.Impulse);
        }
        if (gameObject.GetComponent<Transform>().position.y < -5)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        }
    }
}
