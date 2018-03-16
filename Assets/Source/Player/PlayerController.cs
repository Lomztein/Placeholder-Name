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

        public static ItemSlot itemInHand; // itemInHand and related functionality should perhaps be seperate from PlayerController, so inventories can be managed even with no player character.
        public static Transform itemModel;
        public LayerMask physicalItemLayer;

        public LayerMask groundLayer;
        public Vector3 mouseWorldPos;

        private void Start() {
            character = GetComponent<Humanoid> ();
            itemInHand = ItemSlot.CreateSlot (null);
            itemInHand.OnItemChanged += ItemInHand_OnItemChanged;
        }

        private void ItemInHand_OnItemChanged(ItemSlot itemSlot, Item oldItem, Item newItem) {
            if (itemModel)
                Destroy (itemModel.gameObject);
            if (newItem != null)
                itemModel = newItem.GetModelInstance ().transform;
        }

        // Update is called once per frame
        void Update() {
            UpdateMovement ();
            UpdateAim ();
            if (Input.GetKeyDown (KeyCode.I)) {
                inventory.Display ();
            }

            if (Input.GetMouseButton (0)) {
                character.HoldTool (character.equipment.GetSlot (CharacterEquipment.Type.Tool).currentObject.GetComponent<Tool> ());
            }

            if (itemModel) {
                itemModel.position = mouseWorldPos + Vector3.up + Vector3.up * Mathf.Sin (Time.time) * 0.2f;
                itemModel.Rotate (0f, 60f * Time.deltaTime, 0f);
            }
        }

        private void UpdateMovement() {

            Vector3 moveDirection = new Vector3 () {
                x = Input.GetAxis ("Horizontal"),
                z = Input.GetAxis ("Vertical")
            };
            moveDirection.Normalize ();
            character.Move (moveDirection, Time.deltaTime);

        }

        private void UpdateAim () {

            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast (ray, out hitInfo, Mathf.Infinity, groundLayer)) {
                mouseWorldPos = hitInfo.point;
                character.Aim (mouseWorldPos + Vector3.up, Time.deltaTime);
            }

            if (Input.GetMouseButtonDown (0)) {
                if (Physics.Raycast (ray, out hitInfo, Mathf.Infinity, physicalItemLayer)) {
                    if (itemInHand.item != null)
                        PhysicalItem.Create (itemInHand, itemModel.position, itemModel.rotation);
                    PhysicalItem item = hitInfo.collider.GetComponent<PhysicalItem> ();
                    item.slot.MoveItem (itemInHand);
                }
            }

        }
    }

}
