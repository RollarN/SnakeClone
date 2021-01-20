using UnityEngine;

/// <summary> A component that temporarily scales up its owner object when activated. Scale has no Gameplay impact
/// </summary>
public class EnabledObjectPulsor : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve pulseCurve;
    [SerializeField]
    private float pulseDuration = 0.25f;

    private float currentCurveXPosition;

    private void OnEnable()
    {
        currentCurveXPosition = 0;
    }
    public void Update()
    {
        currentCurveXPosition += Time.deltaTime / pulseDuration;
        transform.localScale = Vector3.one * (pulseCurve.Evaluate(currentCurveXPosition));
    }

}
