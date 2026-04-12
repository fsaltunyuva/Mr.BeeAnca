using UnityEngine;

public class SingletonMusic : MonoBehaviour
{
    public static SingletonMusic Instance { get; private set; }
    
    [Header ("Audio Sources")]
    [SerializeField] AudioSource audioSourceForSFX;
    [SerializeField] AudioSource audioSourceForBGMusic;
    [SerializeField] AudioSource audioSourceForTrafficJam;
    
    [Header ("Audio Clips")]
    [SerializeField] AudioClip [] bgmClips;
    [SerializeField] AudioClip start_SFX;
    [SerializeField] AudioClip waypoint_SFX;
    [SerializeField] AudioClip arrival_SFX;
    [SerializeField] AudioClip waypointNotPassed_SFX;
    [SerializeField] AudioClip trafficJam_SFX;
    

    private void Awake()
    {
        if (Instance != null && Instance != this) // If there is an instance, and it's not me, delete myself. 
        {
            Destroy(gameObject);
        }
        else // Otherwise, set the instance to me and don't destroy me.
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // This will keep the Singleton object alive between scenes.
        }
    }

    public void PlaySFX(string sfx_name)
    {
        if (sfx_name == "start_SFX")
        {
            audioSourceForSFX.PlayOneShot(start_SFX);
        }
        else if (sfx_name == "waypoint_SFX")
        {
            audioSourceForSFX.PlayOneShot(waypoint_SFX);
        }
        else if (sfx_name == "arrival_SFX")
        {
            audioSourceForSFX.PlayOneShot(arrival_SFX);
        }
        else if (sfx_name == "waypointNotPassed_SFX")
        {
            audioSourceForSFX.PlayOneShot(waypointNotPassed_SFX);
        }
        else if (sfx_name == "trafficJam_SFX")
        {
            audioSourceForTrafficJam.PlayOneShot(trafficJam_SFX);
        }
    }
    
    public void StopTrafficJamSFX()
    {
        audioSourceForTrafficJam.Stop();
    }

    public void PlayBGM(int index)
    {
        if (index < 0 || index >= bgmClips.Length) return;
        if(audioSourceForBGMusic.isPlaying && audioSourceForBGMusic.clip == bgmClips[index]) return;
        
        audioSourceForBGMusic.clip = bgmClips[index];
        audioSourceForBGMusic.loop = true;
        audioSourceForBGMusic.Play();
    }
}