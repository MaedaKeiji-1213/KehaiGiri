using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningManager : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] GameObject UI;
    [SerializeField] Image image;
    AudioSource audio_source;
    // Start is called before the first frame update
    void Start()
    {
        audio_source=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(videoPlayer.time >= 4)
        {
            UI.SetActive(true);
        }
        if(Input.GetKeyUp(KeyCode.Return))
        {
            StartCoroutine(GoGameScene());
        }

    }
    IEnumerator  GoGameScene()
    {
        if(UI.active)
        {
            audio_source.PlayOneShot(Resources.Load<AudioClip>("Sounds/抜刀"));
            for(int i=1;i<=5;i++)
            {
                Color color=image.color;
                yield return new WaitForSeconds(0.1f);
                color.a=0;
                image.color=color;
                yield return new WaitForSeconds(0.1f);
                color.a=1;
                image.color=color;
            }
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("GameScene",LoadSceneMode.Single);
        }
        
    }

}
