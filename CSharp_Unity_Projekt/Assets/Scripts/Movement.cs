using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private int jumpCountInput;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    private float moveSmooth;
    [SerializeField]
    private float airMoveSmooth;
    [SerializeField]
    [Range(0, 1)]
    private float jumpForceReduced;
    [SerializeField]
    private float groundedTimerInput;
    [SerializeField]
    private float keyPressedTimerInput;
    [SerializeField]
    private GameObject riggedPlayer;
    [SerializeField]
    private GameObject crosshairPrefab;

    private GameObject crosshair;
    private float groundedTimer;
    private float keyPressedTimer;
    private Rigidbody2D rb;
    private float moveX;
    private Vector3 velocity = Vector3.zero;
    private bool isJumping;
    private bool isDonePlaying;
    private int direction;

    public int Direction { get => direction; set => direction = value; }
    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }
    
    //Festlegen von Variablen und Objekten
    void Start()
    {
        crosshair = Instantiate(crosshairPrefab);
        rb = gameObject.GetComponent<Rigidbody2D>();
        Cursor.visible = false;
        isDonePlaying = false;
        //StartCoroutine(AudioFadeOut.FadeOut(FindObjectOfType<AudioManager>().GetSource("MainMenu"), 1.0f));
    }

    //Funktionsaufruf der anderen Funktionen, Crosshairplacement auf die Mausposition, Animationen werden abgespielt und Timer werden gesetzt/abgezogen.
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        crosshair.transform.position = new Vector3(
            Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 
            transform.position.z);

        if (Mathf.Abs(moveX) >= 0.3)
        {
            riggedPlayer.GetComponent<Animator>().SetBool("isRunning", true);
        }
        else
        {
            riggedPlayer.GetComponent<Animator>().SetBool("isRunning", false);
        }

        if(isGrounded)
            riggedPlayer.GetComponent<Animator>().SetBool("isJumping", false);
        else
            riggedPlayer.GetComponent<Animator>().SetBool("isJumping", true);

        groundedTimer -= Time.deltaTime;
        keyPressedTimer -= Time.deltaTime;

        move();
        jump();
        flip();
    }

    private void FixedUpdate()
    {
        if (FindObjectOfType<AudioManager>().GetSource("MainMenu").volume > 0 && isDonePlaying == false)
        {
            FindObjectOfType<AudioManager>().GetSource("MainMenu").volume -= 0.003f;
        }
        else
        {
            FindObjectOfType<AudioManager>().GetSource("MainMenu").Stop();
            FindObjectOfType<AudioManager>().GetSource("MainMenu").volume = 0.5f;
            isDonePlaying = true;
        }
    }

    //Abhängig von der Richtung, in die der Spieler drückt, bewegt sich der Charakter auch.
    private void move()
    {

        if (isGrounded)
        {
            if (FindObjectOfType<AudioManager>().GetSource("TwoFootsteps").isPlaying != true && moveX != 0)
            {
                FindObjectOfType<AudioManager>().GetSource("TwoFootsteps").Play();
            }
            else if (moveX == 0)
            {
                FindObjectOfType<AudioManager>().GetSource("TwoFootsteps").Stop();
            }
        }
        else
        {
            FindObjectOfType<AudioManager>().GetSource("TwoFootsteps").Stop();
        }
        Vector3 targetVelocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, moveSmooth);
    }

    //Hier wird erst getestet, ob der Spieler den Boden berührt oder nicht. Einfach gesagt kann dieser nur Springen, wenn er den Boden berührt.
    private void jump()
    {       
        if(isGrounded)
            groundedTimer = groundedTimerInput;

        if (Input.GetKeyDown(KeyCode.W))
            keyPressedTimer = keyPressedTimerInput;

        if (Input.GetKeyUp(KeyCode.W))
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpForceReduced);
            }
        }

        if ((groundedTimer > 0) && (keyPressedTimer > 0))
        {
            groundedTimer = 0;
            keyPressedTimer = 0;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    //Hier wird geschaut, ob der Spieler sich in eine Richtung bewegt und dementsprechend wird er in Laufrichtung rotiert.
    //Wenn er aber nicht läuft wird die Mausposition überprüft. Abhängig davon wird der Spieler in die Richtung der Maus rotiert.
    private void flip()
    {
        if (moveX < 0)
        {
            this.gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
            direction = -1;
        }
        else if (moveX > 0)
        {
            this.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            direction = 1;
        }
        else if (moveX == 0)
        {

            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
            {
                this.gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
                direction = -1;
            }
            else
            {
                this.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
                direction = 1;
            }
        }
    }
}
