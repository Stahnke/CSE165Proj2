using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {

    public GameObject cannonBallLauncher;
    public GameObject laserLauncher;
    public GameObject rayCaster;
    public GameObject BrickSpawner;
    public GameObject gazeSlider;
    public Text modeText;

    private int mode = 0;

    //Consts
    private const int NORMAL_MODE = 0;
    private const int LASER_MODE = 1;
    private const int CANNON_MODE = 2;

    private bool eventOccurred;
    private bool switched;

    private IEnumerator dwellRoutine;

    //init
    private void Start()
    {
        eventOccurred = false;
        SwitchMode(NORMAL_MODE);

        dwellRoutine = Dwell();
        StartCoroutine(dwellRoutine);
    }

    //Launch something
    void Launch()
    {
        //print("Launch!");
        //Gaze or normal mode
        eventOccurred = rayCaster.GetComponent<RayCaster>().Launch();

        if (eventOccurred == false)
        {
            //Laser mode
            if (mode == LASER_MODE)
            {
                laserLauncher.GetComponent<Laser>().Launch();
            }

            //Cannon mode
            else if (mode == CANNON_MODE)
            {
                cannonBallLauncher.GetComponent<CannonBallLauncher>().Launch();
            }
        }

        else eventOccurred = false;
    }

    //switch modes
    public void SwitchMode(int mode)
    {
        this.mode = mode;

        //Update the mode settings
        if (this.mode == NORMAL_MODE)
        {
            modeText.text = "Normal Mode";
            modeText.color = new Color(0.0f, 0.5f, 1.0f);
            laserLauncher.SetActive(false);
        }
            
        else if (this.mode == LASER_MODE)
        {
            modeText.text = "Laser Mode";
            modeText.color = new Color(0.0f, 0.6f, 0.4f);
            laserLauncher.SetActive(true);
        }
            
        else if (this.mode == CANNON_MODE)
        {
            modeText.text = "Cannon Mode";
            modeText.color = new Color(1.0f, 0.0f, 0.0f);
            laserLauncher.SetActive(false);
        } 
    }

    public void resetScene()
    {
        Destroy(GameObject.Find("BrickParent(Clone)"));
        BrickSpawner.GetComponent<BrickSpawner>().SpawnBricks();
        print("reset");
    }
    
    //dwell coroutine
    IEnumerator Dwell()
    {
        Quaternion startRot = gameObject.transform.rotation;
        const float threshhold = 0.05f;
        const float waitTime = 0.1f;
        const float dwellTime = 2.0f;
        float curTime = 0.0f;

        while (true)
        {
            
            //update the time
            yield return new WaitForSeconds(waitTime);
            curTime += waitTime;
            gazeSlider.GetComponent<GazeSlider>().UpdateSlider(curTime / 2.0f);

            //didn't break threshhold since last update
            if (Mathf.Abs(startRot.x - gameObject.transform.rotation.x) <= threshhold
                && Mathf.Abs(startRot.y - gameObject.transform.rotation.y) <= threshhold
                && Mathf.Abs(startRot.z - gameObject.transform.rotation.z) <= threshhold)
            {
                //if we didn't break threshhold for the total dwell time, active dwell
                if (curTime >= dwellTime)
                {
                    //launch and reset
                    //print("RESET: DWELL");
                    Launch();
                    curTime = 0.0f;
                    startRot = gameObject.transform.rotation;
                }
            }

            //broke threshhold
            else
            {
                //reset
                //print("RESET: BROKE THRESHHOLD");
                curTime = 0.0f;
                startRot = gameObject.transform.rotation;
            }
        }
    }
}
