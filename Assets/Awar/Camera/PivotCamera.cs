using Cinemachine;
using UnityEngine;

namespace Awar.Camera
{
    public class PivotCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineFreeLook _cmCamera = default;
        [SerializeField] private float _xSensitivity = 2;
        [SerializeField] private float _ySensitivity = 2;
        [SerializeField] private float _multiplier = 1.5f;
        [SerializeField] private float _panSensitivity = 2f;
        [SerializeField] private AnimationCurve _distanceSensitivityCurve = default;

        private void Update()
        {
            float multiplier = Input.GetKey(KeyCode.LeftShift) ? _multiplier : 1;
            float xAxis = Input.GetAxis("Horizontal") * _xSensitivity;
            float yAxis = Input.GetAxis("Vertical") * _ySensitivity;
            float sensitivityGain = _distanceSensitivityCurve.Evaluate(_cmCamera.m_YAxis.Value) * multiplier;

            transform.position += transform.forward * yAxis * sensitivityGain;
            transform.position += transform.right * xAxis * sensitivityGain;

            float pan = Input.GetMouseButton(2) ? Input.GetAxis("Mouse X") : 0f;
            transform.Rotate(0, pan * _panSensitivity, 0);

        }
    }
}
