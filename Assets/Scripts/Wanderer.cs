using UnityEngine;

public class Wanderer : MonoBehaviour
{
    public float speed = 2f;
    public float reachDistance = 0.3f;
    [SerializeField] private Color componentColor = new Color(0.95f, 0.6f, 0.2f);

    private WanderRegion region;
    private Vector3 target;

    void Start()
    {
        ComponentColorUtility.Apply(gameObject, componentColor);
        region = GetComponentInParent<WanderRegion>();
        PickNewTarget();
    }

    void Update()
    {
        if (region == null) return;

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target) < reachDistance)
            PickNewTarget();
    }

    void PickNewTarget()
    {
        target = region.GetRandomPoint();
    }
}
