using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class CameraFilterController : MonoBehaviour
{
    // Reference to the Post Process Volume component
    private PostProcessVolume postProcessVolume;
    
    // Reference to the Post Process Profile
    [SerializeField]
    private PostProcessProfile postProcessProfile;
    
    // Effects
    private ColorGrading colorGrading;
    
    private void Awake()
    {
        // Make sure this object persists across scenes if needed
        // DontDestroyOnLoad(gameObject);
        
        InitializePostProcessing();
        
        // Subscribe to scene loading event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnDestroy()
    {
        // Unsubscribe from scene loading event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    private void InitializePostProcessing()
    {
        // Get or add Post Process Volume component
        postProcessVolume = GetComponent<PostProcessVolume>();
        if (postProcessVolume == null)
        {
            postProcessVolume = gameObject.AddComponent<PostProcessVolume>();
        }
        
        // Ensure we have a profile
        if (postProcessProfile != null)
        {
            postProcessVolume.profile = postProcessProfile;
        }
        else if (postProcessVolume.profile == null)
        {
            // Create a new profile if none exists
            postProcessProfile = ScriptableObject.CreateInstance<PostProcessProfile>();
            postProcessVolume.profile = postProcessProfile;
        }
        
        // Get or create effects
        if (!postProcessVolume.profile.TryGetSettings(out colorGrading))
        {
            colorGrading = postProcessVolume.profile.AddSettings<ColorGrading>();
        }
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reinitialize post-processing when a new scene loads
        InitializePostProcessing();
        
        // Make sure the main camera has a Post Process Layer
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            PostProcessLayer layer = mainCamera.GetComponent<PostProcessLayer>();
            if (layer == null)
            {
                layer = mainCamera.gameObject.AddComponent<PostProcessLayer>();
                layer.volumeLayer = LayerMask.GetMask("Default");
            }
        }   
        // Start with the filter disabled
        SetFilterActive(false);
    }
    
    public void SetFilterActive(bool isActive)
    {
        // Enable/disable the entire post process volume
        postProcessVolume.enabled = isActive;
        
        // Alternatively, you can toggle specific effects
        if (colorGrading != null)
            colorGrading.active = isActive;
    }

    public void SetFilterInActive()
    {
        // Enable/disable the entire post process volume
        postProcessVolume.enabled = false;
        
        // Alternatively, you can toggle specific effects
        if (colorGrading != null)
            colorGrading.active = false;
    }
    
    // Example method to toggle the filter
    public void ToggleFilter()
    {
        SetFilterActive(!postProcessVolume.enabled);
    }
    
    // Example of triggering via input
    private void Update()
    {
        
        // Toggle filter when F key is pressed
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleFilter();
        }

        if((Input.GetKeyDown(KeyCode.RightBracket))||(Input.GetKeyDown(KeyCode.LeftBracket)))
        {
            SetFilterInActive();
        }
    }
}