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

        public void Start()
        {
            player = GetComponent<PlayerBehavior>();
            mainCamera = player.CameraHandler.transform.GetChild(0).gameObject.GetComponent<Camera>();
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

            if (Input.GetMouseButton(0))
                player.Fire();

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (new Plane(Vector3.up, transform.position).Raycast(ray, out float hitDistance))
            {
                player.LookAt(ray.GetPoint(hitDistance));
            }

            if (Input.GetKey(KeyCode.E))
                player.ShiftToHealth();

            if (Input.GetKey(KeyCode.Q))
                player.ShiftToFuel();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(mainCamera.ScreenPointToRay(Input.mousePosition).origin,
                mainCamera.ScreenPointToRay(Input.mousePosition).origin + mainCamera.ScreenPointToRay(Input.mousePosition).direction);
        }
    }
}
