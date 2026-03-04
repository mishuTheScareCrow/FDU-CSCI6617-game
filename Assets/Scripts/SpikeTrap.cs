using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private enum State { Lowered, Lowering, Raising, Raised }
    private State state = State.Lowered;

    private const float SpikeHeight = 3.6f;
    private const float LoweredSpikeHeight = .08f;

    [Header("References")]
    public Transform spikeHolder;
    public GameObject hitboxGameObject;
    public GameObject colliderGameObject;

    [Header("Timing")]
    public float interval = 2f;
    public float raiseWaitTime = 1f;
    public float lowerTime = 1f;
    public float raiseTime = .25f;

    private float lastSwitchTime;

    void Start()
    {
        // spikes start lowered; raise after interval
        Invoke(nameof(StartRaising), interval);
    }

    void Update()
    {
        if (state == State.Lowering)
        {
            Vector3 scale = spikeHolder.localScale;
            scale.y = Mathf.Lerp(SpikeHeight, LoweredSpikeHeight, (Time.time - lastSwitchTime) / lowerTime);
            spikeHolder.localScale = scale;

            if (scale.y == LoweredSpikeHeight)
            {
                Invoke(nameof(StartRaising), interval);
                state = State.Lowered;
                colliderGameObject.SetActive(false);
            }
        }
        else if (state == State.Raising)
        {
            Vector3 scale = spikeHolder.localScale;
            scale.y = Mathf.Lerp(LoweredSpikeHeight, SpikeHeight, (Time.time - lastSwitchTime) / raiseTime);
            spikeHolder.localScale = scale;

            if (scale.y == SpikeHeight)
            {
                Invoke(nameof(StartLowering), raiseWaitTime);
                state = State.Raised;

                // now blocks, but no longer kills
                colliderGameObject.SetActive(true);
                hitboxGameObject.SetActive(false);
            }
        }
    }

    void StartRaising()
    {
        lastSwitchTime = Time.time;
        state = State.Raising;

        // kills during raising
        hitboxGameObject.SetActive(true);
        colliderGameObject.SetActive(false);
    }

    void StartLowering()
    {
        lastSwitchTime = Time.time;
        state = State.Lowering;

        // blocks during lowering
        colliderGameObject.SetActive(true);
        hitboxGameObject.SetActive(false);
    }
}
