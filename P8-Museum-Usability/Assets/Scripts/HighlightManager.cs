using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class HighlightManager : MonoBehaviour
{
    [SerializeField] ExhibitAudioManager exhibitAudioManager = null;
    [SerializeField] UnityEngine.UI.Image cameraIcon = null;
    ObjectTracker objectTracker;

    private void Awake()
    {
        exhibitAudioManager.GetComponent<ExhibitAudioManager>();
        HighlightController.InitializeHighlightList();

        VuforiaARController.Instance.RegisterVuforiaStartedCallback(InstantiateObjectTracker); 
    }

    private void InstantiateObjectTracker()
    {
        objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
    }

    public void TriggerHighlight()
    {
        if (exhibitAudioManager.CurrentExhibitStory[0].exhibitTag == "Sword")
        {
            if (exhibitAudioManager.AudioClipIndex == 3)
            {
                InsertValue(1, 1);
            }
            else if (exhibitAudioManager.AudioClipIndex == 8)
            {
                InsertValue(1, 2);
            }
            else
            {
                if(HighlightController.Highlights.Count > 1)
                {
                    objectTracker.Stop();
                    HighlightController.ClearHighlights();
                    objectTracker.Start();
                }
            }
        }
        else if (exhibitAudioManager.CurrentExhibitStory[0].exhibitTag == "Tub")
        {
            if (exhibitAudioManager.AudioClipIndex == 2)
            {
                InsertValue(1, 1);
                InsertValue(2, 2);
            }
            else if (exhibitAudioManager.AudioClipIndex == 6)
            {
                HighlightController.ClearHighlights();
                InsertValue(1,3);
            }
            else if (exhibitAudioManager.AudioClipIndex == 7)
            {
                HighlightController.ClearHighlights();
                InsertValue(1,4);
            }
            else
            {
                if (HighlightController.Highlights.Count > 1)
                {
                    objectTracker.Stop();
                    HighlightController.ClearHighlights();
                    objectTracker.Start();
                }
            }
        }
        else if (exhibitAudioManager.CurrentExhibitStory[0].exhibitTag == "Skull")
        {
            if (exhibitAudioManager.AudioClipIndex == 4)
            {
                InsertValue(1, 1);
            }
            else if (exhibitAudioManager.AudioClipIndex == 5)
            {
                InsertValue(1, 2);
            }
            else
            {
                if (HighlightController.Highlights.Count > 1)
                {
                    objectTracker.Stop();
                    HighlightController.ClearHighlights();
                    objectTracker.Start();
                }
            }
            
        }
    }

    private void InsertValue(int index, int value)
    {
        if (HighlightController.Highlights.Count == index)
        {
            objectTracker.Stop();
            HighlightController.Highlights.Insert(index, value);
            objectTracker.Start();
        }
        else
        {
            objectTracker.Stop();
            HighlightController.Highlights[index] = value;
            objectTracker.Start();
        }
    }
}
