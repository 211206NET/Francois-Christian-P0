namespace UI;

public class AdminMenu : IMenu
{

private IBL _bl;

    public AdminMenu(IBL bl)
    {
        _bl = bl;
    }
    
    public void Start()
    {
        bool exit = false;
        do
        {
            Console.WriteLine("Admin menu homescreen");
            Console.WriteLine("1. Log in\n2. Return to HomePage");
            switch(Console.ReadLine())
            {
                case "1":
                bool adminUsernameLoop = false;
                while(!adminUsernameLoop)
                {
                    Console.Write("Username: ");
                    String adminUsername = Console.ReadLine();
                    if (String.IsNullOrEmpty(adminUsername))
                    {
                        continue;
                    }
                    bool adminPasswordLoop = false;
                    while(!adminPasswordLoop)
                    {
                        Console.Write("Password: ");
                        String adminPassword = Console.ReadLine();
                        if (String.IsNullOrEmpty(adminPassword))
                        {
                            continue;
                        }
                        Admin currentAdmin = new Admin();
                        List<Admin> AdminList = _bl.GetAdmin();
                        foreach(Admin findAdmin in AdminList)
                        {
                            if(findAdmin.Username == adminUsername && findAdmin.Password == adminPassword)
                            {
                                currentAdmin = findAdmin;
                                break;
                            }
                        }
                        if (currentAdmin.Username == null)
                        {
                            Console.WriteLine("User not found");
                            adminPasswordLoop = true;
                            break;
                        }                                                              
                        AdminStart(currentAdmin);
                        adminPasswordLoop = true;
                        adminUsernameLoop = true;                       
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
    private void AdminStart(Admin admin)
    {
        Console.WriteLine($"Welcome to Store {admin.Username}!");
        bool adminMenuLoop = false;
        while(!adminMenuLoop)
        {
            Console.WriteLine("What would you like to do today?\n1. Add Store\n2. Add Product\n3. Add Inventory\n4. View Orders\n5. Signout");
            String adminPick = Console.ReadLine();
            Console.WriteLine();
            switch(adminPick)
            {
                case "1":
                Console.Write("Store name: ");
                String storeName = Console.ReadLine();
                Console.Write("Address: ");
                String storeAddress = Console.ReadLine();                
                try
                {                   
                    StoreFront newStore = new StoreFront
                    {                               
                        Name = storeName,
                        Address = storeAddress                            
                    };
                    _bl.addStoreFront(newStore);  
                    Console.WriteLine($"PaintLocker {newStore.Name} created!");
                }
                catch (InputInvalidException ex)
                {
                    Console.WriteLine(ex.Message);                           
                }
                break;     
                
                case "2":
                Console.Write("Product name: ");
                String productName = Console.ReadLine();
                Console.WriteLine("Description: ");
                String description = Console.ReadLine();
                Console.WriteLine("Price: ");
                String cost = Console.ReadLine();
                decimal price;
                if(!decimal.TryParse(cost, out price))
                {
                    Console.WriteLine("Enter a number");
                }  
                
                try
                {
                Product newProduct = new Product
                {
                    Name = productName,
                    Description = description,
                    Price = price
                };
                _bl.addProducts(newProduct);
                Console.WriteLine($"{newProduct.Name} add to product list");
                }
                catch (InputInvalidException ex)
                {
                    Console.WriteLine(ex.Message);                           
                }
                break;
                case "3":
                List<StoreFront> allStoreFront = _bl.GetStoreFronts();
                foreach(StoreFront showStoreFront in allStoreFront)
                {
                    Console.WriteLine($"StoreID: {showStoreFront.StoreID} PaintLocker {showStoreFront.Name} Address: {showStoreFront.Address}");
                }
                Console.Write("StoreFrontID: ");
                String sID = Console.ReadLine();
                int storeID;
                if(!int.TryParse(sID, out storeID))
                {
                    Console.WriteLine("Enter an integer");
                    continue;
                }                
                List<Product> allProduct = _bl.GetProducts();
                foreach(Product showProduct in allProduct)
                {
                    Console.WriteLine($"ProductID: {showProduct.ProductID} ProductName: {showProduct.Name} Description: {showProduct.Description} Price: {showProduct.Price}\n");
                }
                Console.Write("ProductID: ");
                String pID = Console.ReadLine();
                int productID;
                if(!int.TryParse(pID, out productID))
                {
                    Console.WriteLine("Enter an integer");
                    continue;
                }                
                Product thisProduct = new Product();
                foreach(Product showProduct in allProduct)
                {
                    if(showProduct.ProductID == productID)
                    {
                        thisProduct = showProduct;
                        break;
                    }
                }
                
                Console.Write("Starting Inventory: ");
                int quantity = Convert.ToInt32(Console.ReadLine());
                
                Inventory newInventory = new Inventory
                {
                    Quantity = quantity,
                    ProductName =  thisProduct.Name,
                    ProductDescription = thisProduct.Description,
                    ProductPrice = thisProduct.Price
                };
                Inventory checkInventory = new Inventory();
                List<Inventory> allInventory = _bl.GetInventories();
                foreach(Inventory searchInventory in allInventory)
                {
                    if(searchInventory.ProductName == newInventory.ProductName && searchInventory.StoreID == newInventory.StoreID)
                    {
                        checkInventory = searchInventory;
                        break;
                    }
                }
                if (checkInventory.ProductName == null)
                {
                    Console.WriteLine($"This store already sells {newInventory.ProductName}");
                    continue;
                }
                _bl.addInventory(storeID, productID, newInventory);                
                Console.WriteLine($"{thisProduct.Name} added");
                break;
                case "4":
                bool adminView = false;
                while(!adminView)
                {
                    List<StoreFront> getStores = _bl.GetStoreFronts();
                    Console.WriteLine("1. View all orders\n2. View orders by customer\n3. View orders by store\n4. Return to previous menu");
                    String orderPick = Console.ReadLine();
                    switch(orderPick)
                    {
                        case "1":
                        foreach(StoreFront store in getStores)
                        {
                            
                        }
                        break;

                        case "2":
                        break;

                        case "3":
                        break;

                        case "4":
                        adminView = true;
                        break;

                        default:
                        break;
                    }
                }
                break;
                case "5":
                adminMenuLoop = true;
                break;
                default:
                break;
            }
        }
    }
}