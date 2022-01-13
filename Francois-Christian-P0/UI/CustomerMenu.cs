namespace UI;

public class CustomerMenu : IMenu
{
    private IBL _bl;
    public CustomerMenu(IBL bl)
    {
        _bl = bl;
    }
    public void Start()
    {
        bool exit = false;
        do 
        {
            Console.WriteLine("Customer menu homescreen");
            Console.WriteLine("1. Log in\n2. Return to HomePage");
            switch(Console.ReadLine())
            {
                case "1":
                bool usernameLoop = false;
                    while(!usernameLoop)  
                    {    
                        Console.Write("Username:");
                        String username = Console.ReadLine();
                        if(String.IsNullOrEmpty(username))
                        {
                            continue;
                        }
                        bool passwordLoop = false;
                        while(!passwordLoop)
                        {
                            Console.Write("Password:");
                            String password = Console.ReadLine();
                            if(String.IsNullOrEmpty(password))
                            {
                                continue;
                            }
                            Customer currentCustomer = new Customer();
                            List<Customer> CustomerList = _bl.GetCustomers();
                            foreach (Customer findCustomer in CustomerList)
                            {
                                if (findCustomer.Username == username && findCustomer.Password == password)
                                {
                                    currentCustomer = findCustomer;
                                    break;
                                }
                            }
                            if (currentCustomer.Username == null)
                            {
                                Console.WriteLine("User not found");
                                passwordLoop = true;
                                usernameLoop = true;
                                break;
                            }
                            CustomerStart(currentCustomer);
                            passwordLoop= true;
                            usernameLoop = true;
                        }
                    }
                break;
                case "2":
                exit = true;
                break;
                default:
                Console.WriteLine("I don't understand your input");
                break;
            }
        }
        while(!exit);
    }
    private void CustomerStart(Customer customer)
    {        
        //DateOnly date = new DateOnly();
        //date = DateOnly.FromDateTime(DateTime.Now);
        
        Console.WriteLine($"Welcome to PaintLocker {customer.Username}!\nHow can we help you today?");
        bool customerMenuLoop  = false;
        while(!customerMenuLoop)
        {
        Console.WriteLine("1. Select Store\n2. View orders\n3. Return to login");
        string customerPick = Console.ReadLine();
        switch(customerPick)
        {
            case "1":
            List<StoreFront> allStores = _bl.GetStoreFronts();
            foreach(StoreFront showStore in allStores)
            {
                Console.WriteLine($"StoreID: {showStore.StoreID} PaintLocker {showStore.Name}");
            }
            Console.Write("Select a StoreID: ");
            String sID = Console.ReadLine();
            int storeID;
            if(!int.TryParse(sID, out storeID))
            {
                Console.WriteLine("Enter an integer");
                continue;
            }            
            StoreFront currentStore = new StoreFront();
            foreach(StoreFront searchStore in allStores)
            {
                if(searchStore.StoreID == storeID)
                {
                    currentStore = searchStore;
                    break;
                }
            }
            if(currentStore.Name == null)
            {
                Console.WriteLine("Couldnt find store");
                continue;
            }
            Order newOrder = new Order
            {
                Total = 0
            };           
            _bl.addOrder(customer.ID, storeID, newOrder); 
            Order thisOrder = new Order();
            List<Order> allOrders = _bl.getOrders();
            foreach(Order searchOrder in allOrders)
            {
                if (searchOrder.CustomerID == customer.ID && searchOrder.StoreId == currentStore.StoreID)
                {
                    if(searchOrder.Total == 0)
                    {
                        thisOrder = searchOrder;
                        break;
                    }
                }
            }
            bool shopping = true;
            while(shopping)
            {
                Console.WriteLine("1. Add to shopping cart\n2. Remove from shopping cart\n3. Submit Order");
                String shoppingcart = Console.ReadLine();
                switch(shoppingcart)
                {
                    case "1":               
                        foreach(StoreFront showStore in allStores)
                        {
                            if(showStore == currentStore)
                            {
                                foreach(Inventory showInventory in showStore.Inventories)
                                {
                                    Console.WriteLine($"ProductID: {showInventory.InventoryID} Name: {showInventory.ProductName} Description: {showInventory.ProductDescription} Price: {showInventory.ProductPrice}");
                                }
                            }
                        }
                        Console.Write("Select a productID: ");
                        String pID = Console.ReadLine();
                        int productID;
                        if(!int.TryParse(pID, out productID))
                        {
                            Console.WriteLine("Enter an integer");
                            continue;
                        }  
                        Inventory currentInventory = new Inventory();
                        foreach(StoreFront showStore in allStores)
                        {
                            if(showStore == currentStore)
                            {
                                foreach(Inventory showInventory in showStore.Inventories)
                                {
                                    if(showInventory.InventoryID == productID)
                                    {
                                        currentInventory = showInventory;
                                        break;
                                    }
                                }
                            }
                        }
                        if (currentInventory.ProductName == null)
                        {
                            Console.WriteLine("Product not found");
                            continue;
                        }
                        Console.Write("Quantity: ");
                        String qID = Console.ReadLine();
                        int quantity;
                        if(!int.TryParse(qID, out quantity))
                        {
                            Console.WriteLine("Enter an integer");
                            continue;
                        }  
                        LineItem lineItem = new LineItem
                        {
                            Quantity = quantity,
                            ProductName = currentInventory.ProductName,
                            ProductDescription = currentInventory.ProductDescription,
                            ProductPrice = currentInventory.ProductPrice
                        };
                        _bl.addLineItem(thisOrder.OrderID, currentInventory.ProductID, lineItem);
                        Console.WriteLine($"{currentInventory.ProductName} added to shopping cart");                                               
                        List<Inventory> getInventory = _bl.GetInventories();                        
                        int Amount = 0;
                        foreach(Inventory searchInventory in getInventory)
                        {                            
                            if(searchInventory.InventoryID == currentInventory.InventoryID)
                            {   
                                Amount += (searchInventory.Quantity - quantity);                               
                            }
                        }                             
                        _bl.updateInventory(Amount, currentInventory.InventoryID, currentInventory);
                    break;
                    case "2":
                    break;
                    case "3":
                    Order changeOrder = new Order();
                    List<Order> getOrders = _bl.getOrders();
                    foreach(Order searchOrder in getOrders)
                    {
                        if (searchOrder.OrderID == thisOrder.OrderID)
                        {
                            changeOrder = searchOrder;
                            break;
                        }
                    }
                    int Quan = 0;
                    decimal Cost = 0;
                    decimal Tots = 0;
                    List<LineItem> getLineItem = _bl.getLineItem();
                    foreach(LineItem searchLine in getLineItem)
                    {
                        if (searchLine.OrderID == thisOrder.OrderID)
                        {
                            Quan = searchLine.Quantity;
                            Cost = searchLine.ProductPrice;
                            Tots += (decimal)Quan * Cost;
                        }
                    }                    
                    _bl.updateOrder(Tots, changeOrder.OrderID, changeOrder);
                    Console.WriteLine("Order Submitted");
                    
                    shopping = false;
                    break;
                    default:
                    break;
                }
            }
            break;
            case "2":
                bool viewingOrders = true;
                while(viewingOrders)
                {
                    List<Customer> allCustomer = _bl.GetCustomers();
                    List<Order> getOrders = _bl.getOrders();
                    Console.WriteLine("1. View orders\n2. View order by price - accending\n3. View order by price deccending\n4. View order by date\n5. Return to previous Screen");
                    String orderView = Console.ReadLine();
                    switch(orderView)
                    {
                        case "1": 
                            
                            foreach(Order showCustomerOrder in getOrders)
                            {
                                if(showCustomerOrder.CustomerID == customer.ID)
                                    Console.WriteLine($"OrderNumber: {showCustomerOrder.OrderID}");
                                    foreach(LineItem showLine in showCustomerOrder.LineItems)
                                    {
                                        Console.WriteLine($"{showLine.ProductName} {showLine.ProductPrice} X {showLine.Quantity}");
                                    }
                                    Console.WriteLine($"Total = {showCustomerOrder.Total}\n");                               
                            }
                            
                        
                        break;
                        case "2":
                        foreach(Order showCustomerOrder in getOrders.OrderBy(o => o.Total))
                            {
                                if(showCustomerOrder.CustomerID == customer.ID)
                                    Console.WriteLine($"OrderNumber: {showCustomerOrder.OrderID}");
                                    foreach(LineItem showLine in showCustomerOrder.LineItems)
                                    {
                                        Console.WriteLine($"{showLine.ProductName} {showLine.ProductPrice} X {showLine.Quantity}");
                                    }
                                    Console.WriteLine($"Total = {showCustomerOrder.Total}\n");                               
                            }
                        break;
                        case "3":
                        foreach(Order showCustomerOrder in getOrders.OrderByDescending(o => o.Total))
                            {
                                if(showCustomerOrder.CustomerID == customer.ID)
                                    Console.WriteLine($"OrderNumber: {showCustomerOrder.OrderID}");
                                    foreach(LineItem showLine in showCustomerOrder.LineItems)
                                    {
                                        Console.WriteLine($"{showLine.ProductName} {showLine.ProductPrice} X {showLine.Quantity}");
                                    }
                                    Console.WriteLine($"Total = {showCustomerOrder.Total}\n");                               
                            }
                        break;

                        case "4":
                        break;

                        case "5":
                        viewingOrders = false;
                        break;
                        default:
                        break;
                    }                
                }
            break;
        }
        }
    }
}