using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StartCutsceneScript : MonoBehaviour
{
    private PlayableDirector timeline;
    private bool played = false;

    // Start is called before the first frame update
    void Start()
    {
        timeline = GetComponent<PlayableDirector>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !played)
        {
            StartCutscene();
        }
    }

    public void StartCutscene()
    {
        timeline.Play();
        played = true;
    }

    public void Reset()
    {
        Debug.Log(name);
        played = false;
    }

}
