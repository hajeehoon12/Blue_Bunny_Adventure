using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] Sound[] sfx = null;
    [SerializeField] Sound[] bgm = null;

    [SerializeField] public AudioSource bgmPlayer = null;
    [SerializeField] public AudioSource bgmPlayer2 = null;
    [SerializeField] AudioSource[] sfxPlayer = null;
    AudioSource myAudioSource;

    private string audioName;
    public float masterVolumeSFX = 1f;

    private void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        
    }

    private void Start()
    {
        PlayBGM("Peace", 0.3f);
        PlaySFX("Bird", 0.2f);
    }

    public void PlayBGM(string p_bgmName)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            if (p_bgmName == bgm[i].name)
            {
                bgmPlayer.clip = bgm[i].clip;
                bgmPlayer.Play();
                return;
            }
        }
    }
    public void PlayBGM(string p_bgmName, float _volume)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            if (p_bgmName == bgm[i].name)
            {
                bgmPlayer.clip = bgm[i].clip;
                bgmPlayer.Play();
                bgmPlayer.volume = _volume;
                break;
            }
        }
    }

    public void PlayBGM2(string p_bgmName, float _volume)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            if (p_bgmName == bgm[i].name)
            {
                bgmPlayer2.clip = bgm[i].clip;
                bgmPlayer2.Play();
                bgmPlayer2.volume = _volume;
                break;
            }
        }
    }

    public void StopBGM2()
    {
        bgmPlayer2.Stop();
    }


    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    public void PlaySFX(string p_sfxName)
    {
        
        for (int i = 0; i < sfx.Length; i++)
        {
            if (p_sfxName == sfx[i].name)
            {
                myAudioSource.PlayOneShot(sfx[i].clip);
                return;
            }
        }
        Debug.Log(p_sfxName + " �̸��� ȿ������ �����ϴ�.");
        return;
    }

    public void PlaySFX(string p_sfxName, float _volume) // overloading
    {

        for (int i = 0; i < sfx.Length; i++)
        {
            if (p_sfxName == sfx[i].name)
            {
                for (int j = 0; j < sfxPlayer.Length; j++)
                {
                    // SFXPlayer���� ��� ������ ���� Audio Source�� �߰��ߴٸ� 
                    if (!sfxPlayer[j].isPlaying)
                    {
                        sfxPlayer[j].clip = sfx[i].clip;
                        sfxPlayer[j].PlayOneShot(sfx[i].clip, _volume);
                        return;
                    }
                }
                Debug.Log("��� ����� �÷��̾ ������Դϴ�.");
                return;
            }
        }
        Debug.Log(p_sfxName + " �̸��� ȿ������ �����ϴ�.");
        return;
    }


}