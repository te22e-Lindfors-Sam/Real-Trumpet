using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SongManager : MonoBehaviour
{
    [SerializeField] Song song;

    //used for spawning the notes
    [SerializeField] Transform vault1Line;
    [SerializeField] Transform vault2Line;
    [SerializeField] Transform vault3Line;
    [SerializeField] GameObject noteObject;

    //Showing end screen
    [SerializeField] TextMeshProUGUI endScreenScore;
    [SerializeField] TextMeshProUGUI screenScore;
    [SerializeField] GameObject endScreen;


    //During code usement
    List<GameObject> notes;
    public float score = 0;

    //timer to know when to show endscreen
    public float songDuration = 0;
    public float timer = 0;



    void Start()
    {
        Application.targetFrameRate = 60;

        notes = new List<GameObject>();
        for (int i = 0; i < song.song.Count; i++)
        {
            noteObject.transform.localScale = new Vector3(song.song[i].duration, 1, 1);
            if (song.song[i].ventil1)
            {
                notes.Add(Instantiate(noteObject, new Vector3(i+songDuration, vault1Line.position.y, 0), Quaternion.identity));
            }
            if (song.song[i].ventil2)
            {
                notes.Add(Instantiate(noteObject, new Vector3(i+songDuration, vault2Line.position.y, 0), Quaternion.identity));
            }
            if (song.song[i].ventil3)
            {
                notes.Add(Instantiate(noteObject, new Vector3(i+songDuration, vault3Line.position.y, 0), Quaternion.identity));
            }
            songDuration += song.song[i].duration;

        }

    }

    void Update()
    {
        //moves each note one unit. As it is always going to be 60 frames per second it is not frame realient
        for (int i = notes.Count - 1; i >= 0; i--)
        {
            if (notes[i].transform.position.x < -10)
            {
                Destroy(notes[i]);
                notes.Remove(notes[i]);
                continue;
            }
            notes[i].transform.position -= new Vector3(0.05f, 0, 0);
        }

        //Showing screenscore
        screenScore.text = "Score: " + score.ToString();
        if (notes.Count == 0)
        {
            endScreen.SetActive(true);
            endScreenScore.text = score.ToString();
            Time.timeScale = 0;
        }

    }


}
