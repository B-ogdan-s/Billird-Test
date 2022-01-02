using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    public Touches touchScript;
    public GameObject boll, navigation_2;

    public void Navigat()
    {
        transform.localPosition = boll.transform.localPosition;
        transform.LookAt(navigation_2.transform);
    }
}
