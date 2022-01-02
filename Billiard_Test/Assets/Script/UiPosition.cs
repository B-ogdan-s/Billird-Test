using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiPosition : MonoBehaviour
{
    public float posX, posY;

    void Start()
    {
        transform.localPosition = new Vector3(posX * Screen.width / 2960f, posY * Screen.height / 1440f);
        transform.localScale = new Vector3(Screen.width / 2960f, Screen.width / 2960f);
    }
}
