using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioClip backgroundClip;
    public AudioClip fightClip;
    public AudioClip bossClip;
    public AudioClip clickBtnClip;
    public AudioClip equipWeaponClip;
    public AudioClip closeUIClip;

    AudioSource backgroundSource;
    AudioSource fightSource;
    AudioSource bossSource;
    AudioSource clickBtnSource;
    AudioSource equipWeaponSource;
    AudioSource closeUISource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        backgroundSource = gameObject.AddComponent<AudioSource>();
        fightSource = gameObject.AddComponent<AudioSource>();
        bossSource = gameObject.AddComponent<AudioSource>();
        clickBtnSource = gameObject.AddComponent<AudioSource>();
        equipWeaponSource = gameObject.AddComponent<AudioSource>();
        closeUISource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayBackGroundAudio()
    {
        fightSource.Stop();
        backgroundSource.clip = backgroundClip;
        backgroundSource.Play();
        backgroundSource.loop = true;
    }
    public void PlayFightAudio()
    {
        backgroundSource.Stop();
        fightSource.clip = fightClip;
        fightSource.Play();
        fightSource.loop = true;
    }
    public void PlayClickBtnAudio()
    {
        clickBtnSource.clip = clickBtnClip;
        clickBtnSource.Play();
    }
    public void PlayEquipWeaponAudio()
    {
        equipWeaponSource.clip = equipWeaponClip;
        equipWeaponSource.Play();
    }
    public void PlayCloseUIAudio()
    {
        closeUISource.clip = closeUIClip;
        closeUISource.Play();
    }
}
