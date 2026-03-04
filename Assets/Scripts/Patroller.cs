using UnityEngine;

public class Patroller : MonoBehaviour
{
    public Transform[] points;
    public float speed = 3f;
    [SerializeField] private Color componentColor = new Color(1f, 0.55f, 0.1f);
    private int index = 0;

    private void Awake()
    {
        ComponentColorUtility.Apply(gameObject, componentColor);
    }

    void Update()
    {
        if (points == null || points.Length == 0) return;

        Transform target = points[index];
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
            index = (index + 1) % points.Length;
    }
}
