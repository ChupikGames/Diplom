using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float camPosY;
    [SerializeField] private float camPosZ;
    private Vector3 camDistance; //Расстояние между игроком и камерой
    private Animator cam_anim;

    void Start()
    {
        camDistance = transform.position - player.position;
        cam_anim = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        Vector3 newPosition = new Vector3(player.position.x, player.position.y + camPosY, camDistance.z + player.position.z - camPosZ);
        transform.position = newPosition;

    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        cam_anim.enabled = false;
    }
    public void CamRotation()
    {
        cam_anim.Play("Camera_anim");
        StartCoroutine(Wait());
        
        //cam_anim.Play("Camer_anim");
    }
}
