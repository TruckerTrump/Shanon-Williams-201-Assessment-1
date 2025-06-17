using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float dayDuration = 120f; // Duration of a full day-night cycle in seconds (e.g., 120s = 2 minutes)
    public Light sunLight; // Reference to the directional light
    private float timeOfDay; // 0 to 1, where 0 is midnight and 0.5 is noon

    void Start()
    {
        if (sunLight == null)
        {
            sunLight = GetComponent<Light>();
        }
    }

    void Update()
    {
        // Increment time based on real-time seconds
        timeOfDay += Time.deltaTime / dayDuration;
        if (timeOfDay > 1f)
        {
            timeOfDay -= 1f; // Loop back to 0
        }

        // Update sun position (simple rotation around Y-axis)
        float sunAngle = timeOfDay * 360f - 90f; // -90 to start at midnight
        sunLight.transform.rotation = Quaternion.Euler(sunAngle, 0, 0);

        // Update sun color based on time of day
        UpdateSunColor();
    }

    void UpdateSunColor()
    {
        if (timeOfDay < 0.25f) // Night to Sunrise (0 to 6 AM)
        {
            float t = timeOfDay / 0.25f;
            sunLight.color = Color.Lerp(Color.black, Color.yellow, t);
        }
        else if (timeOfDay < 0.5f) // Sunrise to Noon (6 AM to 12 PM)
        {
            float t = (timeOfDay - 0.25f) / 0.25f;
            sunLight.color = Color.Lerp(Color.yellow, Color.white, t);
        }
        else if (timeOfDay < 0.75f) // Noon to Sunset (12 PM to 6 PM)
        {
            float t = (timeOfDay - 0.5f) / 0.25f;
            sunLight.color = Color.Lerp(Color.white, Color.yellow, t);
        }
        else // Sunset to Night (6 PM to 12 AM)
        {
            float t = (timeOfDay - 0.75f) / 0.25f;
            sunLight.color = Color.Lerp(Color.yellow, Color.black, t);
        }

        // Adjust intensity (brighter during day, dimmer at night)
        sunLight.intensity = Mathf.Lerp(0f, 1f, Mathf.Abs(timeOfDay - 0.5f) * 2f);
    }
}
