using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Text timeLeftCount;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowTimeLeftCount(int timeLeft)
    {
        timeLeftCount.text = timeLeft.ToString();
    }

}
