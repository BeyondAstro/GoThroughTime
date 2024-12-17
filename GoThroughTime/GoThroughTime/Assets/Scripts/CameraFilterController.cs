using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraFilterController : MonoBehaviour
{
    // Reference to the Post Process Volume component
    private PostProcessVolume postProcessVolume;
    
    // You can add specific effects you want to toggle
    private ColorGrading colorGrading;
    private Vignette vignette;
    private ChromaticAberration chromaticAberration;
    
    private void Start()
    {
        // Get the Post Process Volume component
        postProcessVolume = GetComponent<PostProcessVolume>();
        
        // Get specific effects from the volume
        postProcessVolume.profile.TryGetSettings(out colorGrading);
        postProcessVolume.profile.TryGetSettings(out vignette);
        postProcessVolume.profile.TryGetSettings(out chromaticAberration);
        
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
        if (vignette != null)
            vignette.active = isActive;
        if (chromaticAberration != null)
            chromaticAberration.active = isActive;
    }

    public void SetFilterInActive()
    {
        // Enable/disable the entire post process volume
        postProcessVolume.enabled = false;
        
        // Alternatively, you can toggle specific effects
        if (colorGrading != null)
            colorGrading.active = false;
        if (vignette != null)
            vignette.active = false;
        if (chromaticAberration != null)
            chromaticAberration.active = false;
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