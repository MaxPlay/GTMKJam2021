using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GMTK2021.Gameplay
{
    [RequireComponent(typeof(PlayerBehavior))]
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerBehavior player;
        private Camera mainCamera;

        bool isUsingGamepad = false;
        Vector2 lastMouseLocation;

        public void Start()
        {
            player = GetComponent<PlayerBehavior>();
            mainCamera = Camera.main;
        }

        private void Update()
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            Vector2 moveSpeed = new Vector2(x, y);
            if (moveSpeed.magnitude > 0.125f)
            {
                player.Move(moveSpeed);
            }

            if (Input.GetMouseButton(0) || Input.GetAxis("Fire") > 0.5f)
                player.Fire();

            float aimX = Input.GetAxis("AimHorizontal");
            float aimY = Input.GetAxis("AimVertical");
            Vector3 aimDirection = new Vector3(aimX, 0, aimY);
            isUsingGamepad = (lastMouseLocation - (Vector2)Input.mousePosition).magnitude < 1;
            if (aimDirection.magnitude <= 0.1f && !isUsingGamepad)
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (new Plane(Vector3.up, Vector3.zero).Raycast(ray, out float hitDistance))
                {
                    player.LookAt(ray.GetPoint(hitDistance));
                }
            }
            else
            {
                player.LookAt(transform.position + aimDirection);
            }
            lastMouseLocation = Input.mousePosition;
        }
    }
}
