#region
using System;
using System.Collections.Generic;
using UnityEngine;
using MEC;
#endregion

public class SoundManager : UnitySingleton<SoundManager>
{
    #region VARIABLE
    [HideInInspector] public GameObject pBGMSound;
    [HideInInspector] public GameObject pSESound;
    [HideInInspector] public Transform pTransform;
    [HideInInspector] public AudioClip[] pBGMClip;
    [HideInInspector] public AudioClip[] pSEClip;

    private AudioSource pBGMSource;
    private List<AudioSource> pSESourceList;
    private bool bInit = false;
    #endregion

    #region UNITY LIFE CYCLE
    public void Awake()
    {
        DontDestroyOnLoad(this);
    }
    #endregion

    #region COMMON METHOD
    public void Init()
    {
        if (bInit.Equals(true))
        {
            return;
        }

        if (pBGMSound != null)
        {
            Destroy(pBGMSound);
            pBGMSound = null;
        }
        if (pSESound != null)
        {
            Destroy(pSESound);
            pSESound = null;
        }

        pBGMSound = new GameObject("BGM Sound");
        pBGMSource = pBGMSound.AddComponent<AudioSource>();
        pBGMSource.playOnAwake = false;
        pBGMSource.volume = 0.5f;
        pBGMSource.pitch = 1.0f;
        pBGMSound.transform.parent = transform;

        pSESourceList = new List<AudioSource>();

        pSESound = new GameObject("SE Sound");
        pSESound.transform.parent = transform;

        pTransform = GetComponent<Transform>();

        pBGMClip = Resources.LoadAll<AudioClip>(GlobalData.szBGMClipPath);
        pSEClip = Resources.LoadAll<AudioClip>(GlobalData.szSEClipPath);

        for (int i = 0; i < pSEClip.Length; i++)
        {
            GameObject pSoundObject = GameObject.Instantiate(Resources.Load(GlobalData.szSoundPrefabPath)) as GameObject;
            pSoundObject.name = pSEClip[i].name;
            pSoundObject.transform.parent = pSESound.transform;
            pSoundObject.SetActive(true);

            AudioSource pAudioSource = pSoundObject.GetComponent<AudioSource>();
            pAudioSource.clip = pSEClip[i];
            pAudioSource.volume = 1.0f;
            pAudioSource.loop = false;
            pAudioSource.playOnAwake = false;
            pSESourceList.Add(pAudioSource);
        }
        bInit = true;
    }
    public void PlayBGM(EBGM enBGM, float fVolume = 1.0f, bool bLoop = false)
    {
        AudioSource pAudioSource = pBGMSource;

        if (pAudioSource == null)
        {
            return;
        }

        if (pAudioSource.isPlaying.Equals(true))
        {
            Timing.RunCoroutine(ChangeBGM(pBGMSource, enBGM, fVolume, bLoop));
        }
        else
        {
            pAudioSource.clip = pBGMClip[Convert.ToInt32(enBGM)];
            pAudioSource.volume = fVolume;
            pAudioSource.loop = bLoop;
            pAudioSource.Play();
        }
    }
    public void PlaySE(ESE enSE, float fVolume = 1.0f, bool bLoop = false)
    {
        int iIndex = (int)enSE;

        if (pSESourceList[iIndex].isPlaying.Equals(true))
        {
            pSESourceList[iIndex].Stop();
        }
        pSESourceList[iIndex].volume = fVolume;
        pSESourceList[iIndex].loop = bLoop;
        pSESourceList[iIndex].Play();
    }
    #endregion

    #region IENUMERATOR
    public IEnumerator<float> ChangeBGM(AudioSource pAudioSource, EBGM enBGM, float fVolume = 1.0f, bool bLoop = false)
    {
        while (pAudioSource.volume > 0.0f)
        {
            pAudioSource.volume -= 0.05f;

            yield return Timing.WaitForSeconds(0.05f);
        }
        pAudioSource.Stop();
        pAudioSource.clip = pBGMClip[Convert.ToInt32(enBGM)];
        pAudioSource.volume = fVolume;
        pAudioSource.loop = bLoop;
        pAudioSource.Play();

        yield break;
    }
    #endregion
}
