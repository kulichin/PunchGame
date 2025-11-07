using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class SpringMesh : MonoBehaviour
{
	[SerializeField] public GameObject Head;
	[SerializeField] public Rigidbody  HeadRigidBody;
	
    public SkinnedMeshRenderer skinnedMesh;  // assign in Inspector
    public string boneName = "Bone.002";   // name of the bone to move

    Transform targetBone;

    void Start()
    {
		// Find the bone by name
        foreach (var bone in skinnedMesh.bones)
        {
            if (bone.name.Contains("002"))
            {
                targetBone = bone;
                break;
            }
        }
    }

    void Update()
    {
        if (targetBone != null)
        {
			Debug.Log(targetBone.name);
			
            // Example: simple rotation
            targetBone.position = Head.transform.position;
        }
    }
}
