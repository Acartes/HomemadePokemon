using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialogEvent : MonoBehaviour {
    public List<DialogPrefab> dialogsList = new List<DialogPrefab> ();
    public Text dialogDisplay;
    public float letterTimer = 0.1f;
    public int maxDialogSize;
    int shownDialogSize;
    string dialogLeft = "";

    bool continueDialog;

    bool dialogStarted;

    public bool TryDialogInteraction (InGameEntity entity) {
        EntityDialog entityHasDialog = entity.transform.GetComponent<EntityDialog> ();
        if (entityHasDialog != null && !dialogStarted) {
            LoadDialog (entityHasDialog);
            StartCoroutine (Dialog ());
            return true;
        }
        return false;
    }

    public void LoadDialog (EntityDialog entityDialog) {
        dialogsList.Clear ();
        DialogPrefab nextDialog = entityDialog.startDialog;
        while (nextDialog != null) {
            dialogsList.Add (nextDialog);
            nextDialog = nextDialog.nextDialog;
        }
    }

    public bool canContinueDialog = false;
    public bool accelerateCurrentFrame = false;
    public bool completeCurrentFrame = false;
    public bool TryContinueDialog () {
        if (canContinueDialog) {
            continueDialog = true;
        } else
            continueDialog = false;
        return continueDialog;
    }

    public bool TryAccelerateDialog () {
        if (Player_Gamestate.player_gameState == GAMESTATE.DIALOG && accelerateCurrentFrame) {
            accelerateCurrentFrame = false;
            completeCurrentFrame = true;
            return true;
        } else if (Player_Gamestate.player_gameState == GAMESTATE.DIALOG) {
            if(!completeCurrentFrame)
                accelerateCurrentFrame = true;
            return true;
        }
        return false;
    }

    IEnumerator Dialog () {
        Player_Gamestate.player_gameState = GAMESTATE.DIALOG;
        while (dialogsList.Count != 0) {
            dialogStarted = true;
            continueDialog = false;
            dialogLeft = dialogsList[0].dialog;
            dialogsList.RemoveAt (0);
            dialogDisplay.text = "";
            accelerateCurrentFrame = false;
            completeCurrentFrame = false;
            while (dialogLeft.Length != 0) {
                shownDialogSize++;
                dialogDisplay.text += dialogLeft[0];
                dialogLeft = dialogLeft.Remove (0, 1);
                if (completeCurrentFrame) {
                    yield return new WaitForEndOfFrame ();
                } else if (accelerateCurrentFrame)
                    yield return new WaitForSeconds (letterTimer / 5);
                else
                    yield return new WaitForSeconds (letterTimer);

                if (shownDialogSize >= maxDialogSize) {
                    completeCurrentFrame = false;
                    accelerateCurrentFrame = false;
                    canContinueDialog = true;
                    yield return new WaitUntil (() => continueDialog == true);
                    canContinueDialog = false;
                    continueDialog = false;
                    shownDialogSize = 0;
                    dialogDisplay.text = "";
                }
            }
            dialogDisplay.text = "";
            dialogStarted = false;
        }

        Player_Gamestate.player_gameState = GAMESTATE.WORLDMAP;
    }
}