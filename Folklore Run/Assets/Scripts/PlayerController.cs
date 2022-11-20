using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    public bool isWithBoto = false;

    public GameObject javali;
    public float javaliTimer;
    public float javaliSpeed;
    public float javaliJumpHeight;

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
        Slide();
        Jump();

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

            if (endTouchPosition.x > startTouchPosition.x && transform.position.x < rightSpot.position.x) // Right Swipe
            {
                Debug.Log("Right");
                if (transform.position.x == 0)
                    transform.position = rightSpot.position;
                else
                    transform.position = middleSpot.position;

            }

            if (endTouchPosition.x < startTouchPosition.x && transform.position.x > leftSpot.position.x) // Left Swipe
            {
                Debug.Log("Left");
                if (transform.position.x == 0)
                    transform.position = leftSpot.position;
                else
                    transform.position = middleSpot.position;
            }

            if (endTouchPosition.y > startTouchPosition.y) // Up Swipe
            {
                Debug.Log("Up");
            }

            if (endTouchPosition.y < startTouchPosition.y) // Down Swipe
            {
                Debug.Log("Down");
            }
        }


    }

    public void WASD() // playing on pc, for debugging
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isJumping == false && isPaused == false)
        {
            StartCoroutine(Attack());

            //isJumping = true;
            //Invoke("BaseState", jumpTime);
            StartCoroutine(Jumppp());
        }

        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && transform.position.x > leftX && isPaused == false)
        {
            StartCoroutine(Attack());

            transform.position += new Vector3(-2.5f, 0f, 0f);
        }

        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && isSliding == false && isPaused == false)
        {
            StartCoroutine(Attack());

            isSliding = true;
            Debug.Log("sliding");
            Invoke("BaseState", slideTime);
        }

        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && transform.position.x < rightX && isPaused == false)
        {
            StartCoroutine(Attack());

            transform.position += new Vector3(2.5f, 0f, 0f);
        }


        /***************** Other Controls *****************/
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
        if (levelCount >= 2)
        {
            Destroy(levelsInstantiated[0]);

        }

    }

    public void BaseState()
    {
        isJumping = false;
        isSliding = false;
        Debug.Log("stood up");

    }

    public void Slide()
    {
        if (isSliding)
        {
            isJumping = false;
            transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
            transform.position = new Vector3(transform.position.x, -0.75f, transform.position.z);

        }
        else if (isSliding == false /*&& isJumping == false*/)
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            if (isWithBoto == false)
                transform.position = new Vector3(transform.position.x, -0.25f, transform.position.z);

        }
    }

    public void Jump()
    {
        if (isJumping && isWithBoto == false)
        {
            isSliding = false;
            transform.position = new Vector3(transform.position.x, jumpHeight, transform.position.z);

        }
        else if (isSliding == false && isJumping == false && isWithBoto == false)
        {
            transform.position = new Vector3(transform.position.x, -0.25f, transform.position.z);

        }
        if (isJumping && isWithBoto)
        {
            isSliding = false;
            transform.position = new Vector3(transform.position.x, botoJump, transform.position.z);

        }

    }

    IEnumerator Jumppp()
    {
        isJumping = true;

        yield return new WaitForSeconds(jumpTime);

        isJumping = false;


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

    IEnumerator Boitata()
    {
        movement.moveSpeed = boitataSpeed;
        boitata.SetActive(true);

        yield return new WaitForSeconds(boitataTimer);

        movement.moveSpeed = movement.maxSpeed / 2;
        boitata.SetActive(false);
    }

    IEnumerator Boto()
    {
        isWithBoto = true;
        movement.transform.position = new Vector3(movement.transform.position.x, -12f, movement.transform.position.z);
        cameraPivot.transform.position = new Vector3(cameraPivot.transform.position.x, cameraPivot.transform.position.y -3f, cameraPivot.transform.position.z);


        yield return new WaitForSeconds(botoTimer);

        isWithBoto = false;
        movement.transform.position = new Vector3(movement.transform.position.x, 0f, movement.transform.position.z);
        cameraPivot.transform.position = new Vector3(cameraPivot.transform.position.x, cameraPivot.transform.position.y, cameraPivot.transform.position.z);


    }

    IEnumerator Javali()
    {
        float temp = jumpHeight;
        jumpHeight = javaliJumpHeight;

        yield return new WaitForSeconds(javaliTimer);

        jumpHeight = temp;
    }
}
