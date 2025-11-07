using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class PlaySoundOnHit : MonoBehaviour
{
	private InputDevice rightControllerDevice;
	private InputDevice leftControllerDevice;
	private AudioSource audioSource;
	
    [SerializeField] public AudioClip      hitSound;
    [SerializeField] public ParticleSystem hitEffectPrefab;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
	
    void OnCollisionEnter(Collision collision)
    {
		// ...
		if (!rightControllerDevice.isValid || !leftControllerDevice.isValid)
		{
            rightControllerDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
			leftControllerDevice  = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
		}

		// ...
		Vector3 hitVelocity = Vector3.zero;
        if (collision.gameObject.CompareTag("RightController"))
		{
			rightControllerDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out hitVelocity);
		}
		else if (collision.gameObject.CompareTag("LeftController"))
		{
			leftControllerDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out hitVelocity);
		}
		
		// ...
		if (hitVelocity.magnitude > 1.5f)
		{
			ContactPoint contact = collision.contacts[0];
			
			audioSource.PlayOneShot(hitSound, 1.0f);

            ParticleSystem effect = Instantiate(
                hitEffectPrefab,
                contact.point,
                Quaternion.LookRotation(contact.normal)
            );

			// Particle: create and destroy after
            effect.Play();
            Destroy(effect.gameObject, 2.0f);
		}
	}
}
