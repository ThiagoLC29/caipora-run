using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIText : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI myText;
    public PlayerController controller;

    private void Start()
    {
        controller = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        myText.text = "Score: " + controller.score + "\nCoins: " + controller.coins;
    }

}
