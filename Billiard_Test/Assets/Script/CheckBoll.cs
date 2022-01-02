using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBoll : MonoBehaviour
{
    public GameObject nav;
    public ChrckLevel chrck;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Luza")
        {
            chrck.Check();
            Destroy(nav);
            Destroy(gameObject);
        }
    }
}
