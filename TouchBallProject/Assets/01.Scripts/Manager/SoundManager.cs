using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    public float MasterVoulme => masterVoulme;
    public float BGMVolume => bgmVolume * MasterVoulme;
    public float FxVoulme => fxVoulme * MasterVoulme;

    public float masterVoulme;
    public float bgmVolume;
    public float fxVoulme;

    private AudioSource bgmAudioSourece;
    private List<AudioSource> fxAudioSourceList = new List<AudioSource>();

    private Dictionary<string, AudioClip> bgmSoundDic = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> fxSoundDic = new Dictionary<string, AudioClip>();



    private void Awake()
    {
        foreach (var audioClip in Resources.LoadAll<AudioClip>("Sound/BGM"))
        {
            bgmSoundDic.Add(audioClip.name, audioClip);
        }
    }

    private AudioClip GetBGMSound(string name)
    {
        AudioClip result;
        if (!bgmSoundDic.TryGetValue(name, out result))
        {
            Debug.LogWarning(name + "Not Found");
        }
        return result;
    }

    private AudioClip GetFxSound(string name)
    {
        AudioClip result;
        if (!fxSoundDic.TryGetValue(name, out result))
        {
            result = Resources.Load<AudioClip>("Sound/Fx/" + name);
            if (result == null)
            {
                Debug.LogWarning(name + "Not Found");
                return null;
            }
            fxSoundDic.Add(name, result);
        }
        return result;
    }

    private AudioSource MakeAudioSourceObject(string name)
    {
        GameObject audioObject = new GameObject();
        audioObject.name = name;
        audioObject.transform.SetParent(gameObject.transform);

        return audioObject.AddComponent<AudioSource>();
    }

    private void SetAudioSource(AudioSource audioSource, AudioClip audioClip, bool isLoop, float volume, bool isMute = false)
    {
        audioSource.clip = audioClip;
        audioSource.loop = isLoop;
        audioSource.volume = volume;
        audioSource.mute = isMute;
    }

    public void AdjustMasterVolume(float newVolume)
    {
        masterVoulme = newVolume;
        AdjustBGMVolume(bgmVolume);
        AdjustFxVoulme(fxVoulme);
    }

    public void AdjustBGMVolume(float newVolume)
    {
        bgmVolume = newVolume;
        if (bgmAudioSourece != null)
        {
            bgmAudioSourece.volume = BGMVolume;
        }
    }

    public void AdjustFxVoulme(float newVolume)
    {
        fxVoulme = newVolume;
        foreach (var fxAudioSource in fxAudioSourceList)
        {
            if (fxAudioSource != null)
            {
                fxAudioSource.volume = FxVoulme;
            }
        }
    }

    public void PlayBGMSound(string name)
    {
        if (bgmAudioSourece == null)
        {
            bgmAudioSourece = MakeAudioSourceObject("BGMObject");
        }

        SetAudioSource(bgmAudioSourece, GetBGMSound(name), true, BGMVolume, false);
        bgmAudioSourece.Play();
    }

    public void PlayFXSound(string name)
    {
        if (SecurityPlayerPrefs.GetBool("Sound", true))
        {

            foreach (var fxAudioSource in fxAudioSourceList)
            {
                if (!fxAudioSource.isPlaying)
                {
                    SetAudioSource(fxAudioSource, GetFxSound(name), false, FxVoulme, false);
                    fxAudioSource.Play();
                    return;
                }
            }

            fxAudioSourceList.Add(MakeAudioSourceObject("FxObject"));
            PlayFXSound(name);

        }
    }

}