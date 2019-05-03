using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAudio : MonoBehaviour
{
	public AudioPlayer aP;

    public string[] key = new string[6]{ "a", "s", "d", "f", "g", "h" };

    // Update is called once per frame
    void Update()
    {
        int i = 0;
  	    foreach (string s in key)
        {
            if(Input.GetKeyDown (s))
            {
                if(aP.OnPlay[i])
                {
                    aP.OnPlay[i]=false;
                }
                else
                {
                    aP.OnPlay[i]=true;
                }
            }
            i+=1;
        }
    }
}