using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongPlayer : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] Song song;

    [SerializeField] AudioClip A;
    [SerializeField] AudioClip B;
    [SerializeField] AudioClip C;
    [SerializeField] AudioClip D;
    [SerializeField] AudioClip E;
    [SerializeField] AudioClip F;
    [SerializeField] AudioClip G;



    void Start()
    {
        StartCoroutine("PlaySong");
    }
    IEnumerator PlaySong()
    {
        for (int i = 0; i < song.song.Count; i++)
        {
            AudioClip note = DecideNote(song.song[i]);
            source.clip = note;
            source.Play();
            yield return new WaitForSeconds(1.0f + song.song[i].lenght);
        }
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
        else if (!note.ventil1 && !note.ventil2 && !note.ventil3)//C
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
