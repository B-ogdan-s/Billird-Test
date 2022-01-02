using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Touches : MonoBehaviour
{
    public DrawLine draw;
    public TraficBoll traf;
    public Vector2 startTouch;
    public GameObject boll, sliderOBJ, navigation_2, stop, ghost;
    public LineRenderer line, line2, line3;
    public Slider slider;
    public float startRadius, endRadius;
    public Vector2 oldPos, newPos;
    public int time = 0;

    public GameObject obj;

    private void Update()
    {
        //Physics2D.Simulate(Time.fixedTime);
        oldPos = newPos;

        newPos = boll.transform.localPosition;

        if (oldPos == newPos)
        {
            stop.transform.localPosition = new Vector3(-1380f * Screen.width / 2960f, 1100f * Screen.height / 1440f, -1f);
            if (Input.touchCount == 1 && time >= 3)
            {
                TouchInut(Input.touches[0]);
            }
            time++;
        }
        else
        {
            stop.transform.localPosition = new Vector3(-1380f * Screen.width / 2960f, 600f * Screen.height / 1440f, -1f);
            time = 0;
        }

        slider.value = endRadius / traf.maxRad;
    }

    public void TouchInut(Touch touch)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //проверка на касание
        if (touch.phase == TouchPhase.Began)
        {
            obj = Instantiate(ghost) as GameObject;
            if (Physics.Raycast(ray, out hit))
            {
                startTouch = new Vector2(hit.point.x, hit.point.y);

                startRadius = Mathf.Sqrt(Mathf.Pow(startTouch.x, 2f) + Mathf.Pow(startTouch.y, 2f));
                endRadius = startRadius;
            }
            obj.transform.localPosition = new Vector3(hit.point.x, hit.point.y, -1);

            line.enabled = true;
            line2.enabled = true;
            line3.enabled = true;
            draw.Draw();

            navigation_2.transform.localPosition = boll.transform.localPosition;

            if (startTouch.x<=0)
            {
                sliderOBJ.transform.localPosition = new Vector2(1353f * Screen.width / 2960, -130f * Screen.height / 1440);
            }
            else if(startTouch.x > 0)
            {
                sliderOBJ.transform.localPosition = new Vector2(-1353f * Screen.width / 2960, -130f * Screen.height / 1440);
            }
            line3.SetPosition(0, new Vector3(hit.point.x, hit.point.y, -1));
        }

        // проверка на удержание
        if(touch.phase == TouchPhase.Moved)
        {
            if (Physics.Raycast(ray, out hit))
            {
                //startTouch = new Vector2(hit.point.x, hit.point.y);
                endRadius = Mathf.Sqrt(Mathf.Pow(hit.point.x - startTouch.x, 2f) + Mathf.Pow(hit.point.y - startTouch.y, 2f));
            }
            line3.SetPosition(1, new Vector3(hit.point.x, hit.point.y, -1));
            draw.Draw();

            navigation_2.transform.localPosition = new Vector3(boll.transform.localPosition.x + hit.point.x - startTouch.x, boll.transform.localPosition.y + hit.point.y - startTouch.y, boll.transform.localPosition.z);
        }

        if (touch.phase == TouchPhase.Ended)
        {
            line.enabled = false;
            line2.enabled = false;
            line3.enabled = false;
            if (0.3f < endRadius)
                traf.Trafic(startRadius, endRadius);

            sliderOBJ.transform.localPosition = new Vector2(0f, 2000f * Screen.height/1440);
            slider.value = 0f;
            Destroy(obj);
            draw.DesGhost();
        }
    }
}
