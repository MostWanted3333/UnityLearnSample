using System.Collections;
using UnityEngine;

[HelpURL("https://docs.google.com/document/d/1rdTEVSrCcYOjqTJcFCHj46RvnbdJhmQUb3gHMDhVftI/edit?usp=sharing")]
public class ScalerModule : MonoBehaviour
{
    [Header("Scale Settings")]

    [SerializeField]
    [Tooltip("Scale that the object will change to after activation.")]
    [Min(0.1f)]
    private Vector3 targetScale = new Vector3(2, 2, 2);

    [SerializeField]
    [Tooltip("Speed of changing the object scale.")]
    [Range(0.1f, 10f)]
    private float changeSpeed = 1f;

    private Vector3 defaultScale;
    private Transform myTransform;
    private bool toDefault;

    private void Start()
    {
        myTransform = transform;
        defaultScale = myTransform.localScale;
        toDefault = false;
    }

    [ContextMenu("Activate Module")]
    public void ActivateModule()
    {
        Vector3 target = toDefault ? defaultScale : targetScale;

        StopAllCoroutines();
        StartCoroutine(ScaleCoroutine(target));

        toDefault = !toDefault;
    }

    [ContextMenu("Return To Default State")]
    public void ReturnToDefaultState()
    {
        toDefault = true;
        ActivateModule();
    }

    private IEnumerator ScaleCoroutine(Vector3 target)
    {
        Vector3 start = myTransform.localScale;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * changeSpeed;
            myTransform.localScale = Vector3.Lerp(start, target, t);
            yield return null;
        }

        myTransform.localScale = target;
    }

    private void OnValidate()
    {
        targetScale.x = Mathf.Max(0.1f, targetScale.x);
        targetScale.y = Mathf.Max(0.1f, targetScale.y);
        targetScale.z = Mathf.Max(0.1f, targetScale.z);
        changeSpeed = Mathf.Max(0.1f, changeSpeed);
    }
}