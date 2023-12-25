using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SoundManager : MonoBehaviour
{
    private AudioSetting setting;
    [SerializeField] private AudioClip ClickSFX;

    private AudioSource audioSource;
    [Tooltip("오디오 마스터 믹서")]
    public AudioMixer masterMixer;

    [Tooltip("씬 내에 오디오 설정창이 존재하는가?")]
    [SerializeField] private bool isExistSettingWindowInScene;
    [Header("오디오 설정 슬라이더")]

    [SerializeField]
    private Slider audioSlider_Master;
    [SerializeField]
    private Slider audioSlider_BGM;
    [SerializeField]
    private Slider audioSlider_SFX;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        GetAudioSet();
    }


    void Update()
    {
        UpdateAudioSet();
    }
    
    /**
     * <summary>
     * 클릭 이벤트 SFX 오디오를 재생
     * </summary>
     */
    public void PlayClick()
    {
        audioSource.clip = ClickSFX;
        audioSource.Play();
    }
    
    
    /**
     * <summary>
     * 오디오 슬라이더 값에 따라 오디오 설정을 업데이트 해주는 메서드(오디오 설정창이 존재 할때만 실행)
     * </summary>
     */
    public void UpdateAudioSet()
    {
        if (isExistSettingWindowInScene)
        {
            if (audioSlider_Master.value == -40f)
            {
                masterMixer.SetFloat("Volume_Master", -80);
            }
            else
            {
                masterMixer.SetFloat("Volume_Master", audioSlider_Master.value);
            }

            if (audioSlider_BGM.value == -40f)
            {
                masterMixer.SetFloat("Volume_BGM", -80);
            }
            else
            {
                masterMixer.SetFloat("Volume_BGM", audioSlider_BGM.value);
            }

            if (audioSlider_SFX.value == -40f)
            {
                masterMixer.SetFloat("Volume_SFX", -80);
            }
            else
            {
                masterMixer.SetFloat("Volume_SFX", audioSlider_SFX.value);
            }

        }
        

    }

    
    /**
     * <summary>
     * 오디오 설정을 불러와 적용시키는 메서드 (오디오 설정창이 존재 할때만 실행)
     * </summary>
     * 
     */
    public void GetAudioSet()
    {
        if (isExistSettingWindowInScene)
        {
            setting = DBManager.Get_AudioSetting();

            audioSlider_Master.value = setting.soundLevel_Master;
            audioSlider_BGM.value = setting.soundLevel_BGM;
            audioSlider_SFX.value = setting.soundLevel_SFX;
        }
        
    }
    /**
     * <summary>
     * 오디오 설정을 저장하는 메서드 (오디오 설정창이 존재 할때만 실행)
     * </summary>
     */
    public void SaveAudioSet()
    {
        if (isExistSettingWindowInScene)
        {
            setting = new AudioSetting(audioSlider_Master.value, audioSlider_BGM.value, audioSlider_SFX.value);
            DBManager.Save_AudioSetting(setting);
        }
        
    }
    
}