using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager2 : MonoBehaviour
{
    //общедоступный объект сценирия
    public static AudioManager2 instance;

    //связываем объект со скриптом
    void Awake() { instance = this; }

    public List<AudioClip> sfxLibrary;
    public AudioClip sfx_jump, sfx_hit, sfx_char, sfx_dash, sfx_attack;

    public AudioClip music_back;
    //текущая музыка
    public GameObject currentMusicObject;

    public GameObject soundObject;

    public void PlaySFX(string sfxName)
    {
        switch (sfxName)
        {
            case "jump":
                SoundObjectCreation(sfx_jump);
                break;
            case "hit":
                SoundObjectCreation(sfx_hit);
                break;
            case "char":
                SoundObjectCreation(sfx_char);
                break;
            case "dash":
                SoundObjectCreation(sfx_dash);
                break;
            case "attack":
                SoundObjectCreation(sfx_attack);
                break;
            default:
                break;
        }
    }


    public void PlayMusic(string musicName)
    {
        switch (musicName)
        {
            case "music_back":
                SoundObjectCreation(music_back);
                break;
            default:
                break;
        }
    }


    void SoundObjectCreation(AudioClip clip)
    {
        //Создаем звуковой объект
        GameObject newObject = Instantiate(soundObject, transform);
        //Подставляем нужный звук
        newObject.GetComponent<AudioSource>().clip = clip;
        //Воспроизводим звук
        newObject.GetComponent<AudioSource>().Play();

    }

    public void MusicObjectCreation(AudioClip clip)
    {
        //проверяем существует ли музыка
        if (currentMusicObject)
            Destroy(currentMusicObject);
        //Создаем звуковой объект
        GameObject newObject = Instantiate(soundObject, transform);
        //Подставляем нужный звук
        currentMusicObject.GetComponent<AudioSource>().clip = clip;
        //Зацикливаем источник звука
        currentMusicObject.GetComponent<AudioSource>().loop = true;
        //Воспроизводим звук
        currentMusicObject.GetComponent<AudioSource>().Play();

    }
}
