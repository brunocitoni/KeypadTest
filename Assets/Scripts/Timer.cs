using System;
using TMPro;
using UnityEngine;

//Implements a countdown timer/
public class Timer : MonoBehaviour
{
    public Action TimerElapsed;

    public TMP_Text timerText;
    public float timeDuration;
    public bool isCounting;
    private float timer;

    private void Update()
    {
        if (isCounting)
        {
            if (timer > 0)
            {
                // clamp at 0
                timer = Mathf.Max(timer - Time.deltaTime, 0);

                // if a text element has been given for this timer to be printed
                if (timerText != null)
                    FormatAndDisplayTimer();
            }
            else
            {
                // stop timer and invoke callback
                StopTimer();
                OnTimerFinish();
            }
        }
    }

    /// <summary>
    /// Set timer (timer must be stopped first)
    /// </summary>
    /// <param name="duration"></param>
    public void SetDuration(float duration)
    {
        if (!isCounting)
        {
            timeDuration = duration;
        }
    }

    public void ResumeTimer()
    {
        isCounting = true;
    }

    public void RestartTimer()
    {
        timer = timeDuration;
        isCounting = true;
    }

    public void StopTimer()
    {
        isCounting = false;
        timer = 0;
    }

    public void PauseTimer()
    {
        isCounting = false;
    }

    public float GetTimeLeft()
    {
        return timer;
    }

    public void OnTimerFinish()
    {
        TimerElapsed?.Invoke();
    }

    /// <summary>
    /// Format the time left on the timer to be displayed
    /// </summary>
    public void FormatAndDisplayTimer()
    {
        // Convert timer to TimeSpan and format
        var timeSpan = TimeSpan.FromMilliseconds(timer*1000);
        string formattedTime = string.Format("{0:D1}:{1:D3}", timeSpan.Seconds, timeSpan.Milliseconds);

        // Update the timer text
        timerText.text = formattedTime;
    }
}