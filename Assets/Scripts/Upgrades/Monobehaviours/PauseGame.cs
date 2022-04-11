using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject backgroundSprite;
    public Transform newLoc;
    public GameObject shopObject;

    public bool startPanning;

    Vector3 originalBackgroundPosition;

    public void Pause() => Time.timeScale = 0;

    public void Resume()
    {
        Time.timeScale = 1;
        backgroundSprite.transform.position = originalBackgroundPosition;
    }

    public void SetPanning(bool pan) => startPanning = pan;

    private void Start()
    {
        originalBackgroundPosition = backgroundSprite.transform.position;
    }

    public void StartPanning()
    {
        if (startPanning)
        {
            print("Beginning Lerp");
            StaticHelpers.Move(backgroundSprite.transform, newLoc.position, FindObjectOfType<MenuController>().gameTime, Reset);
        }
    }

    public void Reset()
    {
        Debug.Log("Reseted");
        if(Application.IsPlaying(gameObject))
        backgroundSprite.transform.position = originalBackgroundPosition;
        shopObject.SetActive(true);
    }
}
