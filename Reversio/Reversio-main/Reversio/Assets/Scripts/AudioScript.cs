using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public static AudioScript Instance;
    private AudioSource audioSource;
    public AudioClip[] audioClips;
    
    void Awake()
    {
        if(Instance==null)
        {
            Instance=this;
           
        }
        else
            {Destroy(gameObject);}
        audioSource = GetComponent<AudioSource>();
    }
    
    public void checkBGM()
    {
        if(audioSource.isPlaying==false)
        {audioSource.Play();}
    }
    
    public void play(int index,bool bgmEnds=false)
    {
        if(bgmEnds)
            {audioSource.Stop();}
        audioSource.PlayOneShot(audioClips[index]);
    }

    public IEnumerator PlayDelayed(float wait, int index, bool bgmEnds = false)
    {
        yield return new WaitForSeconds(wait);
        play(index, bgmEnds);
    }
   
}

