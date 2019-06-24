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
    private GameObject crosshairPrefab;
    [SerializeField]
    [Range(1.5f, 5f)]
    private float crosshairDistanceToPlayer;


    private GameObject crosshair;
    private Vector3 crosshairSize;
    private float groundedTimer;
    private float keyPressedTimer;
    private Rigidbody2D rb;
    private float moveX;
    private Vector3 velocity = Vector3.zero;
    private bool isJumping;
    private bool isDonePlaying;
    private int direction;
    private bool canMove;
    private bool canFlip;
    private bool canJump;
    private float jumpPressed;
    

    public int Direction { get => direction; set => direction = value; }
    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }
    public bool CanMove { get => canMove; set => canMove = value; }
    public bool CanFlip { get => canFlip; set => canFlip = value; }
    public bool CanJump { get => canJump; set => canJump = value; }

    //Festlegen von Variablen und Objekten
    void Start()
    {
        crosshair = Instantiate(crosshairPrefab);
        rb = gameObject.GetComponent<Rigidbody2D>();
        Cursor.visible = false;
        isDonePlaying = false;
        canMove = true;
        canFlip = true;
        canJump = true;
        crosshairSize = crosshair.transform.localScale;
        //StartCoroutine(AudioFadeOut.FadeOut(FindObjectOfType<AudioManager>().GetSource("MainMenu"), 1.0f));
    }

    //Funktionsaufruf der anderen Funktionen, Crosshairplacement auf die Mausposition, Animationen werden abgespielt und Timer werden gesetzt/abgezogen.
    void Update()
    {
        if (!GetComponent<PlayerHealth>().IsDead && !GetComponent<PauseMenu>().GamePaused)
        {
            if(!GameData.options.UseController)
            {
                crosshair.transform.position = new Vector3(
                    Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                    Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
                    transform.position.z);
            }
            else if(!GameData.options.UseXbox)
            {
                Vector3 stickPosition = new Vector3(transform.position.x + Input.GetAxis("HorizontalAim") * crosshairDistanceToPlayer, transform.position.y + Input.GetAxis("VerticalAim") * crosshairDistanceToPlayer);
                crosshair.transform.position = new Vector3(
                    stickPosition.x,
                    stickPosition.y,
                    transform.position.z);

                if (stickPosition == transform.position)
                {
                    crosshair.transform.localScale = Vector3.zero;
                }
                else
                {
                    crosshair.transform.localScale = crosshairSize;
                }
            }
            else
            {
                Vector3 stickPosition = new Vector3(transform.position.x + Input.GetAxis("HorizontalXbox") * crosshairDistanceToPlayer, transform.position.y + Input.GetAxis("VerticalXbox") * crosshairDistanceToPlayer);
                crosshair.transform.position = new Vector3(
                    stickPosition.x,
                    stickPosition.y,
                    transform.position.z);

                if (stickPosition == transform.position)
                {
                    crosshair.transform.localScale = Vector3.zero;
                }
                else
                {
                    crosshair.transform.localScale = crosshairSize;
                }
            }

            if (canMove)
            {
                moveX = Input.GetAxis("Horizontal");

                if (Mathf.Abs(moveX) != 0)
                {
                    GetComponent<Animator>().SetBool("isMoving", true);
                    GetComponent<Animator>().SetFloat("moveSpeed", Mathf.Abs(moveX));
                }
                else
                {
                    GetComponent<Animator>().SetBool("isMoving", false);
                }

                if (isGrounded)
                    GetComponent<Animator>().SetBool("isJumping", false);
                else if(!isGrounded && jumpPressed < 1)
                    GetComponent<Animator>().SetBool("isJumping", true);

                groundedTimer -= Time.deltaTime;
                keyPressedTimer -= Time.deltaTime;

                move();
                jump();
                flip();
                falling();
            }
        }

        if (GetComponent<PauseMenu>().GamePaused || GetComponent<PlayerHealth>().IsDead)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void falling()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1), Vector2.down, .5f);

        if(hit.collider == null)
            GetComponent<Animator>().SetBool("isJumping", true);
    }

    private void FixedUpdate()
    {
        //Marcel
        //if (FindObjectOfType<AudioManager>().GetSource("MainMenu").volume > 0 && isDonePlaying == false)
        //{
        //    FindObjectOfType<AudioManager>().GetSource("MainMenu").volume -= 0.003f;
        //}
        //else
        //{
        //    FindObjectOfType<AudioManager>().GetSource("MainMenu").Stop();
        //    FindObjectOfType<AudioManager>().GetSource("MainMenu").volume = 0.5f;
        //    isDonePlaying = true;
        //}
    }

    //Abhängig von der Richtung, in die der Spieler drückt, bewegt sich der Charakter auch.
    private void move()
    {
        if (isGrounded)
        {
            //Marcel
            //if (FindObjectOfType<AudioManager>().GetSource("TwoFootsteps").isPlaying != true && moveX != 0)
            //{
            //    FindObjectOfType<AudioManager>().GetSource("TwoFootsteps").Play();
            //}
            //else if (moveX == 0)
            //{
            //    FindObjectOfType<AudioManager>().GetSource("TwoFootsteps").Stop();
            //}
        }
        else
        {
            //Marcel
            //FindObjectOfType<AudioManager>().GetSource("TwoFootsteps").Stop();
        }
        Vector3 targetVelocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, moveSmooth);
    }

    //Hier wird erst getestet, ob der Spieler den Boden berührt oder nicht. Einfach gesagt kann dieser nur Springen, wenn er den Boden berührt.
    private void jump()
    {
        if(canJump)
        {
            if (isGrounded)
                groundedTimer = groundedTimerInput;

            if(!GameData.options.UseXbox)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    keyPressedTimer = keyPressedTimerInput;
                    jumpPressed = 0;
                }
                else
                {
                    jumpPressed += Time.deltaTime;
                }

                if (Input.GetButtonUp("Jump"))
                {
                    if (rb.velocity.y > 0)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpForceReduced);
                    }
                }
            }
            else
            {
                if (Input.GetButtonDown("JumpXbox"))
                {
                    keyPressedTimer = keyPressedTimerInput;
                    jumpPressed = 0;
                }
                else
                {
                    jumpPressed += Time.deltaTime;
                }

                if (Input.GetButtonUp("JumpXbox"))
                {
                    if (rb.velocity.y > 0)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpForceReduced);
                    }
                }
            }

            if ((groundedTimer > 0) && (keyPressedTimer > 0))
            {
                groundedTimer = 0;
                keyPressedTimer = 0;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }

    //Hier wird geschaut, ob der Spieler sich in eine Richtung bewegt und dementsprechend wird er in Laufrichtung rotiert.
    //Wenn er aber nicht läuft wird die Mausposition überprüft. Abhängig davon wird der Spieler in die Richtung der Maus rotiert.
    private void flip()
    {
        if(canFlip)
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
                if(!GameData.options.UseController)
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
    }
}
