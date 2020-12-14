using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMusic : MonoBehaviour
{   

    public FullNotes.Notes note;
    // Start is called before the first frame update
    private AudioSource myAudioSource;
    private NotesController notesController;

    void Start()
    {
        notesController = GameObject.Find("NotesController").GetComponent<NotesController>();
        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = SoundsLibrary.library.pianoSounds[(int)note];
    }
    
    public void PlaySound()
    {
        myAudioSource.Play();
        notesController.PressNote((int)note);
    }
}
