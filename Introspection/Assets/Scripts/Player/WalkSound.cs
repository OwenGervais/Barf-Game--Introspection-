using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class WalkSound : MonoBehaviour
{
    private new Collider collider;
    private float distToGround;
    private bool walking;
    [SerializeField] private float stepTime;

    [Header("Sounds")]
    private AudioSource audioSource;
    private AudioClip currentSound;
    [SerializeField] private AudioClip[] defaultWalk;
    [SerializeField] private AudioClip[] glassWalk;

    private Coroutine footstepCoroutine;


    private void Awake()
    {
        collider = GetComponent<Collider>();
        distToGround = collider.bounds.extents.y;

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        Vector2 movement = new Vector2(horizontal, vertical);

        bool wasWalking = walking;
        walking = movement != Vector2.zero;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, distToGround + 0.1f))
        {
            if (hit.transform.gameObject.CompareTag("Glass"))
            {
                currentSound = glassWalk[Random.Range(0,defaultWalk.Length)];
            }
            else
            {
                currentSound = defaultWalk[Random.Range(0,defaultWalk.Length)];
            }
        }

        if (walking && !wasWalking)
        {
            if (footstepCoroutine == null)
            {
                footstepCoroutine = StartCoroutine(RepeatSound(currentSound));
            }
        }
        else if (!walking && wasWalking)
        {
            if (footstepCoroutine != null)
            {
                StopCoroutine(footstepCoroutine);
                footstepCoroutine = null;
            }
        }
    }

    private IEnumerator RepeatSound(AudioClip sound)
    {
        while (walking)
        {
            audioSource.PlayOneShot(currentSound);
            yield return new WaitForSeconds(stepTime);
        }
    }
}
