using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
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

    int currentNote = 0;
    public Note note;
    bool changedNote;

    public float score = 0;

    public float timer = -2;

    [SerializeField] GameObject noteObjet;

    void Start()
    {
        Application.targetFrameRate = 60;
        notes = new List<GameObject>();
        note = new();
        for (int i = 0; i < song.song.Count; i++)
        {
            noteObjet.transform.localScale = new Vector3(song.song[i].duration, 1, 1);
            if (song.song[i].ventil1)
            {
                notes.Add(Instantiate(noteObjet, new Vector3(song.song[i].whenToPlay * 60f + 300, line1.position.y, 0), quaternion.identity, canvas.transform));
            }
            if (song.song[i].ventil2)
            {
                notes.Add(Instantiate(noteObjet, new Vector3(song.song[i].whenToPlay * 60f + 300, line2.position.y, 0), quaternion.identity, canvas.transform));
            }
            if (song.song[i].ventil3)
            {
                notes.Add(Instantiate(noteObjet, new Vector3(song.song[i].whenToPlay * 60f + 300, line3.position.y, 0), quaternion.identity, canvas.transform));
            }
        }
        StartCoroutine("PlaySong");
    }
    IEnumerator PlaySong()
    {
        for (int i = 0; i < song.song.Count; i++)
        {
            AudioClip note = DecideNote(song.song[i]);
            source.clip = note;
            source.Play();
            yield return new WaitForSeconds(song.song[i].duration);


        }
    }

    void Update()
    {
        for (int i = notes.Count - 1; i >= 0; i--)
        {
            if (notes[i].transform.position.x < 0)
            {
                Destroy(notes[i]);
                notes.Remove(notes[i]);
                continue;
            }
            notes[i].transform.position -= new Vector3(1, 0, 0);
        }

        timer += Time.deltaTime;










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
            note.duration = 0;
            source.Pause();
        }
        if (changedNote)
        {
            note.whenToPlay = timer;
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
    AudioClip DecideNote(Note note)
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
