using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lomztein.PlaceholderName.Items;
using Lomztein.PlaceholderName.Characters;
using Lomztein.PlaceholderName.Characters.PhysicalEquipment;

namespace Lomztein.PlaceholderName.Player {

    public class PlayerController : MonoBehaviour {

        public Humanoid character;
        public Inventory inventory;

        public static ItemSlot itemInHand;

        public LayerMask groundLayer;

        private void Start() {
            character = GetComponent<Humanoid> ();
            itemInHand = ItemSlot.CreateSlot (null);
        }

        // Update is called once per frame
        void Update() {
            UpdateMovement ();
            if (Input.GetKeyDown (KeyCode.I)) {
                inventory.Display ();
            }

            if (Input.GetMouseButton (0)) {
                character.HoldTool (character.equipment.GetSlot (CharacterEquipment.Type.Tool).currentObject.GetComponent<Tool> ());
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

            if (Physics.Raycast (ray, out hitInfo, Mathf.Infinity, groundLayer)) {
                Vector3 mousePosition = hitInfo.point;
                character.Aim (mousePosition + Vector3.up, Time.deltaTime);
            }

            character.Move (moveDirection, Time.deltaTime);

        }
    }

}
