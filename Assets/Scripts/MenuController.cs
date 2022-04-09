using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    //Replace with returnToMenu
    [SerializeField] private GameObject exitGame;

    [SerializeField] private GameObject menuElementsParent;
    [SerializeField] private Transform hideLoc;
    [SerializeField] private float lerpDuration;
    [SerializeField] private GameObject titleObject;

    [SerializeField] private float fadeDuration;
    // Start is called before the first frame update
    private Image titleImg;

    private void Awake()
    {
        titleImg = titleObject.GetComponent<Image>();
    }

    public void StartGame()
    {
        print("called");
        StaticHelpers.Move(menuElementsParent.transform, hideLoc.position, lerpDuration);
        StaticHelpers.UIFade(titleImg, fadeDuration);
    }
    
    public void ReturnToMenu()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
