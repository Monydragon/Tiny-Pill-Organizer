using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    // Singleton instance of the GameManager
    private static AppManager instance;

    // List of all users
    public List<User> users;
    // Reference to the current user
    public User currentUser;

    public Medication currentMedication;

    // MedicationInventoryScriptableObject for managing medications
    public MedicationInventoryScriptableObject MedicationInventory;
    public string usersFileName = "Users.json";
    
    public static AppManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            users = new List<User>();
            var filePath = Path.Combine(Application.persistentDataPath, usersFileName);
            if (File.Exists(filePath))
                users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(filePath));

            if (users.Count == 0)
            {
                Debug.Log("No users found in file " + usersFileName);
                return;
            }
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void OnEnable()
    {
        User.OnUserLogin += UserOnOnUserLogin; 
        User.OnUserCreated += UserOnOnUserCreated;
        MedicationInventoryScriptableObject.OnAddMedication += SaveOnMedicationChange;
        MedicationInventoryScriptableObject.OnRemoveMedication += SaveOnMedicationChange;
        MedicationInventoryScriptableObject.OnUpdateMedication += SaveOnMedicationChange;
    }

    private void OnDisable()
    {
        User.OnUserLogin -= UserOnOnUserLogin; 
        User.OnUserCreated -= UserOnOnUserCreated;
        MedicationInventoryScriptableObject.OnAddMedication -= SaveOnMedicationChange;
        MedicationInventoryScriptableObject.OnRemoveMedication -= SaveOnMedicationChange;
        MedicationInventoryScriptableObject.OnUpdateMedication -= SaveOnMedicationChange;
    }

    public void AddMedication(Medication medication, int quantity)
    {
        MedicationInventory.AddMedication(medication, quantity);
    }

    public void RemoveMedication(Medication medication)
    {
        MedicationInventory.RemoveMedication(medication);
    }

    public void UpdateMedication(Medication medication, int quantity)
    {
        MedicationInventory.UpdateMedication(medication, quantity);
    }
    
    public void SaveUsers()
    {
        var filePath = Path.Combine(Application.persistentDataPath, usersFileName);
        File.WriteAllText(filePath, JsonConvert.SerializeObject(users, Formatting.Indented));
        Debug.Log("Users saved to " + filePath);
    }
    
    private void UserOnOnUserCreated(User user)
    {
        users.Add(user);
        currentUser = user;
        SaveUsers();
    }

    private void UserOnOnUserLogin(User user)
    {
        currentUser = user;
    }
    
    private void SaveOnMedicationChange(Medication medication)
    {
        SaveUsers();
    }
}