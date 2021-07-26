using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioListener audioListener;
    public AudioSource musicSource;
    public AudioClip clickSound;
    public AudioClip lost;

    public AudioClip error;
    public AudioClip getScore;
    public float [] pitchValues;
    public float [] notesTime;
    public int [] pitchOrder;

    public Image muteBtn;
    public Color disableColorW;
    public Color originalColorW;
    public Color disableColorB;
    public Color originalColorB;
    int indexNote=0;

    public int channel, numSamples, freq;
    public float pitchMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if ( !PlayerPrefs.HasKey("SoundEffectsOn")) PlayerPrefs.SetInt("SoundEffectsOn", 1);
        if (PlayerPrefs.GetInt("SoundEffectsOn")==0) {
            Mute();
        }
        StartCoroutine(MusicPitch());
    }

/*
    void Update(){
        if (Input.GetKeyDown(KeyCode.S))
        {
            ScreenCapture.CaptureScreenshot("Screenshot"+ssIndex+".png");
            ssIndex++;
        }
        if (Input.GetKeyDown(KeyCode.Q)){
            audioSource.pitch = pitchValues[0];
            PlaySound(5);
        }
        if (Input.GetKeyDown(KeyCode.W)){
            audioSource.pitch = pitchValues[1];
            PlaySound(5);
        }
        if (Input.GetKeyDown(KeyCode.E)){
            audioSource.pitch = pitchValues[2];
            PlaySound(5);
        }
        if (Input.GetKeyDown(KeyCode.R)){
            audioSource.pitch = pitchValues[3];
            PlaySound(5);
        }
        if (Input.GetKeyDown(KeyCode.T)){
            audioSource.pitch = pitchValues[4];
            PlaySound(5);
        }
    }*/

    public void PlayClickSound(){
        if (PlayerPrefs.GetInt("SoundEffectsOn") == 1) {
            audioSource.PlayOneShot(clickSound);
        }
    }

    public void PlaySound(int code){

        switch (code) {
            case 0:
                audioSource.pitch = Random.Range(1.2f,2f);
                audioSource.PlayOneShot(clickSound);
                break;
            case 1:
                audioSource.PlayOneShot(lost);
                break;
            case 3:
                audioSource.PlayOneShot(error);
                break;
            case 4:
                //var number = AudioListener.GetSpectrumData(numSamples, channel, FFTWindow.BlackmanHarris);
                //float pitch =  number[freq]*pitchMultiplier;
                //audioSource.pitch =Mathf.Clamp(pitch, 0.8f, 1.5f);

                audioSource.pitch = pitchValues[pitchOrder[indexNote]];
                audioSource.PlayOneShot(getScore);
                //indexNote++;
                break;
            case 5:
                audioSource.PlayOneShot(getScore);
                break;

        }
    }

    public void Mute(){
        audioSource.mute= !audioSource.mute;
        musicSource.mute= !musicSource.mute;
        if (audioSource.mute) {
            PlayerPrefs.SetInt("SoundEffectsOn", 0);
            if (PlayerPrefs.GetInt("Mode")==1) {
                muteBtn.color = disableColorW;
            }else{
                muteBtn.color = disableColorB;
            }

        }else{
            PlayerPrefs.SetInt("SoundEffectsOn", 1);
            if (PlayerPrefs.GetInt("Mode")==1) {
                muteBtn.color = originalColorW;
            }else{
                muteBtn.color = originalColorB;
            }

        }
    }

    public IEnumerator MusicPitch(){
        while(true){
            //Debug.Log(indexNote);
            float timeToWait = notesTime[indexNote+1]-notesTime[indexNote];
            yield return new WaitForSeconds(timeToWait);
            indexNote++;
            if (indexNote>=notesTime.Length-1) {
                indexNote=0;
            }

        }
    }
}
