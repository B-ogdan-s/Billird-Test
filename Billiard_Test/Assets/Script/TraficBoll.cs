using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraficBoll : MonoBehaviour
{
    public GameObject navigation;
    public float force, maxRad;

    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Trafic(float startRad, float finRad)
    {
        if (navigation.transform.rotation.eulerAngles.y < 100)
            transform.rotation = Quaternion.Euler(0f, 0f, -navigation.transform.rotation.eulerAngles.x);
        else if(navigation.transform.rotation.eulerAngles.y >250)
            transform.rotation = Quaternion.Euler(0f, 0f, navigation.transform.rotation.eulerAngles.x + 180f);

        if (finRad > maxRad)
            finRad = maxRad;

        force = 6.2f * (finRad / maxRad);

        rb.AddForce(-transform.right * force, ForceMode2D.Impulse);
    }
}
