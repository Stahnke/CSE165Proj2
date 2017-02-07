using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallLauncher : MonoBehaviour {

    public GameObject cannonBallPrefab;
    public AudioClip cannonSound;
    public Transform cannonBallSpanwerTrans;
    public float launchForce = 50000.0f;
    public GameObject eventText;

    public void Launch()
    {
        //Instantiate cannon ball
        GameObject cannonBall = Instantiate(cannonBallPrefab, cannonBallSpanwerTrans.position, Quaternion.identity);
        //Add force
        cannonBall.GetComponent<Rigidbody>().AddForce(transform.forward * launchForce);
        //Play sound effect
        gameObject.GetComponent<AudioSource>().PlayOneShot(cannonSound);

        eventText.GetComponent<EventText>().UpdateText("Cannonball shot!");
    }
}
