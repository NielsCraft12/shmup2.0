using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
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
    public int health = 3;

    [SerializeField]
    List<Image> liveImages;

    [SerializeField]
    float cooldownTime;

    float hurttimer;
    SpriteRenderer sprite;

    [SerializeField]
    float iframeTime;

    float cooldown;
    private SaveData saveData;
    private ScoreEntry scoreEntry;
    private List<ScoreEntry> entries;

    [SerializeField]
    private string currentUserName;

    [SerializeField]
    private int currentScore;
    bool canTakeDamage = true;

    BoxCollider2D boxCollider;

    Vector2 moveDirection = Vector2.zero;

    private void Awake()
    {
        playerControls = new PlayerControls();
        sprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
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

        if (health <= 0)
        {
            HighscoreManager.Instance.AddHighscore();
            SceneManager.LoadScene("GameOver");
        }

        if (hurttimer > 0)
        {
            hurttimer -= Time.deltaTime;

            if (hurttimer <= 0)
            {
                sprite.color = Color.white;
                hurttimer = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        float rotationSpeed = 5f;

        if (moveDirection == new Vector2(-1, 0))
        {
            float targetAngle = 12f;
            float currentAngle = transform.eulerAngles.z;
            float newAngle = Mathf.LerpAngle(
                currentAngle,
                targetAngle,
                rotationSpeed * Time.deltaTime
            );
            transform.rotation = Quaternion.Euler(0, 0, newAngle);
        }

        if (moveDirection == new Vector2(1, 0))
        {
            float targetAngle = -12f;
            float currentAngle = transform.eulerAngles.z;
            float newAngle = Mathf.LerpAngle(
                currentAngle,
                targetAngle,
                rotationSpeed * Time.deltaTime
            );
            transform.rotation = Quaternion.Euler(0, 0, newAngle);
        }

        if (moveDirection == new Vector2(0, 0))
        {
            float targetAngle = 0f;
            float currentAngle = transform.eulerAngles.z;
            float newAngle = Mathf.LerpAngle(
                currentAngle,
                targetAngle,
                rotationSpeed * Time.deltaTime
            );
            transform.rotation = Quaternion.Euler(0, 0, newAngle);
        }

        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            iframeTime = .3f;
            sprite.color = new Color(1, 0, 0);
            if (canTakeDamage)
            {
                health--;
            }
            hurttimer = 0.1f;
            StartCoroutine(IframeFlash());
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "EnemyBullet")
        {
            sprite.color = new Color(1, 0, 0);
            if (canTakeDamage)
            {
                health--;
            }
            iframeTime = .3f;
            hurttimer = 0.1f;
            StartCoroutine(IframeFlash());
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator IframeFlash()
    {
        bool flashState = true;

        while (iframeTime > 0)
        {
            canTakeDamage = false;

            iframeTime -= Time.deltaTime;

            if (flashState)
            {
                sprite.enabled = false;
                flashState = false;
                yield return new WaitForSeconds(0.1f);
            }
            if (!flashState)
            {
                flashState = true;
                sprite.enabled = true;
                yield return new WaitForSeconds(0.1f);
            }
        }
        if (iframeTime <= 0)
        {
            canTakeDamage = true;
            flashState = true;
        }
    }
}
