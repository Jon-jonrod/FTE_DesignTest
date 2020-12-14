using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesController : MonoBehaviour
{
    public int goodNoteCounter = 0;
    public FullNotes.Notes[] noteSeries;

    private ButtonOpenDoor openDoorScript;

    
    // Start is called before the first frame update
    void Start()
    {
        openDoorScript = GetComponent<ButtonOpenDoor>();
    }

    public void PressNote(int notePlayed)
    {
        if (notePlayed == (int)noteSeries[goodNoteCounter])
        {
            goodNoteCounter++;
        } else
        {
            goodNoteCounter = 0;
        }
        if (goodNoteCounter == 4)
        {
            openDoorScript.OpenDoor();
        }


    }
}
