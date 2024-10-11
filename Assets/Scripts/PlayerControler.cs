
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    PlayerControls playerControls;
    private InputAction move;
    private InputAction fire;

    [SerializeField]
    GameObject Bullet;

    [SerializeField]
    int health = 3;

    [SerializeField]
    List<Image> liveImages;

    [SerializeField]
    float cooldownTime;

    float cooldown;
    


    Vector2 moveDirection = Vector2.zero;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        fire = playerControls.Player.Fire;
        move.Enable();
        fire.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerControls.Player.Fire.started += Fire;
    }

    private void Fire(InputAction.CallbackContext context)
    {

        if (cooldown <= 0)
        {
        Instantiate(Bullet, gameObject.transform.position, Quaternion.identity);
            cooldown = cooldownTime;
        }

    }

    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }

        moveDirection = move.ReadValue<Vector2>();

        for (int i = 0; i < liveImages.Count; i++)
        {
            if (i < health)
            {
                liveImages[i].enabled = true;
            }
            else
            {
                liveImages[i].enabled = false;
            }
        }


        if (health == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            health--;
        }
    }


}
