using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceScript : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 startPos;
    private Vector3 endPos;
    private float x_swipeLength;
    private float y_swipeLength;
    public Text diceNumber;
    private int dice = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        diceNumber.text = dice.ToString();
    }

    public void InputMouse()
    {
        startPos = Input.mousePosition;
        Debug.Log("ImputMouse");
    }
    public void MouseUp()
    {
        endPos = Input.mousePosition;
        x_swipeLength = (endPos.x - startPos.x) / 30;
        y_swipeLength = (endPos.y - startPos.y) / 30;
        rb.AddForce(x_swipeLength, 0, y_swipeLength, ForceMode.Impulse);
        Debug.Log("MouseUp");
    }
}
