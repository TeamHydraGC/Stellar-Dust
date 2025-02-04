using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] private float grappleLength;
    [SerializeField] private LayerMask grappleLayer;
    [SerializeField] private LineRenderer rope;
    public AudioSource audioSource;
    public AudioClip grappleSound;

    private Vector3 grapplePoint;
    private DistanceJoint2D joint;
    private bool isGrappleTargetInRange;

    void Start()
    {
        joint = gameObject.GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        rope.enabled = false;
    }

    void Update()
    {
        if (isGrappleTargetInRange && Input.GetMouseButtonDown(1))
        {
            ActivateGrapple();
        }

        if (Input.GetMouseButtonUp(1))
        {
            DeactivateGrapple();
        }

        if (rope.enabled)
        {
            rope.SetPosition(1, transform.position);
        }
    }

    public void ActivateGrapple()
    {
        joint.connectedAnchor = grapplePoint;
        joint.enabled = true;
        joint.distance = grappleLength;
        rope.SetPosition(0, grapplePoint);
        rope.SetPosition(1, transform.position);
        rope.enabled = true;
        audioSource.PlayOneShot(grappleSound);
    }

    public void DeactivateGrapple()
    {
        joint.enabled = false;
        rope.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & grappleLayer) != 0)
        {
            grapplePoint = other.transform.position;
            grapplePoint.z = 0;
            isGrappleTargetInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & grappleLayer) != 0)
        {
            isGrappleTargetInRange = false;
        }
    }
}
