using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lomztein.PlaceholderName.Items;

namespace Lomztein.PlaceholderName.Player {

    public class PlayerController : MonoBehaviour {

        public IControllable controllable;
        public Inventory inventory;

        public static ItemSlot itemInHand;

        private void Start() {
            controllable = GetComponent<IControllable> ();
            itemInHand = ItemSlot.CreateSlot (null);
        }

        // Update is called once per frame
        void Update() {
            UpdateMovement ();
            if (Input.GetKeyDown (KeyCode.I)) {
                inventory.Display ();
            }
        }

        private void UpdateMovement() {

            Vector3 moveDirection = new Vector3 () {
                x = Input.GetAxis ("Horizontal"),
                z = Input.GetAxis ("Vertical")
            };
            moveDirection.Normalize ();

            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast (ray, out hitInfo, Mathf.Infinity)) {
                Vector3 mousePosition = hitInfo.point;
                controllable.Aim (mousePosition, Time.deltaTime);
            }

            controllable.Move (moveDirection, Time.deltaTime);

        }
    }

}
