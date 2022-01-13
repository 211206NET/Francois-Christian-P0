namespace Models;

public class Customer
{
    public int ID {get;set;}
    public String Username {get;set;}
    public String Password {get;set;}
    public List<Order> Orders {get;set;}
    
    public void ToDataRow(ref DataRow row)
    {
        row["Username"] = this.Username;
        row["Passcode"] = this.Password;
    }
}