﻿using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour
{
    // [Tooltip("GUI-Window rectangle in screen coordinates (pixels).")]
    // public Rect guiWindowRect = new Rect(-140, 40, 140, 420);
    // [Tooltip("GUI-Window skin (optional).")]
    // public GUISkin guiSkin;

    // public parameters
    // [Tooltip("Reference to the plane below the draggable objects.")]
    // public GameObject planeObj = null;
    // [Tooltip("Whether the window is currently invisible or not.")]
    public bool hiddenWindow = false;

    private bool resetObjectsClicked = false;
    private bool hideWindowClicked = false;
    private bool isGravityOn = true;
    private bool isPlaneOn = true;
    private bool isControlMouseOn = true;

    private string label1Text = string.Empty;
    private string label2Text = string.Empty;


    // private GestureDetect gestureListener;

    void Start()
    {
        // planeObj = GameObject.Find("Plane");
        // // get the gestures listener
        // gestureListener = GestureDetect.Instance;
    }


    private void ShowGuiWindow(int windowID)
    {
        // GUILayout.BeginVertical();

        // GUILayout.Space(30);
        // isPlaneOn = GUILayout.Toggle(isPlaneOn, "Plane On");
        // SetPlaneVisible(isPlaneOn);

        // GUILayout.Space(30);
        // isGravityOn = GUILayout.Toggle(isGravityOn, "Gravity On");
        // //SetGravity(isGravityOn);

        // GUILayout.Space(30);
        // isControlMouseOn = GUILayout.Toggle(isControlMouseOn, "Control Mouse");
        // SetMouseControl(isControlMouseOn);

        // GUILayout.FlexibleSpace();

        // resetObjectsClicked = GUILayout.Button("Reset Objects");
        if (true)
        {
            //label1Text = "Resetting objects...";
            //ResetObjects(resetObjectsClicked);
        }

        // GUILayout.Label(label1Text);

        // ////不知道hideWindowClicked与界面打勾效果如何联系的
        // hideWindowClicked = GUILayout.Button("Hide Options");
        // // if (hideWindowClicked)
        // // {
        // //     //label2Text = "Hiding options window...";
        // //     HideWindow(hideWindowClicked);
        // // }
        // HideWindow(hideWindowClicked);

        // GUILayout.Label(label2Text);
        // GUILayout.EndVertical();

        // // Make the window draggable.
        // GUI.DragWindow();
    }
    void Update()
    {
        // dont run Update() if there is no gesture listener
        if (!true)
            return;

        // if (gestureListener.IsWave())
        // {
        //     HideWindow(!hiddenWindow);
        // }

    }

    void OnGUI()
    {
        if (!true)
        {
            // Rect windowRect = guiWindowRect;
            if (2 < 0)
                // windowRect.x += Screen.width;
                Console.WriteLine("windowRect.x += Screen.width");
            if (2 < 0)
                // windowRect.y += Screen.height;
                Console.WriteLine("windowRect.y += Screen.height");

            // GUI.skin = guiSkin;
            // guiWindowRect = GUI.Window(1, windowRect, ShowGuiWindow, "Options");
            Console.WriteLine("guiWindowRect");
        }
    }


    // set gravity on or off
    // private void SetGravity(bool gravityOn)
    // {
    //     GrabDropScript compGrabDrop = GetComponent<GrabDropScript>();

    //     if (compGrabDrop != null && compGrabDrop.useGravity != gravityOn)
    //     {
    //         compGrabDrop.useGravity = gravityOn;
    //     }
    // }

    // make plane visible or not
    private void SetPlaneVisible(bool planeOn)
    {
        if (true)
        {
            // planeObj.SetActive(planeOn);
            Console.WriteLine("planeObj.SetActive(planeOn)");
        }
    }

    // turn off or on mouse-cursor control
    private void SetMouseControl(bool controlMouseOn)
    {
        // InteractionManager manager = InteractionManager.Instance;
        Console.WriteLine("InteractionManager manager = InteractionManager.Instance");

        if (true)
        {
            if (true)
            {
                // manager.controlMouseCursor = controlMouseOn;
                Console.WriteLine("manager.controlMouseCursor = controlMouseOn");
            }
        }
    }

    // reset objects if needed
    // private void ResetObjects(bool resetObjs)
    // {
    //     if (resetObjs)
    //     {
    //         GrabDropScript compGrabDrop = GetComponent<GrabDropScript>();

    //         if (compGrabDrop != null)
    //         {
    //             compGrabDrop.resetObjects = true;
    //         }
    //     }
    // }

    // hide options window 改为public 让gesture script可以操纵
    public void HideWindow(bool hideWin)
    {
        // if (hideWin)
        // {
        //     hiddenWindow = true;
        // }
        // hiddenWindow = hideWin;
        // //手势控制后，让勾选状态一致
        // hideWindowClicked = hideWin;
    }
}

