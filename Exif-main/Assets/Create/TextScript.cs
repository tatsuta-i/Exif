using Google.XR.ARCoreExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TextScript : MonoBehaviour
{
    public AREarthManager EarthManager;
    public ARCoreExtensions ARCoreExtensions;
    public Text infoText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Geospatialが利用できるか判定
        var featureSupport = EarthManager.IsGeospatialModeSupported(GeospatialMode.Enabled);
        switch (featureSupport)
        {
            case FeatureSupported.Unknown:
                this.infoText.text = "FeatureSupported UnKnown";
                return;
            case FeatureSupported.Unsupported:
                this.infoText.text = "Geospatial API is not supported by this devices.";
                return;
            case FeatureSupported.Supported:
                if (ARCoreExtensions.ARCoreExtensionsConfig.GeospatialMode ==
                    GeospatialMode.Disabled)
                {
                    Debug.Log("Geospatial sample switched to GeospatialMode.Enabled.");
                    ARCoreExtensions.ARCoreExtensionsConfig.GeospatialMode =
                        GeospatialMode.Enabled;
                    return;
                }

                break;
        }

        //現在地情報を表示
        var earthTrackingState = EarthManager.EarthTrackingState;
        var pose = earthTrackingState == TrackingState.Tracking ?
            EarthManager.CameraGeospatialPose : new GeospatialPose();
        if (earthTrackingState == TrackingState.Tracking)
        {
            this.infoText.text = string.Format(
            "Latitude/Longitude: {1}°, {2}°{0}" +
            "Horizontal Accuracy: {3}m{0}" +
            "Altitude: {4}m{0}" +
            "Vertical Accuracy: {5}m{0}" +
            "Heading: {6}°{0}" +
            "Heading Accuracy: {7}°",
            System.Environment.NewLine,
            pose.Latitude.ToString("F6"),
            pose.Longitude.ToString("F6"),
            pose.HorizontalAccuracy.ToString("F6"),
            pose.Altitude.ToString("F2"),
            pose.VerticalAccuracy.ToString("F2"),
            pose.Heading.ToString("F1"),
            pose.HeadingAccuracy.ToString("F1"));
        } else
        {
            this.infoText.text = "GEOSPATIAL POSE: not tracking";
        }
    }
}
