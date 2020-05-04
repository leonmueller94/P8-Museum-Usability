using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioUIControlManager : MonoBehaviour
{
    [SerializeField] Button skipButton = null, playPauseButton = null;
    [SerializeField] Image talkingIndicator = null, talkingIndicatorIcon = null, playPauseIcon = null;
    [SerializeField] Sprite playSprite = null, notTalkingIndicator = null;

    Sprite defaultPauseSprite, defaultTalkingIndicatorSprite;
    ExhibitAudioManager exhibitAudioManager;

    public Button SkipButton { get { return skipButton; } }

    void Awake()
    {
        exhibitAudioManager = FindObjectOfType<ExhibitAudioManager>();
        defaultPauseSprite = playPauseIcon.GetComponent<Image>().sprite;
        defaultTalkingIndicatorSprite = talkingIndicatorIcon.sprite;
        playPauseButton.gameObject.SetActive(false);
        skipButton.gameObject.SetActive(false);
        talkingIndicator.gameObject.SetActive(false);
    }

    void Update()
    {
        TalkingIcon();
    }

    public void PlayPause()
    {
        if (playPauseIcon.GetComponent<Image>().sprite == defaultPauseSprite)
        {
            playPauseIcon.GetComponent<Image>().sprite = playSprite;
            exhibitAudioManager.GetAudioSource.Pause();
            exhibitAudioManager.IsDisplayQuestions = false;
        }
        else
        {
            playPauseIcon.GetComponent<Image>().sprite = defaultPauseSprite;
            exhibitAudioManager.GetAudioSource.UnPause();
            exhibitAudioManager.IsDisplayQuestions = true;
        }
    }

    public void Skip()
    {
        playPauseIcon.GetComponent<Image>().sprite = defaultPauseSprite;
        exhibitAudioManager.GetAudioSource.Stop();
        exhibitAudioManager.IsDisplayQuestions = true;
    }

    private void TalkingIcon()
    {
        if (exhibitAudioManager.GetAudioSource.isPlaying)
        {
            talkingIndicatorIcon.sprite = defaultTalkingIndicatorSprite;
            talkingIndicator.gameObject.SetActive(true);
            playPauseButton.gameObject.SetActive(true);
            skipButton.gameObject.SetActive(true);

            //if (!storyCompletionManager.IsEnd)
            //{
            //    npc.ChangeExhibitNPC();
            //}
        }
        else
        {
            talkingIndicatorIcon.sprite = notTalkingIndicator;
        }
    }

    public void HideAudioControlUI()
    {
        playPauseButton.gameObject.SetActive(false);
        skipButton.gameObject.SetActive(false);
    }

    public void HideTalkingIcon()
    {
        talkingIndicator.gameObject.SetActive(false);
    }
}