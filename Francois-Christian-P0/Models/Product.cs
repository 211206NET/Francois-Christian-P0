
namespace Models;


public class Product
{
    public int ProductID{get;set;}
    private string _name{get;set;}
    private string _description{get;set;} 
    public String Name
    {
        get => _name;
        set
        {          
            Regex pattern = new Regex("^[a-zA-Z0-9 ']+$");
            if (string.IsNullOrEmpty(value))
            {
                throw new InputInvalidException("Name cant be empty");
            }
            else if (!pattern.IsMatch(value))
            {
                throw new InputInvalidException("Product name can only have alphanumerical characters, white space, and '");
            }
            else 
            {
                this._name = value;
            }
        }
    }
    public String Description
    {
        get => _description;
        set
        {          
            Regex pattern = new Regex("^[a-zA-Z ']+$");
            if (string.IsNullOrEmpty(value))
            {
                throw new InputInvalidException("Description cant be empty");
            }
            else if (!pattern.IsMatch(value))
            {
                throw new InputInvalidException("Description can only have alphabet characters, white space, and '");
            }
            else 
            {
                this._description = value;
            }
        }
    }
    public decimal Price{get;set;}

    public override string ToString()
    {
        return $"Id: {this.ProductID} Name: {this.Name} Description: {this.Description} Price: {this.Price}";
    }
}
