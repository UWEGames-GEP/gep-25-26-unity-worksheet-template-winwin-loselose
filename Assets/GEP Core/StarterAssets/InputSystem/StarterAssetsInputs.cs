using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
        public bool addItem;
        public bool removeItem;
		public bool pause;
		public bool _return;
		public bool inventory;

        [Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

        public void OnAddItem(InputValue value)
        {
            AddItemInput(value.isPressed);
        }
        public void OnRemoveItem(InputValue value)
        {
            RemoveItemInput(value.isPressed);
        }
        public void OnPause(InputValue value)
        {
            PauseInput(value.isPressed);
        }
        public void OnReturn(InputValue value)
        {
            ReturnInput(value.isPressed);
        }
        public void OnInventory(InputValue value)
        {
            InventoryInput(value.isPressed);
        }

        public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void AddItemInput(bool newAddItemState)
		{
			addItem = newAddItemState;
		}
        public void RemoveItemInput(bool newRemoveItemState)
        {
            removeItem = newRemoveItemState;
        }

        public void PauseInput(bool newPauseState)
        {
            pause = newPauseState;
        }
        public void ReturnInput(bool newReturnState)
        {
            _return = newReturnState;
        }
        public void InventoryInput(bool newInventoryState)
        {
            inventory = newInventoryState;
        }

        public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			//SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			//Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}