using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClic : MonoBehaviour
{
    public GameObject exit, restart, conti, force;
    public Touches touch;

    private void Start()
    {
        touch = GameObject.Find("Main Camera").GetComponent<Touches>();
    }
    public void PauseClic()
    {
        force.transform.localPosition -= new Vector3(1000f, 0f, 0f);
        transform.localPosition += new Vector3(1000f, 0f, 0f);
        exit.transform.localPosition -= new Vector3(0f, 1500f * Screen.height / 1400, 0f);
        restart.transform.localPosition -= new Vector3(0f, 1500f * Screen.height / 1400, 0f);
        conti.transform.localPosition -= new Vector3(0f, 1500f * Screen.height / 1400, 0f);
        touch.enabled = false;
    }

    public void RestartClic()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GamePole");
    }

    public void ExirClic()
    {
        Application.Quit();
    }

    public void ContinueClic()
    {
        transform.localPosition -= new Vector3(1000f, 0f, 0f);
        exit.transform.localPosition += new Vector3(0f, 1500f * Screen.height / 1400, 0f);
        restart.transform.localPosition += new Vector3(0f, 1500f * Screen.height / 1400, 0f);
        conti.transform.localPosition += new Vector3(0f, 1500f * Screen.height / 1400, 0f);
        touch.enabled = true;
    }
}
