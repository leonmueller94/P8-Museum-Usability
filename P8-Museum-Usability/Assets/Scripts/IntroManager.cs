﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class IntroManager : MonoBehaviour
{
    [Header("Start Screen UI")]
    [SerializeField] Button startButton = null;
    [SerializeField] Image logo = null;
    [SerializeField] TextMeshProUGUI title = null;

    [Header("Introduction UI Control")]
    [SerializeField] Button skipButton = null;
    [SerializeField] Button playPauseButton = null;
    [SerializeField] Image talkingIndicator = null, talkingIndicatorIcon = null, playPauseIcon = null;
    [SerializeField] Sprite playSprite = null, notTalkingIndicator = null;

    [Header("Introduction Audio")]
    [SerializeField] AudioClip[] introduction = null;

    Sprite defaultPauseSprite, defaultTalkingIndicatorSprite;
    AudioSource audioSource;
    int audioClipIndex = 0;
    bool isNextClip = true;
    bool isIntroOver = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        defaultPauseSprite = playPauseIcon.GetComponent<Image>().sprite;
        defaultTalkingIndicatorSprite = talkingIndicatorIcon.sprite;

        playPauseButton.gameObject.SetActive(false);
        skipButton.gameObject.SetActive(false);
        talkingIndicator.gameObject.SetActive(false);
    }

    private void Update()
    {
        TalkingIcon();
        IntroductionFinish();
    }

    public void StartJourney()
    {
        title.gameObject.SetActive(false);
        logo.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        playPauseButton.gameObject.SetActive(true);
        skipButton.gameObject.SetActive(true);

        PlayIntroductionStory(introduction, audioClipIndex);
    }

    private void PlayIntroductionStory(AudioClip[] introduction, int audioClipIndex)
    {
        if (audioClipIndex >= introduction.Length) { isIntroOver = true; return; }

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(introduction[audioClipIndex]);
            StartCoroutine(WaitThenResumeAudio());
        }
    }

    IEnumerator WaitThenResumeAudio()
    {
        yield return new WaitUntil(() => !audioSource.isPlaying && isNextClip);
        audioClipIndex++;
        PlayIntroductionStory(introduction, audioClipIndex);
    }

    public void PlayPause()
    {
        if (playPauseIcon.GetComponent<Image>().sprite == defaultPauseSprite)
        {
            playPauseIcon.GetComponent<Image>().sprite = playSprite;
            audioSource.Pause();
            isNextClip = false;
        }
        else
        {
            playPauseIcon.GetComponent<Image>().sprite = defaultPauseSprite;
            audioSource.UnPause();
            isNextClip = true;
        }
    }

    public void Skip()
    {
        audioSource.Stop();
        audioClipIndex++;
        if (audioClipIndex < introduction.Length)
        {
            playPauseIcon.GetComponent<Image>().sprite = defaultPauseSprite;
            PlayIntroductionStory(introduction, audioClipIndex);
        }
        else
        {
            isIntroOver = true;
        }
    }

    private void TalkingIcon()
    {
        if (audioSource.isPlaying)
        {
            talkingIndicator.gameObject.SetActive(true);
            talkingIndicatorIcon.sprite = defaultTalkingIndicatorSprite;
        }
        else
        {
            talkingIndicatorIcon.sprite = notTalkingIndicator;
        }
    }

    private void IntroductionFinish()
    {
        if (isIntroOver)
        {
            SceneManager.LoadScene(1);
        }
    }
}