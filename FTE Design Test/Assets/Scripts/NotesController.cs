using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesController : MonoBehaviour
{
    public int goodNoteCounter = 0;
    public FullNotes.Notes[] noteSeries;
    private bool puzzleFinished = false;

    public StartCutsceneScript startCutscene;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    public void PressNote(int notePlayed)
    {
        if (!puzzleFinished)
        {
            if (notePlayed == (int)noteSeries[goodNoteCounter])
            {
                goodNoteCounter++;
            }
            else
            {
                goodNoteCounter = 0;
            }
            if (goodNoteCounter == 4)
            {
                puzzleFinished = true;
                startCutscene.StartCutscene();
            }
        }

    }
}
