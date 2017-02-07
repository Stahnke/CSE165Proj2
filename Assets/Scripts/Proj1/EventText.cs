using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventText : MonoBehaviour {

    public Text eventText;

    private void Start()
    {
        eventText.text = "";
    }

    public void UpdateText(string text)
    {
        eventText.text = text;
        StartCoroutine(resetText());
    }

    IEnumerator resetText()
    {
        yield return new WaitForSeconds(1.75f);
        eventText.text = "";
    }
}
