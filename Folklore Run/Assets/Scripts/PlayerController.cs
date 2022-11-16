using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    [Header("Checking Touch")]
    private Vector2 startTouchPosition;
    //private Vector2 currentTouchPosition;
    private Vector2 endTouchPosition;
    //private bool stopTouch = false;

    public float swipeRange;
    public float tapRange;

    public Transform leftSpot, middleSpot, rightSpot;
    public float leftX = -2.5f;
    public float middleX = 0f;
    public float rightX = 2.5f;
    public float moveSpeed = 100f;

    [Header("Other Stuff")]
    public float slideTime = 1f;

    public int levelCount = 0;
    public GameObject[] levelsInstantiated;
    bool isSliding = false;


    void Start()
    {
        
    }
    void Update()
    {
        Swipe();
        WASD();
        Slide();
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

    public void WASD()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {

        }

        else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && transform.position.x > leftX)
        {
            transform.position += new Vector3(-2.5f, 0f, 0f);
        }

        else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && isSliding == false)
        {
            isSliding = true;
            Debug.Log("sliding");
            Invoke("GetBackUp", slideTime);
        }

        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && transform.position.x < rightX)
        {
            transform.position += new Vector3 (2.5f, 0f, 0f); 
        }

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

    public void GetBackUp()
    {

        isSliding = false;
        Debug.Log("stood up");
        
    }

    public void Slide()
    {
        if (isSliding)
        {
            transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
            transform.position = new Vector3(transform.position.x, -0.75f, transform.position.z);

        }
        else
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            transform.position = new Vector3(transform.position.x, -0.25f, transform.position.z);

        }
    }

}
