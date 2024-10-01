using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        playerControls.Player.Fire.performed += Fire;
    }

    private void Fire(InputAction.CallbackContext context)
    {
        Instantiate(Bullet, gameObject.transform.position, Quaternion.identity);
    }

    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }



}
