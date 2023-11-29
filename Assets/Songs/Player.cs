using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] AudioClip A;
    [SerializeField] AudioClip B;
    [SerializeField] AudioClip C;
    [SerializeField] AudioClip D;
    [SerializeField] AudioClip E;
    [SerializeField] AudioClip F;
    [SerializeField] AudioClip G;

    [SerializeField] GameObject Input1;
    [SerializeField] GameObject Input2;
    [SerializeField] GameObject Input3;

    AudioSource source;
    Ventil ven1;
    Ventil ven2;
    Ventil ven3;
    Image ven1Image;
    Image ven2Image;
    Image ven3Image;

    Note note;
    bool changedNote = false;

    void Start()
    {
        source = GetComponent<AudioSource>();

        ven1 = Input1.GetComponent<Ventil>();
        ven1Image = Input1.GetComponent<Image>();

        ven2 = Input2.GetComponent<Ventil>();
        ven2Image = Input2.GetComponent<Image>();

        ven3 = Input3.GetComponent<Ventil>();
        ven3Image = Input3.GetComponent<Image>();

        note = new();
    }

    void Update()
    {
        //Take the Input and check if it changed from last frame
        changedNote = false;
        if (Input.GetKey(KeyCode.A))
        {
            if (!note.ventil1)
            {
                ven1.pressedTime = 0;
                ven1.pressed = true;
                ven1Image.color = Color.red;
                changedNote = true;
                note.ventil1 = true;
            }
        }
        else
        {
            if (note.ventil1)
            {
                ven1.pressed = false;
                changedNote = true;
                note.ventil1 = false;
                ven1Image.color = Color.white;
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (!note.ventil2)
            {
                ven2.pressedTime = 0;
                ven2.pressed = true;
                ven2Image.color = Color.red;
                changedNote = true;
                note.ventil2 = true;
            }
        }
        else
        {
            if (note.ventil2)
            {
                ven2.pressed = false;
                ven2Image.color = Color.white;
                changedNote = true;
                note.ventil2 = false;
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (!note.ventil3)
            {
                ven3.pressedTime = 0;
                ven3.pressed = true;
                ven3Image.color = Color.red;
                changedNote = true;
                note.ventil3 = true;
            }
        }
        else
        {
            if (note.ventil3)
            {
                ven3.pressed = false;
                ven3Image.color = Color.white;
                changedNote = true;
                note.ventil3 = false;
            }
        }

        //code to play the audio
        AudioClip noteToPlay = DecideNote();
        if (noteToPlay == null)
        {
            note.duration = 0;
            source.Pause();
        }
        if (changedNote)
        {
            source.clip = noteToPlay;
            source.Play();
        }
        else
        {
            note.duration += Time.deltaTime;
        }
    }

    AudioClip DecideNote()
    {
        if (!note.ventil1 && note.ventil2 && note.ventil3)//A
        {
            return A;
        }
        else if (!note.ventil1 && note.ventil2 && !note.ventil3)//B
        {
            return B;
        }
        else if (note.ventil1 && note.ventil2 && note.ventil3)//C
        {
            return C;
        }
        else if (note.ventil1 && !note.ventil2 && note.ventil3)//D
        {
            return D;
        }
        else if (note.ventil1 && note.ventil2 && !note.ventil3)//E
        {
            return E;
        }
        else if (note.ventil1 && !note.ventil2 && !note.ventil3)//F
        {
            return F;
        }
        else if (!note.ventil1 && !note.ventil2 && note.ventil3)//G
        {
            return G;
        }
        return null;
    }

}
