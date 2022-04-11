using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    //Replace with returnToMenu
    [SerializeField] private Button exitGame;
    [SerializeField] private GameObject menuElementsParent;
    [SerializeField] private GameObject warningObject;
    [SerializeField] private GameObject titleObject;
    [SerializeField] private GameObject ptsObject;
    [SerializeField] private float fadeDuration;
    [SerializeField] private Text masterTXT;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private AudioLink music;
    [SerializeField] private AudioLink sfx;
    [SerializeField] private UnityEvent onStart;

    private float _masterMultiplier;

    private void Awake()
    {
        masterSlider.onValueChanged.AddListener((newVal) => {
            _masterMultiplier = newVal;
            masterTXT.text = ((int)(_masterMultiplier*100)).ToString();
        });
        music.Init();
        sfx.Init();
    }

    public void StartGame()
    {
        
        //StaticHelpers.Move(menuElementsParent.transform, hideLoc.position, lerpDuration);
        for (int i = 0; i < menuElementsParent.transform.childCount; i++)
        {
            int i1 = i;
            StaticHelpers.UIFade(menuElementsParent.transform.GetChild(i).GetComponent<Image>(), fadeDuration, false,  () =>
            {
                menuElementsParent.SetActive(false);
                onStart.Invoke();
                ptsObject.SetActive(true);
            });
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
            StaticHelpers.UIFade(menuElementsParent.transform.GetChild(i).GetComponent<Image>(), fadeDuration, true,
                () => { ptsObject.SetActive(false); });
            
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

[Serializable]
public struct AudioLink
{
    public Slider slider;
    public AudioSource [] audioSource;
    public Text txt;
    public Slider multipler;

    public void Init()
    {
        AudioLink tmpThis = this;
        
        tmpThis.multipler.onValueChanged.AddListener(newVal =>
        {
            float multi = tmpThis.slider.value;
            for (int i = 0; i < tmpThis.audioSource.Length; i++)
                tmpThis.audioSource[i].volume = multi * newVal;
        });
        
        tmpThis.slider.onValueChanged.AddListener( (newVal) =>
        {
            float multi = tmpThis.multipler.value; 
            tmpThis.txt.text = ((int)(newVal*100)).ToString();
            for (int i = 0; i < tmpThis.audioSource.Length; i++)
                tmpThis.audioSource[i].volume = multi * newVal;
        });
    }
}
