using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

[System.Serializable]
public class User
{
    public string email;
    public string firstName;
    public string lastName;
    public DateTime dateOfBirth;
    public string password;
    public List<MedicationStock> medicationInventory;
    public static event Action<User> OnUserCreated;
    public static event Action<User> OnUserLogin;

    public User(string email, string firstName, string lastName, string dateOfBirth, string password, List<MedicationStock> medicationInventory)
    {
        this.email = email;
        this.firstName = firstName;
        this.lastName = lastName;
        this.dateOfBirth = DateTime.Parse(dateOfBirth);
        this.password = password;
        this.medicationInventory = medicationInventory;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            throw new System.ArgumentException("Username and password are required");
        }
    }

    public static void UserLogin(User user)
    {
        OnUserLogin?.Invoke(user);
    }
    
    public static void UserCreated(User user)
    {
        OnUserCreated?.Invoke(user);
    }
    
}