using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public bool[] OnPlay;
	public AudioClip[] clips;
    public GameObject[] sounds;

	void Start () {
		int i = 0;
		foreach (AudioClip s in clips) {
    		sounds[i] = new GameObject();
			sounds[i].AddComponent<AudioSource>();
			sounds[i].GetComponent<AudioSource>().clip = s;
            sounds[i].GetComponent<AudioSource>().enabled = !true;
            //spawn (sounds[i]);
			i+=1;
		}
	}
/*
    void spawn(GameObject gB)
    {
        Instantiate(gB,transform.position,Quaternion.identity);
    }
*/
    // Update is called once per frame
    void Update()
    {
        int i = 0;
     	foreach (bool s in OnPlay) {
            if(s)
            {
                sounds[i].GetComponent<AudioSource>().enabled = true;
            }
            else
            {
                sounds[i].GetComponent<AudioSource>().enabled = !true;
            }
            i+=1;
        }
    }
}