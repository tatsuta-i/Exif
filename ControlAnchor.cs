using Google.XR.ARCoreExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ControlAnchor : MonoBehaviour
{
    public AREarthManager EarthManager;
    public ARCoreExtensions ARCoreExtensions;
    public CesiumGlobeAnchor cesiumGlobeAnchor;

    private double latitude = -1;
    private double longtitude = -1;
    private double altitude = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //現在地情報を更新
        var earthTrackingState = EarthManager.EarthTrackingState;
        var pose = earthTrackingState == TrackingState.Tracking ?
            EarthManager.CameraGeospatialPose : new GeospatialPose();
        if (earthTrackingState == TrackingState.Tracking)
        {
            latitude = pose.Latitude;
            longtitude = pose.Longitude;
            altitude = pose.Altitude;
            cesiumGlobeAnchor.Position(latitude, longtitude, altitude);
        }
    }
}
