using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton containing all the sounds in the level
/// </summary>
public class SoundsLibrary : MonoBehaviour
{
    public static SoundsLibrary library;
    public AudioClip[] pianoSounds;
	// Start is called before the first frame update
	void Awake()
	{
		if (library == null)
		{
			DontDestroyOnLoad(gameObject);
			library = this;
		}
		else if (library != this)
		{
			Destroy(gameObject);
		}
	}
}
