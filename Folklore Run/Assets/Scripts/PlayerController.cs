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

    [Header("Other Stuff")]
    public int levelCount = 0;
    public GameObject[] levelsInstantiated;


    void Start()
    {
        
    }
    void Update()
    {
        Swipe();
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

    public void HandleLevel()
    {
        levelCount = levelsInstantiated.Count();
        levelsInstantiated = GameObject.FindGameObjectsWithTag("Level");
        if (levelCount >= 2)
        {
            Destroy(levelsInstantiated[0]);

        }

    }

}
