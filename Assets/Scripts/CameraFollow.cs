using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public enum ViewMode
    {
        TopDown,
        Angled,
        Custom
    }

    [SerializeField] private Transform target;
    [SerializeField] private ViewMode viewMode = ViewMode.Angled;

    [Header("Offsets")]
    [SerializeField] private Vector3 topDownOffset = new Vector3(0f, 14f, 0f);
    [SerializeField] private Vector3 angledOffset = new Vector3(-10f, 10f, -10f);
    [SerializeField] private Vector3 customOffset = new Vector3(-8f, 8f, -8f);

    [Header("Look")]
    [SerializeField] private bool lookAtTarget = true;
    [SerializeField] private Vector3 lookAtOffset = new Vector3(0f, 1f, 0f);
    [SerializeField, Min(0f)] private float followSharpness = 12f;

    private void Awake()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                target = player.transform;
        }
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + GetOffset();

        if (followSharpness <= 0f)
            transform.position = desiredPosition;
        else
            transform.position = Vector3.Lerp(transform.position, desiredPosition, 1f - Mathf.Exp(-followSharpness * Time.deltaTime));

        if (lookAtTarget)
            transform.LookAt(target.position + lookAtOffset);
    }

    private Vector3 GetOffset()
    {
        switch (viewMode)
        {
            case ViewMode.TopDown:
                return topDownOffset;
            case ViewMode.Custom:
                return customOffset;
            default:
                return angledOffset;
        }
    }
}
