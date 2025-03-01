using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DrawerInteractable : XRGrabInteractable
{
    [SerializeField]
    XRSocketInteractor socketInteractor;

    [SerializeField]
    bool isLocked = true;

    Transform parentTransform;
    const string defaultLayer = "Default";
    const string grabLayer = "Grab";



    //XRSocketInteractable HAS AN AWAKE METHOD!!! DO NOT OVERRIDE ITS AWAKE METHOD WITHOUT CALLING IT
    protected override void Awake()
    {
        //Call XRGrabInteractable awake method
        base.Awake();

        //This script's awake code
        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.AddListener(OnDrawerUnlocked);
            socketInteractor.selectExited.AddListener(OnDrawerLocked);
        }

        parentTransform = transform.parent.transform;
    }

    private void OnDrawerLocked(SelectExitEventArgs arg0)
    {
        isLocked = true;
        Debug.Log(isLocked);
    }

    private void OnDrawerUnlocked(SelectEnterEventArgs arg0)
    {
        isLocked = false;
        Debug.Log(isLocked);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        if (isLocked)
        {
            //Change interaction layer mask
            interactionLayers = InteractionLayerMask.GetMask(defaultLayer);
        }
        else
        {
            //Prevent the child object from disconneceting from the parent when grabbed
            //This will allow us to track its local z position
            transform.SetParent(parentTransform, false);
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        interactionLayers = InteractionLayerMask.GetMask(grabLayer);

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
