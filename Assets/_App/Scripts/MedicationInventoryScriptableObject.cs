using System;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MedicationInventory", menuName = "Medication/MedicationInventory", order = 1)]
public class MedicationInventoryScriptableObject : ScriptableObject
{
    public static event Action<Medication> OnAddMedication;
    public static event Action<Medication> OnRemoveMedication;
    public static event Action<Medication> OnUpdateMedication;
    
    public string UserId
    {
        get => _userId;
        set => _userId = value;
    }

    public List<MedicationStock> MedicationStockList = new List<MedicationStock>();
    [SerializeField]
    private string _userId;

    private void OnEnable()
    {
        User.OnUserCreated += UserOnOnUserCreated;
        User.OnUserLogin += UserOnOnUserLogin;
    }

    private void OnDisable()
    {
        User.OnUserCreated -= UserOnOnUserCreated;
        User.OnUserLogin -= UserOnOnUserLogin;
    }

    public void AddMedication(Medication medication, int quantity)
    {
        MedicationStock newStock = new MedicationStock();
        newStock.medication = medication;
        newStock.quantity = quantity;
        MedicationStockList.Add(newStock);
        OnAddMedication?.Invoke(medication);
    }
    public void RemoveMedication(Medication medication)
    {
        for (int i = 0; i < MedicationStockList.Count; i++)
        {
            if (MedicationStockList[i].medication == medication)
            {
                MedicationStockList.RemoveAt(i);
                OnRemoveMedication?.Invoke(medication);
                break;
            }
        }
    }
    public void UpdateMedication(Medication medication, int quantity)
    {
        MedicationStock medStock = MedicationStockList.Find(ms => ms.medication.Name == medication.Name);
        if (medStock != null)
        {
            medStock.quantity = quantity;
            medStock.medication = medication;
            OnUpdateMedication?.Invoke(medication);
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
    
    public void RemoveMedicationStock(Medication medication, int quantity)
    {
        MedicationStock medStock = MedicationStockList.Find(ms => ms.medication.Name == medication.Name);
        if (medStock != null)
        {
            medStock.quantity -= quantity;
            if (medStock.quantity <= 0)
            {
                MedicationStockList.Remove(medStock);
            }
        }
    }
    private void UserOnOnUserLogin(User user)
    {
        _userId = user.email;
        MedicationStockList = user.medicationInventory;
    }

    private void UserOnOnUserCreated(User user)
    {
        _userId = user.email;
    }
}