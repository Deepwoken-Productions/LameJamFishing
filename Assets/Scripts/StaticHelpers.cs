using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public static class StaticHelpers
{
    public static async void Move(Transform movedObject, Vector2 newLocation, float duration, Action postEvt = null)
    {
        float curTime = 0;
        Vector2 origin = movedObject.position;
        while (curTime < duration)
        {
            
            movedObject.position = Vector2.Lerp(origin, newLocation, curTime/duration);
            curTime += Time.deltaTime;
            await Task.Yield();
        }
        postEvt?.Invoke();
    }

    public static async void UIFade(Image img, float duration, bool fadeIn = false, Action postEvt = null)
    {
        float curTime = 0;
        Color imgColor = img.color;
        float originAlpha = imgColor.a;
        float endAlpha = fadeIn ? 1 : 0;
        while (curTime < duration)
        {
            Debug.Log("Fading");
            img.color = new Color(imgColor.r, imgColor.g, imgColor.b, Mathf.Lerp(originAlpha, endAlpha, curTime / duration));
            curTime += Time.deltaTime;
            await Task.Yield();
        }
        postEvt?.Invoke();
    }
}
