namespace UI;

public class CreateUser : IMenu
{
    private IBL _bl;

    public CreateUser(IBL bl)
    {
        _bl = bl;
    }

    public void Start()
    {
        bool exit = false;
        do
        {
            Console.WriteLine("Create account screen");
            Console.WriteLine("1. Create account\n2. Return to HomePage");
            switch(Console.ReadLine())
            {
                case "1":
                
                bool createUsernameLoop = false;
                    while (!createUsernameLoop)
                    {
                        Console.Write("Create Username: ");
                        String? createUsername = Console.ReadLine();  
                        String regUsername = @"^[a-zA-Z0-9]{5,12}$";              
                        if (!Regex.IsMatch(createUsername, regUsername))
                        {
                            Console.WriteLine("Input a valid username only A - Z and 0 - 9 characters allowed. Name must be between 5 and 12 characters.");
                            continue;
                        }
                        bool createPasswordLoop = false;
                        while(!createPasswordLoop)
                        {
                            Console.Write("Create Password: ");
                            String createPassword = Console.ReadLine();
                            String regPassword = @"^[a-zA-Z0-9@!?]{5,12}$";
                            if (!Regex.IsMatch(createPassword, regPassword))
                            {
                                Console.WriteLine("Input a valid username only A - Z, 0 - 9, and !?@ characters allowed. Name must be between 5 and 12 characters.");
                                continue;
                            }                            
                            
                            Customer newCustomer = new Customer
                            {
                                
                                Username = createUsername,
                                Password = createPassword
                            };
                            _bl.AddCustomer(newCustomer);
                            Console.WriteLine("Your account has been sucessfully created");
                            createPasswordLoop = true;
                            createUsernameLoop = true;
                        }
                    }
                break;
                case "2":
                exit = true;
                break;
                default:
                Console.WriteLine("I dont understand your input");
                break;
            }
        }
        while(!exit);      
    }
}