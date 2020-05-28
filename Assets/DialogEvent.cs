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

    public void TryDialogInteraction (InGameEntity entity) {
        EntityDialog entityHasDialog = entity.transform.GetComponent<EntityDialog> ();
        if (entityHasDialog != null && !dialogStarted) {
            LoadDialog (entityHasDialog);
            StartCoroutine (Dialog ());
        }
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
    public void TryContinueDialog () {
        if (canContinueDialog) {
            continueDialog = true;
        }
    }

    IEnumerator Dialog () {
        while (dialogsList.Count != 0) {
            dialogStarted = true;
            continueDialog = false;
            dialogLeft = dialogsList[0].dialog;
            dialogsList.RemoveAt (0);
            dialogDisplay.text = "";
            while (dialogLeft.Length != 0) {
                shownDialogSize++;
                dialogDisplay.text += dialogLeft[0];
                dialogLeft = dialogLeft.Remove (0, 1);
                yield return new WaitForSeconds (letterTimer);
                if (shownDialogSize >= maxDialogSize) {
                    canContinueDialog = true;
                    yield return new WaitUntil (() => continueDialog == true);
                    canContinueDialog = false;
                    continueDialog = false;
                    shownDialogSize = 0;
                    dialogDisplay.text = "";
                }
            }
            dialogStarted = false;
        }
    }
}