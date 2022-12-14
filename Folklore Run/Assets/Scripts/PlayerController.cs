using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    [Header("--- Check Touch ---")]
    private Vector2 startTouchPosition;
    //private Vector2 currentTouchPosition;
    private Vector2 endTouchPosition;
    //private bool stopTouch = false;

    public float swipeRange;
    public float tapRange;

    [Header("--- Movement and Attack ---")]
    public PlayerMovement movement;

    public Transform leftSpot, middleSpot, rightSpot;
    public float leftX = -2.5f;
    public float middleX = 0f;
    public float rightX = 2.5f;

    public float slideTime = 1f;
    public bool isSliding = false;

    public bool isJumping = false;
    public float jumpHeight = 2f;
    public float jumpTime = 1f;

    public bool isPaused = false;
    public bool isAttacking = false;
    public float attackTime = 0.5f;

    [Header("--- Power Ups ---")]

    public GameObject boitata;
    public float boitataTimer;
    public float boitataSpeed;

    public GameObject boto;
    public float botoTimer;
    public float botoSpeed;
    public float botoJump;
    public float botoSlide;
    public bool isWithBoto = false;

    public GameObject javali;
    public float javaliTimer;
    public float javaliSpeed;
    public float javaliJumpHeight;
    public bool isWithJavali = false;

    [Header("--- Animation ---")]
    public Animator animator;



    [Header ("--- Other Stuff ---")]

    public int coins;
    public int score;

    public int levelCount = 0;
    public GameObject[] levelsInstantiated;

    public GameObject cameraPivot;

    void Awake()
    {
        boitata = GameObject.FindGameObjectWithTag("Boitata");
        boitata.SetActive(false);
        movement = FindObjectOfType<PlayerMovement>();
    }
    void Update()
    {

        Swipe();
        WASD();

    }


    public void Swipe()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;

            if (endTouchPosition.x > startTouchPosition.x && transform.position.x < rightSpot.position.x && isPaused == false) // Right Swipe
            {
                StartCoroutine(Attack());

                transform.position += new Vector3(2.5f, 0f, 0f);

            }

            if (endTouchPosition.x < startTouchPosition.x && transform.position.x > leftSpot.position.x && isPaused == false) // Left Swipe
            {

                StartCoroutine(Attack());

                transform.position += new Vector3(-2.5f, 0f, 0f);
            }

            if (endTouchPosition.y > startTouchPosition.y && isJumping == false && isPaused == false) // Up Swipe
            {
                StartCoroutine(Attack());

                StartCoroutine(Jump());
            }

            if (endTouchPosition.y < startTouchPosition.y && isSliding == false && isPaused == false) // Down Swipe
            {
                StartCoroutine(Attack());

                Debug.Log("sliding");

                if (isWithBoto == false)
                    StartCoroutine(Slide());
            }
        }


    }

    public void WASD() // playing on pc, for debugging
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isJumping == false && isPaused == false)
        {
            StartCoroutine(Attack());

            StartCoroutine(Jump());
        }

        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && transform.position.x > leftX && isPaused == false)
        {
            StartCoroutine(Attack());

            transform.position += new Vector3(-2.5f, 0f, 0f);
        }

        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && isSliding == false && isPaused == false)
        {
            StartCoroutine(Attack());

            Debug.Log("sliding");

            if (isWithBoto == false)
                StartCoroutine(Slide());

        }

        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && transform.position.x < rightX && isPaused == false)
        {
            StartCoroutine(Attack());

            transform.position += new Vector3(2.5f, 0f, 0f);
        }


        /***************** Other Controls / Cheats *****************/
        if (Input.GetKeyDown(KeyCode.P))
            Pause();

        if (Input.GetKeyDown(KeyCode.M))
            StartCoroutine(Boitata());

        if (Input.GetKeyDown(KeyCode.B))
            StartCoroutine(Boto());

        if (Input.GetKeyDown(KeyCode.J))
            StartCoroutine(Javali());
    }

    public void HandleLevel()
    {
        levelCount = levelsInstantiated.Count();
        levelsInstantiated = GameObject.FindGameObjectsWithTag("Level");
        if (levelCount >= 4)
        {
            Destroy(levelsInstantiated[0]);

        }

    }

    IEnumerator Slide()
    {
        isSliding = true;

        isJumping = false;
        transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
        if (isWithBoto == false)
            transform.position = new Vector3(transform.position.x, -0.75f, transform.position.z);
        
        animator.SetBool("shouldSlide", true);


        yield return new WaitForSeconds(slideTime);

        isSliding = false;

        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        if (isWithBoto == false)
            transform.position = new Vector3(transform.position.x, -0.25f, transform.position.z);

        animator.SetBool("shouldSlide", false);

    }

    IEnumerator Jump()
    {
        isJumping = true;

        if (isWithBoto == false)
        {
            isSliding = false;
            transform.position = new Vector3(transform.position.x, jumpHeight, transform.position.z);

        }
        if (isWithJavali)
            animator.SetBool("shouldJavaliJump", true);
        else
            animator.SetBool("shouldJump", true);
        animator.speed = 0.5f;

        yield return new WaitForSeconds(jumpTime);

        isJumping = false;
        if (isSliding == false && isWithBoto == false)
        {
            transform.position = new Vector3(transform.position.x, -0.25f, transform.position.z);

        }
        animator.SetBool("shouldJavaliJump", false);
        animator.SetBool("shouldJump", false);

        animator.speed = 1f;

    }


    public void Pause()
    {
        if (isPaused == false)
        {
            Time.timeScale = 0f;
            isPaused = true;
        }
        else if (isPaused)
        {
            Time.timeScale = 1f;
            movement.moveSpeed = movement.moveSpeed / 2;
            isPaused = false;
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;

        yield return new WaitForSeconds(attackTime);

        isAttacking = false;
    }

    public IEnumerator Boitata()
    {
        movement.moveSpeed = boitataSpeed;
        boitata.SetActive(true);
        animator.SetBool("shouldBoitata", true);

        yield return new WaitForSeconds(boitataTimer);

        movement.moveSpeed = movement.maxSpeed / 2;
        boitata.SetActive(false);
        animator.SetBool("shouldBoitata", false);

    }

    public IEnumerator Boto()
    {
        isWithBoto = true;
        movement.transform.position = new Vector3(movement.transform.position.x, -12f, movement.transform.position.z);
        animator.SetBool("shouldBoto", true);


        yield return new WaitForSeconds(botoTimer);

        isWithBoto = false;
        movement.transform.position = new Vector3(movement.transform.position.x, 0f, movement.transform.position.z);
        animator.SetBool("shouldBoto", false);


    }

    public IEnumerator Javali()
    {
        float temp = jumpHeight;
        jumpHeight = javaliJumpHeight;
        animator.SetBool("shouldJavali", true);
        isWithJavali = true;

        yield return new WaitForSeconds(javaliTimer);

        jumpHeight = temp;
        animator.SetBool("shouldJavali", false);
        animator.SetBool("shouldJavaliJump", false);
        isWithJavali = false;

    }
    

    public void Die()
    {
        SceneManager.LoadScene(0);

    }
}
