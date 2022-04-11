using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject backgroundSprite;
    public Vector3 backgroundPanDirection;

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

    private void Update()
    {
        if (startPanning)
        {
            backgroundSprite.transform.position -= backgroundPanDirection * Time.deltaTime;
        }
    }
}
