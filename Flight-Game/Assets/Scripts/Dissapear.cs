using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissapear : MonoBehaviour, IPooledObject
{
    public bool colDestroy;
    public AudioClip[] sound;
    public float volume = 1f;
    AudioSource source;
    public string effect;
    public GameObject effectObj;

    public float DissapearTime;

    public bool trail;
    public bool particleTrail;
    public GameObject trailParticles;

    public string bulletHole;

    LayerMask layerMask;

    public bool shake;
    public float screenShakeMag, screenShakeDur;

    public bool pooled;

    void Start()
    {
        if (!pooled) OnObjectSpawn();
    }

    // Start is called before the first frame update
    public void OnObjectSpawn()
    {
        Invoke(nameof(Remove), DissapearTime);

        if (sound.Length != 0)
        {
            gameObject.AddComponent(typeof(AudioSource));
            source = GetComponent<AudioSource>();
            source.volume = volume;
            source.spatialBlend = 0.5f;

            //int i = 0;
            source.PlayOneShot(sound[Random.Range(0, sound.Length)]);
        }
        if (trail)
        {
            trailParticles = Instantiate(trailParticles, transform.position, transform.rotation);
        }

        for (int i = 0; i < 32; i++)
        {
            if (!Physics.GetIgnoreLayerCollision(gameObject.layer, i))
            {
                layerMask |= 1 << i;
            }
        }
        if(shake) StartCoroutine(Camera.main.GetComponent<CamShake>().Shake(screenShakeDur, screenShakeMag));
    }

    private void OnTriggerEnter(Collider col)
    {
        if(colDestroy)
        {
            Remove();
            if(effect != "") ObjectPooler.instance.SpawnFromPool(effect, transform.position, transform.rotation);
            if(bulletHole != "")
            {
                if(col.gameObject.CompareTag("Ground"))
                {
                    if (Physics.Raycast(transform.position - 1f * transform.forward, transform.forward, out RaycastHit hit, Mathf.Infinity, layerMask))
                    {
                        ObjectPooler.instance.SpawnFromPool(bulletHole, hit.point, Quaternion.LookRotation(hit.normal));
                    }
                }
            }
        }
    }

    private void Update()
    {
        if(trail)
        {
            trailParticles.transform.position = transform.position;
            trailParticles.transform.rotation = transform.rotation;
        }
        if (source != null) source.pitch = Time.timeScale;
    }

    void Remove()
    {
        if (!pooled) Destroy(gameObject);
        else gameObject.SetActive(false);

        if (effectObj != null) Instantiate(effectObj, transform.position, transform.rotation);
    }
}
