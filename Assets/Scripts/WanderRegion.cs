using UnityEngine;

public class WanderRegion : MonoBehaviour
{
    public Vector3 size = new Vector3(10, 0, 10);
    [SerializeField] private Color componentColor = new Color(0.6f, 1f, 0.35f);

    private void Awake()
    {
        ComponentColorUtility.Apply(gameObject, componentColor);
    }

    public Vector3 GetRandomPoint()
    {
        Vector3 half = size * 0.5f;
        float x = Random.Range(-half.x, half.x);
        float z = Random.Range(-half.z, half.z);
        return transform.position + new Vector3(x, 0, z);
    }
}
