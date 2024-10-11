using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{

    [SerializeField]
    GameObject backGround1;
    [SerializeField]
    GameObject backGround2;

    [SerializeField]
    Transform player;
    [SerializeField]
    float speed = 5;

    void Update()
    {
        backGround1.transform.Translate(Vector2.down * speed * Time.deltaTime);
        backGround2.transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (backGround1.transform.position.y <= -62)
        {
            backGround1.transform.position = new Vector2(-3, backGround2.transform.position.y + 62);
        } if (backGround2.transform.position.y <= -62)
        {
            backGround2.transform.position = new Vector2(-3, backGround1.transform.position.y + 62);
        }

    }
}
