using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBreathing : MonoBehaviour
{
    [Header("Breathing Settings")]
    public float verticalAmplitude = 0.02f;
    public float horizontalAmplitude = 0.015f;
    public float baseFrequency = 1.2f;
    public float positionUpdateSpeed = 5f;

    private Vector3 targetBasePosition;
    private float timeOffset;
    private float randomFreqMultiplier;

    void Start()
    {
        targetBasePosition = transform.localPosition;
        timeOffset = Random.Range(0f, 100f); // Desfase para que no arranque igual cada vez
        randomFreqMultiplier = Random.Range(0.9f, 1.2f); // Frecuencia inicial levemente aleatoria
    }

    void Update()
    {
        // Actualiza la posición base suavemente si la cámara se mueve
        targetBasePosition = Vector3.Lerp(targetBasePosition, transform.localPosition, Time.deltaTime * positionUpdateSpeed);

        // Cambia aleatoriamente la frecuencia cada ciertos intervalos
        if (Time.frameCount % 240 == 0) // Aproximadamente cada 4 segundos a 60fps
        {
            randomFreqMultiplier = Random.Range(0.9f, 1.2f);
        }

        float t = (Time.time + timeOffset) * baseFrequency * randomFreqMultiplier;

        float offsetY = Mathf.Sin(t) * verticalAmplitude;
        float offsetX = Mathf.Cos(t * 0.8f) * horizontalAmplitude;

        transform.localPosition = targetBasePosition + new Vector3(offsetX, offsetY, 0f);
    }
}
