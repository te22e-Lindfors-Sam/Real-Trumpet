using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SongManager : MonoBehaviour
{
    [SerializeField] Image backdrop;
    [SerializeField] Song song;
    [SerializeField] Canvas canvas;

    [SerializeField] Transform line1;
    [SerializeField] Transform line2;
    [SerializeField] Transform line3;

    [SerializeField] TextMeshProUGUI scoreShower;
    [SerializeField] TextMeshProUGUI finalScore;
    [SerializeField] GameObject done;


    List<GameObject> notes;
    public float score = 0;

    public float songDuration = 10;
    public float timer = 0;



    [SerializeField] GameObject noteObjet;

    void Start()
    {
        Application.targetFrameRate = 60;
        backdrop.sprite = song.backdrop;
        notes = new List<GameObject>();
        for (int i = 0; i < song.song.Count; i++)
        {
            noteObjet.transform.localScale = new Vector3(song.song[i].duration, 1, 1);
            noteObjet.GetComponent<BoxCollider2D>().size = new Vector2(song.song[i].duration * 50, 100);
            if (song.song[i].ventil1)
            {
                notes.Add(Instantiate(noteObjet, line1.position + new Vector3(i * 60 + song.song[i].duration * 30, 0, 0), Quaternion.identity, canvas.transform));
            }
            if (song.song[i].ventil2)
            {
                notes.Add(Instantiate(noteObjet, line2.position + new Vector3(i * 60 + song.song[i].duration * 30, 0, 0), Quaternion.identity, canvas.transform));
            }
            if (song.song[i].ventil3)
            {
                notes.Add(Instantiate(noteObjet, line3.position + new Vector3(i * 60 + song.song[i].duration * 30, 0, 0), Quaternion.identity, canvas.transform));
            }
            songDuration += song.song[i].duration;
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

        scoreShower.text = score.ToString();
        timer += Time.deltaTime;
        if (timer > songDuration)
        {
            done.SetActive(true);
            finalScore.text = score.ToString();
            Time.timeScale = 0;
        }

    }


}
