using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        { 
            if (instance == null)
            {
                GameObject SoundManager = new GameObject("SoundManager");
                instance = SoundManager.AddComponent<SoundManager>();
                DontDestroyOnLoad(SoundManager);
            }

            return instance;
        }
    }

    private AudioSource bgSound;

    [Header("Background Sound")]
    public AudioClip bgm;

    [Header("Background Clip List")]
    public AudioClip[] clips;

    [Header("Audio Mixer")]
    public AudioMixer mixer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        bgSound = this.GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        for (int i = 0; i < clips.Length; i++)
        {
            if (scene.name == clips[i].name)
            {
                BGPlay(clips[i]);
                break;
            }
            else
                BackgroundStop();
        }
    }

    // 배경음 재생
    private void BGPlay(AudioClip clip)
    {
        if (bgSound == null)
            return;

        bgSound.outputAudioMixerGroup = mixer.FindMatchingGroups("BackGround")[0];
        bgSound.clip = clip;
        bgSound.loop = true;
        if (bgSound != null)
            bgSound.Play();
    }

    // 배경음 정지
    public void BackgroundStop()
    {
        if (bgSound == null)
            return;

        bgSound.Stop();
    }

    // 효과음 생성 및 재생
    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(go, clip.length);
    }

    // 마스터 볼륨 조절
    public void MasterVolume(float volume)
    {
        if (volume > 0)
            mixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        else
            mixer.SetFloat("Master", -80f);
    }

    // 배경음 볼륨 조절
    public void BGVolume(float volume)
    {
        if (volume > 0)
            mixer.SetFloat("BackGround", Mathf.Log10(volume) * 20);
        else
            mixer.SetFloat("BackGround", -80f);
    }

    // 효과음 볼륨 조절
    public void SfxVolume(float volume)
    {
        if (volume > 0)
            mixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        else
            mixer.SetFloat("SFX", -80f);
    }
}
