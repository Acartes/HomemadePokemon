using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterInteractionState {
    ON_THE_MAP,
}
public class MapInteractionEvent : MonoBehaviour {
    public InGameEntity Character;
    public InteractableItems interactableItems;

    public CharacterInteractionState characterInteractionState;
    public DialogEvent dialogEvent;

    // Update is called once per frame
    public void Action (Action action) {
        switch (characterInteractionState) {
            case CharacterInteractionState.ON_THE_MAP:
                CharacterMapAction ();
                break;

        }
    }

    public void CharacterMapAction () {
        if (characterInteractionState == CharacterInteractionState.ON_THE_MAP) {
            InteractWithEntity(GetFacedEntity());
        }
    }

    public InGameEntity GetFacedEntity () {
        Vector3 characterFacedObject_Position = Character.transform.position;
        switch (Character.facingDirection) {
            case Direction.UP:
                characterFacedObject_Position += new Vector3 (0, 1, 0);
                break;
            case Direction.DOWN:
                characterFacedObject_Position += new Vector3 (0, -1, 0);
                break;
            case Direction.RIGHT:
                characterFacedObject_Position += new Vector3 (1, 0, 0);
                break;
            case Direction.LEFT:
                characterFacedObject_Position += new Vector3 (-1, 0, 0);
                break;
        }
        InGameEntity entity = interactableItems.interactableEntities.Find (x => x.transform.position == characterFacedObject_Position);
        return entity;
    }

    public void InteractWithEntity(InGameEntity interactableEntity){
        if(interactableEntity == null){
            Debug.Log("No interactable entity found");
        }
        if(interactableEntity){
            if(dialogEvent.TryDialogInteraction(interactableEntity) ||  dialogEvent.TryContinueDialog() || dialogEvent.TryAccelerateDialog()){
                Player_Gamestate.player_gameState = GAMESTATE.DIALOG;
            }
            else
                Player_Gamestate.player_gameState = GAMESTATE.WORLDMAP;
        }
    }
}