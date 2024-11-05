
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
   public int health = 3;

    [SerializeField]
    List<Image> liveImages;

    [SerializeField]
    float cooldownTime;

    float cooldown;
    private SaveData saveData;
    private ScoreEntry scoreEntry;
    private List<ScoreEntry> entries;

    [SerializeField]
   private  string currentUserName;
    [SerializeField]
   private int currentScore;


    Vector2 moveDirection = Vector2.zero;

    private void Awake()
    {
       // SaveGame();
        //LoadGame();
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

       // ScoreBord.instance.AddScore(currentUserName, currentScore);
       
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
            HighscoreManager.Instance.AddHighscore();
            SceneManager.LoadScene("GameOver");
        }
    }

    private void FixedUpdate()
    {
        float rotationSpeed = 5f; // Adjust the speed as needed

        if (moveDirection == new Vector2(-1, 0))
        {
            float targetAngle = 12f;
            float currentAngle = transform.eulerAngles.z;
            float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, newAngle);
        }

        if (moveDirection == new Vector2(1, 0))
        {
            float targetAngle = -12f;
            float currentAngle = transform.eulerAngles.z;
            float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, newAngle);
        }

        if (moveDirection == new Vector2(0, 0))
        {
            float targetAngle = 0f;
            float currentAngle = transform.eulerAngles.z;
            float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, newAngle);
        }


        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            health--;
        }
        if(collision.gameObject.tag == "EnemyBullet")
        {
            health--;
            Destroy(collision.gameObject);
        }
    }


/*    public void LoadGame()
    {
        if (saveData == null)
        {
            saveData = new SaveData();
        }

      //  saveData = SaveSystem.DeSerializeData();

        entries = saveData.entries;
    }*/

}
