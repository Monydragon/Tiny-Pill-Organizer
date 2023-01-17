using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicationListUI : MonoBehaviour
{
    public MedicationInventoryScriptableObject medicationInventory;
    public GameObject medicationUiDisplayPanelPrefab;
    public Transform medicationUiDisplayPanelRoot;
    
    // Start is called before the first frame update
    void Start()
    {
        if (medicationInventory != null)
        {
            for (int i = 0; i < medicationUiDisplayPanelRoot.childCount; i++)
            {
                Destroy(medicationUiDisplayPanelRoot.GetChild(i).gameObject);
            }
            for (int i = 0; i < medicationInventory.MedicationStockList.Count; i++)
            {
                GameObject go = Instantiate(medicationUiDisplayPanelPrefab, medicationUiDisplayPanelRoot);
                go.transform.SetParent(medicationUiDisplayPanelRoot.transform);
                var medicationUiDisplayScript = go.GetComponent<MedicationUIDisplay>();
                medicationUiDisplayScript.medication = medicationInventory.MedicationStockList[i].medication;
                medicationUiDisplayScript.UpdateDisplay();
            }
                
        }
    }
}
