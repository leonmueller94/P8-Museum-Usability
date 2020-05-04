using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ImageTargetContainer : MonoBehaviour
{
    [SerializeField] GameObject cauldron;
    [SerializeField] GameObject sword;

    ObjectTracker objectTracker;

    List<GameObject> imageTargets;

    int localTarget;
    bool firstRun;
    void Awake()
    {
        imageTargets = new List<GameObject>();

        imageTargets.Add(cauldron);
        imageTargets.Add(sword);

        ImageTargetController.NumberOfImageTargets = imageTargets.Count;

        localTarget = ImageTargetController.CurrentImageTarget;
        firstRun = true;

        VuforiaARController.Instance.RegisterVuforiaStartedCallback(ActiavteImageTarget);
    }

  
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I))
        {
            ImageTargetController.SwitchToNextImageTarget();
        }

        if (localTarget != ImageTargetController.CurrentImageTarget)
        {
            ActiavteImageTarget();
            localTarget = ImageTargetController.CurrentImageTarget;
        }
    }

    private void ActiavteImageTarget()
    {
        if (firstRun)
        {
            objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
            firstRun = false;
        }
        
        objectTracker.Stop();

        foreach (var item in imageTargets)
        {
            item.SetActive(false);
        }

        imageTargets[ImageTargetController.CurrentImageTarget].SetActive(true);
        
        objectTracker.Start();
    }
}
