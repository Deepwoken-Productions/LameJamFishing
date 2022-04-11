using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject backgroundSprite;
    public Transform newLoc;
    public GameObject shopObject;

    Vector3 originalBackgroundPosition;

    public void Pause() => Time.timeScale = 0;

    public void Resume()
    {
        Time.timeScale = 1;
        backgroundSprite.transform.position = originalBackgroundPosition;
    }


    private void Start()
    {
        originalBackgroundPosition = backgroundSprite.transform.position;
    }

    public void StartBorderMove()
    {
       StartCoroutine(StaticHelpers.Move(backgroundSprite.transform, newLoc.position, FindObjectOfType<MenuController>().gameTime, Reset));
    }

    public void Reset()
    {
        backgroundSprite.transform.position = originalBackgroundPosition;
        if(Application.IsPlaying(gameObject))
            shopObject.SetActive(true);
    }
}
