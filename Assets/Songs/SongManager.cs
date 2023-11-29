using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] Song song;
    [SerializeField] Canvas canvas;

    [SerializeField] Transform line1;
    [SerializeField] Transform line2;
    [SerializeField] Transform line3;

    [SerializeField] AudioClip A;
    [SerializeField] AudioClip B;
    [SerializeField] AudioClip C;
    [SerializeField] AudioClip D;
    [SerializeField] AudioClip E;
    [SerializeField] AudioClip F;
    [SerializeField] AudioClip G;

    List<GameObject> notes;
    public Note note;
    bool changedNote;
    AudioClip currentClip;

    [SerializeField] GameObject noteObjet;

    void Start()
    {
        note = new();
        Application.targetFrameRate = 60;
        notes = new List<GameObject>();
        StartCoroutine("PlaySong");
    }
    IEnumerator PlaySong()
    {
        for (int i = 0; i < song.song.Count; i++)
        {
            //AudioClip note = DecideNote(song.song[i]);
            //source.clip = note;
            //source.Play();
            if (song.song[i].ventil1)
            {
                notes.Add(Instantiate(noteObjet, line1.position, quaternion.identity, canvas.transform));
            }
            if (song.song[i].ventil2)
            {
                notes.Add(Instantiate(noteObjet, line2.position, quaternion.identity, canvas.transform));
            }
            if (song.song[i].ventil3)
            {
                notes.Add(Instantiate(noteObjet, line3.position, quaternion.identity, canvas.transform));
            }
            yield return new WaitForSeconds(1.0f + song.song[i].lenght);


        }
    }

    void Update()
    {
        for (int i = notes.Count - 1; i >= 0; i--)
        {
            Debug.Log("itterating");
            if (notes[i].transform.position.x < 0)
            {
                Debug.Log("HEJ");
                Destroy(notes[i]);
                notes.Remove(notes[i]);
                continue;
            }
            notes[i].transform.position -= new Vector3(1, 0, 0);
        }


        changedNote = false;
        if (Input.GetKey(KeyCode.A))
        {
            if (!note.ventil1)
            {
                changedNote = true;
                note.ventil1 = true;
            }
        }
        else
        {
            if (note.ventil1)
            {
                changedNote = true;
                note.ventil1 = false;
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (!note.ventil2)
            {
                changedNote = true;
                note.ventil2 = true;
            }
        }
        else
        {
            if (note.ventil2)
            {
                changedNote = true;
                note.ventil2 = false;
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (!note.ventil3)
            {
                changedNote = true;
                note.ventil3 = true;
            }
        }
        else
        {
            if (note.ventil3)
            {
                changedNote = true;
                note.ventil3 = false;
            }
        }

        AudioClip noteToPlay = DecideNote();
        if (noteToPlay == null)
        {
            currentClip = null;
            source.Pause();
        }
        if (changedNote)
        {
            source.clip = noteToPlay;
            source.Play();
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
