using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    public float moveSpeed = 2f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(transform.position + Vector3.down * moveSpeed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        
        SoundManager.instance.EffectAudioSource.clip = SoundManager.instance.itemAudioClips[0];

        SoundManager.instance.EffectAudioSource.Play();
    }
}
