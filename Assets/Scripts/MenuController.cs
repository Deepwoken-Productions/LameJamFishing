using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    //Replace with returnToMenu
    [SerializeField] private Button exitGame;
    [SerializeField] private GameObject menuElementsParent;
    [SerializeField] private GameObject warningObject;
    [SerializeField] private GameObject titleObject;

    [SerializeField] private float fadeDuration;

    public void StartGame()
    {
        //StaticHelpers.Move(menuElementsParent.transform, hideLoc.position, lerpDuration);
        for (int i = 0; i < menuElementsParent.transform.childCount; i++)
        {
            int i1 = i;
            StaticHelpers.UIFade(menuElementsParent.transform.GetChild(i).GetComponent<Image>(), fadeDuration, false,  () => menuElementsParent.SetActive(false));
        }
        StaticHelpers.UIFade(titleObject.GetComponent<Image>(), fadeDuration, false, () => titleObject.SetActive(false));
        exitGame.onClick.RemoveAllListeners();
        exitGame.onClick.AddListener(ShowWarning);
        exitGame.GetComponentInChildren<Text>().text = "To Menu";
    }
    private void ShowWarning()
    {
        GameObject go = Instantiate(warningObject, transform);
        go.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        {
            Destroy(go);
        });
        go.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
        {
            ReturnToMenu();
            Destroy(go);
        });
    }

    public void ReturnToMenu()
    {
        print("Called");
        for (int i = 0; i < menuElementsParent.transform.childCount; i++)
        {
            menuElementsParent.SetActive(true);
            StaticHelpers.UIFade(menuElementsParent.transform.GetChild(i).GetComponent<Image>(), fadeDuration, true);
        }
        exitGame.onClick.RemoveAllListeners();
        exitGame.onClick.AddListener(ExitGame);
        exitGame.GetComponentInChildren<Text>().text = "Exit Game";
        titleObject.SetActive(true);
        StaticHelpers.UIFade(titleObject.GetComponent<Image>(), fadeDuration, true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
