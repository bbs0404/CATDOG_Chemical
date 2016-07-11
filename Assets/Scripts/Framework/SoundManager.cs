using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : SingletonBehaviour<SoundManager> {

    [SerializeField]
    private float BGM_Volume = 0.5f;
    [SerializeField]
    private float SFX_Volume = 0.5f;
    [SerializeField]
    private AudioSource[] SFX_List;
    [SerializeField]
    private AudioSource BGM;

    void Awake()
    {
        Inst();
        SFX_List = (AudioSource[])GameObject.FindObjectsOfType(typeof(AudioSource));
        foreach (var item in SFX_List)
        {
            if (item.transform.gameObject.name.Substring(0, 3) == "BGM")
            {
                item.volume = BGM_Volume;
                BGM = item;
                BGM.loop = true;
            }
            else
                item.volume = SFX_Volume;
        }
        if (BGM != null)
        {
            BGM.Play();
        }
    }

    public void playAudio(AudioSource source)
    {
        source.Play();
    }

    public void stopAudio(AudioSource source)
    {
        source.Stop();
    }

    public void changeBGM(AudioClip newBGM)
    {
        BGM.clip = newBGM;
    }

    public void setSFX_Volume(float volume)
    {
        SFX_Volume = volume;
    }

    public void setBGM_Volume(float volume)
    {
        BGM_Volume = volume;
    }
}
