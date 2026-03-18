using System;
using Unity.Netcode;
using UnityEngine;

public class ControllwerPlayerNetcode : NetworkBehaviour
{
    [SerializeField] private float _speed = 5f;
    
    private void Update()
    {
        if(!IsOwner) return;
        
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var inputDirection = new Vector3(horizontal, 0, vertical);

        if (inputDirection.magnitude > 0f)
            MoveServerRpc(inputDirection);
    }

    [ServerRpc]
    private void MoveServerRpc(Vector3 inputDirection)
    {
        transform.position += inputDirection * _speed * Time.deltaTime;
    }
}