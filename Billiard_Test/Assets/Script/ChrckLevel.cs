using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChrckLevel : MonoBehaviour
{
    public GameObject exit, restart, pouse, force;
    public Touches touch;
    public Text text;
    public int num;
    public void Check()
    {
        num--;
        if(num<=0)
        {
            text.text = "Победа";
            OpenMenu();
        }
    }

    public void Luser()
    {
        text.text = "Проиuрыш";
        OpenMenu();
    }

    public void OpenMenu()
    {
        text.transform.localPosition -= new Vector3(0f, 1500f * Screen.height / 1400, 0f);
        force.transform.localPosition -= new Vector3(1000f, 0f, 0f);
        pouse.transform.localPosition += new Vector3(1000f, 0f, 0f);
        exit.transform.localPosition -= new Vector3(0f, 1500f * Screen.height / 1400, 0f);
        restart.transform.localPosition -= new Vector3(0f, 1500f * Screen.height / 1400, 0f);
        touch.enabled = false;
    }
}
