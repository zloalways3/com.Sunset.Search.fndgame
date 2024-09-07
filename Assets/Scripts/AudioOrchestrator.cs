using Other;
using UnityEngine;
using UnityEngine.Audio;

public class AudioOrchestrator : MonoBehaviour
{
    public AudioMixer audioMixer; // Перетащите сюда ваш AudioMixer из инспектора
    public Animator musicAnimator;
    public Animator _soundAnimator; // Перетащите сюда ваш Animator для анимации

    private bool isMusicOn;
    private bool isSoundOn;

    void Start()
    {
        LoadSettings(); // Загружаем настройки при старте
        SetInitialVolume(); // Устанавливаем начальное состояние звука
    }

    private void SetInitialVolume()
    {
        float musicVolume;
        audioMixer.GetFloat("MusicVolume", out musicVolume);
        isMusicOn = musicVolume > -80f; // Проверяем, включена ли музыка
        if (isMusicOn)
            musicAnimator.SetTrigger("OnScrollbarMusic");
        else
            musicAnimator.SetTrigger("OffScrollbarMusic");

        float soundVolume;
        audioMixer.GetFloat("SoundVolume", out soundVolume);
        isSoundOn = soundVolume > -80f; // Проверяем, включены ли эффекты
        if (isSoundOn)
            _soundAnimator.SetTrigger("OnScrollbarMusic");
        else
            _soundAnimator.SetTrigger("OffScrollbarMusic");
    }

    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;

        if (isMusicOn)
        {
            // Включаем музыку
            audioMixer.SetFloat("MusicVolume", 0f); // Установите нужное значение громкости
            musicAnimator.SetTrigger("OnScrollbarMusic"); // Запускаем анимацию включения
        }
        else
        {
            // Выключаем музыку
            audioMixer.SetFloat("MusicVolume", -80f); // Убираем звук
            musicAnimator.SetTrigger("OffScrollbarMusic"); // Запускаем анимацию выключения
        }

        SaveSettings(); // Сохраняем настройки после изменения
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;

        if (isSoundOn)
        {
            // Включаем звуковые эффекты
            audioMixer.SetFloat("SoundVolume", 0f); // Установите нужное значение громкости
            _soundAnimator.SetTrigger("OnScrollbarMusic");
        }
        else
        {
            // Выключаем звуковые эффекты
            audioMixer.SetFloat("SoundVolume", -80f); // Убираем звук
            _soundAnimator.SetTrigger("OffScrollbarMusic");
        }

        SaveSettings(); // Сохраняем настройки после изменения
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetInt("MusicOn", isMusicOn ? 1 : 0);
        PlayerPrefs.SetInt("SoundOn", isSoundOn ? 1 : 0);
        PlayerPrefs.Save(); // Сохраняем изменения
    }

    private void LoadSettings()
    {
        isMusicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1; // По умолчанию музыка включена
        isSoundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1; // По умолчанию звуки включены

        // Устанавливаем начальные значения громкости в зависимости от загруженных настроек
        audioMixer.SetFloat("MusicVolume", isMusicOn ? 0f : -80f);
        audioMixer.SetFloat("SoundVolume", isSoundOn ? 0f : -80f);
    }
}