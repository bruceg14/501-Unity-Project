using System;
using UnityEngine;
using UnityEngine.UI;

public class SpeedControlsButton : MonoBehaviour
{
    public Image normalStateImage;

    public Image activeStateImage;

    public Button button;

    public bool activeState { get; private set; }

    public event Action click;

    private void Awake()
    {
        button.onClick.AddListener(() => click?.Invoke());
    }

    public void SetActiveState(bool activeState)
    {
        this.activeState = activeState;
        normalStateImage.gameObject.SetActive(!activeState);
        activeStateImage.gameObject.SetActive(activeState);
    }
}