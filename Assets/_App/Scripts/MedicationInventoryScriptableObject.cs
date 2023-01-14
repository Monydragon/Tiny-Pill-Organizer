using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MedicationInventory", menuName = "Medication/MedicationInventory", order = 1)]
public class MedicationInventoryScriptableObject : ScriptableObject
{
    public string UserId
    {
        get => _userId;
        set => _userId = value;
    }

    public List<MedicationStock> MedicationStockList = new List<MedicationStock>();
    [SerializeField]
    private string _userId;

    public void AddMedication(Medication medication, int quantity)
    {
        MedicationStock newStock = new MedicationStock();
        newStock.medication = medication;
        newStock.quantity = quantity;
        MedicationStockList.Add(newStock);
    }
    public void RemoveMedication(Medication medication)
    {
        for (int i = 0; i < MedicationStockList.Count; i++)
        {
            if (MedicationStockList[i].medication == medication)
            {
                MedicationStockList.RemoveAt(i);
                break;
            }
        }
    }
    public int GetMedicationQuantity(Medication medication)
    {
        for (int i = 0; i < MedicationStockList.Count; i++)
        {
            if (MedicationStockList[i].medication == medication)
            {
                return MedicationStockList[i].quantity;
            }
        }
        return 0;
    }
}