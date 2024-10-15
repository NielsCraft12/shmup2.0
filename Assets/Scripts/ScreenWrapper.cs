using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        // Get the object pos in pixels
        Vector3 screenpos = Camera.main.WorldToScreenPoint(transform.position);  

        // get the right side of the screen
        float rightSideOfScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height)).x;
        float leftSideOfScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;

        // player going out of sceen on the left
        if (screenpos.x <= 0 && rb.velocity.x < 0)
        {
            transform.position = new Vector2(rightSideOfScreenInWorld,transform.position.y);
        }
        // player going out of sceen on the right
        else if (screenpos.x >= Screen.width && rb.velocity.x > 0)
        {
            transform.position = new Vector2(leftSideOfScreenInWorld, transform.position.y);
        }
    }
}
