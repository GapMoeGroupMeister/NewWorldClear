using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    [SerializeField]private CinemachineVirtualCamera playerCam;
    private CinemachineBasicMultiChannelPerlin channelPerlin;

    private float time;

    private void Awake()
    {
        channelPerlin = playerCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake(float amplitude, float frequency, float shakeTime)
    {
        time = shakeTime;
        channelPerlin.m_AmplitudeGain = amplitude;
        channelPerlin.m_FrequencyGain = frequency;
        StartCoroutine("ShakeRoutine");
    }


    IEnumerator ShakeRoutine()
    {
        yield return new WaitForSeconds(time);
        channelPerlin.m_AmplitudeGain = 0;
        channelPerlin.m_FrequencyGain = 0;
    }
}
