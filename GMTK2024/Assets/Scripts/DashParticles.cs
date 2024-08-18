using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerMovement))]
public class DashParticles : MonoBehaviour
{
    private PlayerMovement PlayerMovement;
    private void Start()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerMovement.ListenToDash(OnDash);
    }
    private void OnDash()
    {

    }
    void Update()
    {

    }
}