using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    Rigidbody rb;
    Vector3 initialPos;
    float input;
    bool touchedWall;
    ParticleSystem trailParticles, HsParticles;

    [SerializeField] float speed, force;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        trailParticles = transform.GetChild(0).GetComponent<ParticleSystem>();
        HsParticles = transform.GetChild(1).GetComponent<ParticleSystem>();
        initialPos = transform.position;
    }

    void FixedUpdate()
    {
        input = Input.GetAxis("Horizontal");

        if (input != 0 && !touchedWall)
        {
            rb.angularVelocity = Vector3.back * (input * speed);
            rb.AddForce(Vector3.right * force * input, ForceMode.Impulse);
        }
        else if (input != 0)
            rb.angularVelocity = Vector3.back * (input * speed/2);

        if (rb.velocity.y > 2)
            trailParticles.Play();
        else
            trailParticles.Stop();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ScoreTrigger"))
        {
            other.gameObject.SetActive(false);
            LevelManager.instance.IncrementScore();

        if (LevelManager.instance.Score == PlayerPrefs.GetInt("HS"))
                HsParticles.Play();
        }

        if (other.CompareTag("GameOver"))
            LevelManager.instance.Fail();
    }

    void OnCollisionStay(Collision other)
    {
        if (other.transform.CompareTag("Wall"))
            touchedWall = true;
        else
            touchedWall = false;
    }

    public void ResetPos()
    {
        rb.position = initialPos;
        rb.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true; rb.isKinematic = false;
    }
}