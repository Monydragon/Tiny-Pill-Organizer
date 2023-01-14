[System.Serializable]
public class Credentials
{
    public string username;
    public string password;

    public Credentials(string username, string password)
    {
        this.username = username;
        this.password = password;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            throw new System.ArgumentException("Username and password are required");
        }
    }
}