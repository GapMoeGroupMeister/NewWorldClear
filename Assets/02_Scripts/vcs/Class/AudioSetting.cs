public class AudioSetting
{
    public float soundLevel_Master;
    public float soundLevel_BGM;
    public float soundLevel_SFX;

    public AudioSetting()
    {
        soundLevel_Master = 0;
        soundLevel_BGM = 0;
        soundLevel_SFX = 0;
        
    }

    public AudioSetting(float master, float BGM, float SFX)
    {
        soundLevel_Master = master;
        soundLevel_BGM = BGM;
        soundLevel_SFX = SFX;
        

    }
}