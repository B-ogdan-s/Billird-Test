using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBollPlayer : MonoBehaviour
{
    public ChrckLevel chrck;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Luza")
        {
            chrck.Luser();
            Destroy(gameObject);
        }
    }
}
