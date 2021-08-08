using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    private Text _textTimer;
    
    private static float _seconds;
    private static int _minutes;

    private void Start()
    {
        _textTimer = this.gameObject.GetComponent<Text>();
    }

    void Update()
    {
        SetTimer();
    }

    private void SetTimer()
    {
        _seconds += Time.deltaTime;
        if (_seconds >= 60)
        {
            _seconds = 0;
            _minutes++;
        }

        PrintTime();
    }

    private void PrintTime()
    {
        if (_minutes < 10)
        {
            if (_seconds < 10)
            {
                _textTimer.text = "0" + _minutes.ToString() + " : " + "0" + ((int)_seconds).ToString();
            }
            else
            {
                _textTimer.text = "0" + _minutes.ToString() + " : " + ((int)_seconds).ToString();
            }
        }
        else
        {
            if (_seconds < 10)
            {
                _textTimer.text = _minutes.ToString() + " : " + "0" + ((int)_seconds).ToString();
            }
            else
            {
                _textTimer.text = _minutes.ToString() + " : " + ((int)_seconds).ToString();
            }
        }
    }

    public static void ReduceTime(float delta)
    {
        if (_seconds > delta)
        {
            _seconds -= delta;
        }
        else
        {
            if (_minutes == 0)
            {
                _seconds = 0;
            }

            if (_minutes > 0)
            {
                _minutes--;
                float secondsInMinets = 60;
                _seconds = secondsInMinets - (delta - _seconds);
            }

        }
    }


}
