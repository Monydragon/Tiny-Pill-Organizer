using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class MainMenu : MonoBehaviour
{
    public MedicationInventoryScriptableObject MedicationInventory;
    public TMP_InputField EmailInput;
    public TMP_InputField PasswordInput;
    public GameObject MessageText;
    public List<Credentials> Users;
    public string UsersFileName = "Users.json";

    private void Start()
    {   
        //Load Users from a a json file located in persistent storage using the UsersFileName property as the filename
        Users = new List<Credentials>();
        var filePath = Path.Combine(Application.persistentDataPath, UsersFileName);
        if (File.Exists(filePath))
            Users = JsonConvert.DeserializeObject<List<Credentials>>(File.ReadAllText(filePath));

        if (Users.Count == 0)
        {
            Debug.Log("No users found in file " + UsersFileName);
            return;
        }

    }

    public void CreateAccount()
    {
        string email = EmailInput.text;
        string password = PasswordInput.text;

        if (!IsValidEmail(email))
        {
            ShowMessage("Invalid email format. Please try again.",Color.red);
            return;
        }

        if (Users.Exists(x=> x.username == email))
        {
            ShowMessage("Email already in use. Please try again.",Color.red);
            return;
        }

        Users.Add(new Credentials(email, password));
        MedicationInventory.UserId = email;
        ShowMessage("Account Created!", Color.green);
        SaveUsers();
    }
    public void SaveUsers()
    {
        var filePath = Path.Combine(Application.persistentDataPath, UsersFileName);
        File.WriteAllText(filePath, JsonConvert.SerializeObject(Users));
        Debug.Log("Users saved to " + filePath);
    }

    public void Login()
    {
        string email = EmailInput.text;
        string password = PasswordInput.text;
        if (!IsValidEmail(email))
        {
            ShowMessage("Invalid email format. Please try again.", Color.red);
            return;
        }
        var user = Users.Find(x => x.username == email);
        if (user == null)
        {
            ShowMessage("Email not found. Please try again.", Color.red);
            return;
        }
        else
        {
            if (user.password != password)
            {
                ShowMessage("Incorrect password. Please try again.", Color.red);
                return;
                
            }
        }
        MedicationInventory.UserId = email;
        ShowMessage("Logged In!", Color.green);
        // Load the main menu or navigate to the next scene
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private void ShowMessage(string message, Color color)
    {
        MessageText.SetActive(true);
        MessageText.GetComponent<TMP_Text>().color = color;
        MessageText.GetComponent<TMP_Text>().text = message;
    }
}
