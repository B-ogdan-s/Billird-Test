using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public Touches toucheScript;
    public TraficBoll traf;
    public Navigation navigat;
    public GameObject boll, ghost, ghost_2, navigation, boll_clon;
    public LineRenderer line, line2;
    public float rad, rad_1, x_boll_player, y_boll_player, v_boll_2, v_boll_player_2;
    public int layerMask = ~(1 << 6);
    public int layerMask_2 = ~(1 << 7);
    public bool checkGhost = true;

    public void Draw()
    {

        navigat.Navigat();
        RaycastHit2D hit = Physics2D.Raycast(boll.transform.localPosition, navigation.transform.TransformDirection(-Vector3.forward), Mathf.Infinity, layerMask);

        line.SetPosition(0, new Vector3(boll.transform.localPosition.x, boll.transform.localPosition.y, -1));
        line.SetPosition(1, new Vector3(hit.point.x, hit.point.y, -1));

        if (hit.collider.tag == "Boll")
        {
            rad_1 = Mathf.Sqrt(Mathf.Pow(hit.transform.position.x - hit.point.x, 2f) + Mathf.Pow(hit.transform.position.y - hit.point.y, 2f));

            PhysicsBoll(hit.point, hit.transform.position);

            line2.SetPosition(0, new Vector3(hit.transform.position.x, hit.transform.position.y, -1));

            line2.SetPosition(1, new Vector3(v_boll_2/7f * ((hit.transform.position.x - hit.point.x) / rad_1) + hit.transform.position.x,
                 v_boll_2/7f * ((hit.transform.position.y - hit.point.y) / rad_1) + hit.transform.position.y, -1));

            line.SetPosition(2, new Vector3(x_boll_player, y_boll_player, -1));

            if (checkGhost == true)
            {
                ghost_2 = Instantiate(ghost) as GameObject;
                checkGhost = false;
            }
            ghost_2.transform.localPosition = new Vector3(hit.point.x, hit.point.y, -1);
        }
        else if(hit.collider.tag == "Wall")
        {
            line2.SetPosition(0, new Vector3(0f, 0f, 0f));
            line2.SetPosition(1, new Vector3(0f, 0f, 0f));

            PhysicsWall(hit.point, hit.normal);

            line.SetPosition(2, new Vector3(x_boll_player, y_boll_player, -1));

            if (checkGhost == true)
            {
                ghost_2 = Instantiate(ghost) as GameObject;
                checkGhost = false;
            }
            ghost_2.transform.localPosition = new Vector3(hit.point.x, hit.point.y, -1);
        }
        else
        {
            line2.SetPosition(0, new Vector3(0f, 0f, 0f));
            line2.SetPosition(1, new Vector3(0f, 0f, 0f));
            line.SetPosition(2, new Vector3(hit.point.x, hit.point.y, -1));

            DesGhost();
        }
    }

    public void DesGhost()
    {
        if(checkGhost == false)
        {
            Destroy(ghost_2);
            checkGhost = true;
        }
    }

    public void PhysicsWall(Vector3 b, Vector2 c)
    {
        var ab = boll.transform.localPosition - (boll.transform.localPosition + new Vector3(1f, 0f, 0f));
        var bc = boll.transform.localPosition - b;
        float l1 = Vector3.Angle(ab,bc);

        float v_boll_player_start = 6.2f * (toucheScript.endRadius / traf.maxRad) / 0.282f;

        v_boll_2 = Mathf.Abs(Mathf.Cos(l1 / 180f * Mathf.PI) * v_boll_player_start);
        v_boll_player_2 = Mathf.Abs(Mathf.Sin(l1 / 180f * Mathf.PI) * v_boll_player_start);

        Debug.Log(c);

        var r = Vector2.Reflect(new Vector2(v_boll_2, v_boll_player_2), c);

        if (b.x < boll.transform.localPosition.x)
        {
            r.x *= -1f;
            
        }
        if (b.y < boll.transform.localPosition.y)
        {
            if (c.x != 0f && c.y <= 0.9f)
            {
                r.x *= -1f;
                r.y *= -1f;
            }
            r.y *= -1f;
        }

        x_boll_player = r.x/10 + b.x;
        y_boll_player = r.y/10 + b.y;
    }

    public void PhysicsBoll(Vector3 b, Vector3 c)
    {
        var ab = b - new Vector3(b.x + 1f, b.y, b.z);
        var bc = b - c;

        float l =  Vector3.Angle(ab, bc);
        float l1 = Vector3.Angle(b - boll.transform.localPosition, bc);
        float f1;

        float f2 = Vector3.Angle(boll.transform.position - new Vector3(boll.transform.position.x, boll.transform.position.y + 1f, boll.transform.position.z), boll.transform.position - b);
        float l2 = Vector3.Angle(boll.transform.position - new Vector3(boll.transform.position.x + 1f, boll.transform.position.y, boll.transform.position.z), boll.transform.position - b);
        float l3 = Vector3.Angle(boll.transform.position - new Vector3(boll.transform.position.x + 1f, boll.transform.position.y, boll.transform.position.z), boll.transform.position - c);


        float v_boll_player_start = 6.2f * (toucheScript.endRadius / traf.maxRad) / 0.282f;

        v_boll_2 = Mathf.Abs(Mathf.Cos(l1 / 180f * Mathf.PI) * v_boll_player_start);
        v_boll_player_2 = Mathf.Abs(Mathf.Sin(l1 / 180f * Mathf.PI) * v_boll_player_start);

        f1 = Mathf.Acos((v_boll_player_start * Mathf.Cos((l2) / 180f * Mathf.PI) - v_boll_2 * Mathf.Cos(l / 180f * Mathf.PI)) / v_boll_player_2);

        if (l2 <= 90f)
        {
            if (f2 <= 90f)
            {
                if (l2 >= l3)
                {

                }
                else
                {
                    f1 *= -1f;
                }
            }
            else
            {
                if (l2 >= l3)
                {
                    f1 *= -1f;
                }
                else
                {

                }
            }
        }
        else
        {
            if (f2 <= 90f)
            {
                if (l2 >= l3)
                { 

                }
                else
                {
                    f1 *= -1f;
                }
            }
            else
            {
                if (l2 >= l3)
                {
                    f1 *= -1f;
                }
                else
                {

                }
            }
        }
        if(l > 90f)
        {
            f1 *= -1f;
        }

        x_boll_player = v_boll_player_2 * Mathf.Cos(f1) / 10f + b.x;
        y_boll_player = v_boll_player_2 * Mathf.Sin(f1) / 10f + b.y;

        if (Mathf.Abs(v_boll_player_2) < 0.05f)
        {
            x_boll_player = b.x;
            y_boll_player = b.y;
        }
    }
}
