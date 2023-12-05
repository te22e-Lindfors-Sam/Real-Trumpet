using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ventil : MonoBehaviour
{
    float time;
    public float pressedTime;
    bool colliding = false;
    public bool pressed = false;
    [SerializeField] SongManager songManager;
    void Update()
    {
        if (colliding)
        {
            time += Time.deltaTime;
        }
        if (pressed)
        {
            pressedTime += Time.deltaTime;
        }
        if (pressedTime > 0.3f && !colliding && pressed)
        {
            songManager.score -= 0.5f;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (pressedTime <= 0.3f)
        {
            pressedTime = 0;
        }
        colliding = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("ecit");
        colliding = false;
        if (time == pressedTime)
        {
            songManager.score += 30;
        }
        else if (time > pressedTime + 0.2f)
        {
            songManager.score += 20;
        }
        else if (time > pressedTime + 0.5f)
        {
            songManager.score += 10;
        }
        time = 0;
    }
}
