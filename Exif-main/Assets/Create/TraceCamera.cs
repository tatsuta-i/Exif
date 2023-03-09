using Google.XR.ARCoreExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TraceCamera : MonoBehaviour
{
    private static readonly Matrix4x4 _unityWorldToGLWorld = Matrix4x4.Scale(new Vector3(1, 1, -1));
    private static readonly Matrix4x4 _unityWorldToGLWorldInverse = _unityWorldToGLWorld.inverse;
    private static readonly Quaternion _unityWorldToGLWorldRotation = Quaternion.LookRotation(_unityWorldToGLWorld.GetColumn(2), _unityWorldToGLWorld.GetColumn(1));
    private static readonly Quaternion _glWorldToUnityWorldRotation = Quaternion.Inverse(_unityWorldToGLWorldRotation);
    public GameObject _camera;
    public GameObject _georeference;
    public AREarthManager EarthManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //yの値だけ0にしてコピーする
        Vector3 pos = _camera.transform.position;
        pos.y = 0;
        _georeference.transform.position = pos;

        // カメラの向いている方向へ向かせる回転を計算（Vector3.forwardに掛けるとカメラの方向になる回転）
        Vector3 forward = Vector3.ProjectOnPlane(_camera.transform.forward, Vector3.up).normalized;
        Quaternion look = Quaternion.LookRotation(forward, Vector3.up);

        GeospatialPose data = EarthManager.CameraGeospatialPose;
        // GeospatialAPIの回転をUnity座標系の回転へ変換する
        float y = data.EunRotation.eulerAngles.y;
        Quaternion rotation = _glWorldToUnityWorldRotation * Quaternion.AngleAxis(y, Vector3.up);
        _georeference.transform.rotation = rotation * look;
    }
}
