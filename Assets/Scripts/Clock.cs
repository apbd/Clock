using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    // Pointers
    public GameObject secondPointer;
    public GameObject minutePointer;
    public GameObject hourPointer;

    // Degrees for analog clock
    float secondDegree;
    float minuteDegree;
    float hourDegree;

    public TextMeshProUGUI digitalClock;

    // Custom time and other extra features
    public string inputTime;
    public InputField customTimeInputField;
    public Toggle stopButton;
    public GameObject error;
    
    public bool clockIsOn;
    public bool millisecondOptionOn;
    public string millisecondsDigital;


    void Start()
    {
        secondPointer = GameObject.Find("SecondPointer");
        minutePointer = GameObject.Find("MinutePointer");
        hourPointer = GameObject.Find("HourPointer");
        digitalClock = GameObject.Find("Clock/DigitalClockCanvas/DigitalClockText").GetComponent<TextMeshProUGUI>();

        customTimeInputField = GameObject.Find("Canvas/InputField").GetComponent<InputField>();
        error = GameObject.Find("Canvas/Error");
        error.SetActive(false);

    }

    void Update()
    {
        // Computer time now
        DateTime time = DateTime.Now;
        

        // Is the clock on
        if (clockIsOn)
        {
            // Change each unit of time to degree for analog clock 
            // Add previous unit of time to soften pointer movement
            secondDegree = (float)((time.Second + time.Millisecond / 1000f) / (60f)) * -360f;
            secondPointer.transform.eulerAngles = new Vector3(0, 0, secondDegree);                          // Moves Pointers

            minuteDegree = (float)((time.Minute + time.Second / 60f) / (60f)) * -360f;
            minutePointer.transform.eulerAngles = new Vector3(0, 0, minuteDegree);

            hourDegree = (float)((time.Hour + time.Minute / 60f) / (12f)) * -360f;
            hourPointer.transform.eulerAngles = new Vector3(0, 0, hourDegree);

            // Add zero to digital clock when unit of time is single digit
            string secondsDigitalClock = Mathf.RoundToInt(time.Second).ToString("00");
            string minutesDigitalClock = Mathf.RoundToInt(time.Minute).ToString("00");
            string hoursDigitalClock = Mathf.RoundToInt(time.Hour).ToString("00");

            // Add Milliseconds
            if (millisecondOptionOn == true)
            {
                millisecondsDigital = ":" + Mathf.RoundToInt(time.Millisecond).ToString("000");
            }
            else
            {
                millisecondsDigital= "";
            }

            // Puts time to digital clock text field
            digitalClock.text = hoursDigitalClock + ":" + minutesDigitalClock + ":" + secondsDigitalClock + millisecondsDigital;

        }
    }

    // Milliseconds toggle
    public void MillisecondToggle(bool toggle)
    {
        millisecondOptionOn = toggle;
    }

    // Custom time
    public void CustomTime()
    {
        // Shutsdown clock
        clockIsOn = false;
        stopButton.isOn = true;

        // Takes time from inputfield
        inputTime = customTimeInputField.text;

        try
        {
            // Separates strings and puts them in a list
            String[] separatedTime = inputTime.Split(':');

            // Changes input to float as well as unit of times to degrees for analog
            secondDegree = (float.Parse(separatedTime[2]) / 60f) * -360f;
            minuteDegree = ((float.Parse(separatedTime[1]) + (float.Parse(separatedTime[2]) / 60f)) / 60f) * -360f;
            hourDegree = ((float.Parse(separatedTime[0]) + (float.Parse(separatedTime[1]) / 60f)) / 12f) * -360f;

            // Sets rotations
            secondPointer.transform.eulerAngles = new Vector3(0, 0, secondDegree);
            minutePointer.transform.eulerAngles = new Vector3(0, 0, minuteDegree);
            hourPointer.transform.eulerAngles = new Vector3(0, 0, hourDegree);

            // Custom time for digital clock
            string secondDigital = Mathf.Floor(float.Parse(separatedTime[2])).ToString("00");
            string minutesDigital = Mathf.Floor(float.Parse(separatedTime[1])).ToString("00");
            string hoursDigital = Mathf.Floor(float.Parse(separatedTime[0])).ToString("00");

            digitalClock.text = hoursDigital + ":" + minutesDigital + ":" + secondDigital;

        }
        catch
        {
            // Red error text
            Debug.Log("input error");
            error.SetActive(true);
        }
      
    }

    // Stop clock toggle
    public void StopClock(bool toggle)
    {
        error.SetActive(false);
        clockIsOn = !toggle;
    }
}
