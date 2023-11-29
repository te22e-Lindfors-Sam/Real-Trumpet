using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[CreateAssetMenu(fileName = "Song", menuName = "Song/Song", order = 1)]
public class Song : ScriptableObject
{
    public Sprite backdrop;
    public List<Note> song;
}

[System.Serializable]
public class Note
{
    public bool ventil1;
    public bool ventil2;
    public bool ventil3;

    public bool low;
    public bool heigh;
    public bool hastag;

    public float duration = 1;

    public Note()
    {

    }
}
