
namespace BL;

public class RRBL : IBL
{
    private IRepo _dl;
    public RRBL(IRepo repo)
    {
        _dl = repo;
    }

    public List<Customer> GetCustomers()
    { 
        return _dl.GetCustomers();
    }

    public void AddCustomer(Customer newCustomer)
    {
        _dl.AddCustomer(newCustomer);
    }
    public void updateCustomer(List<Customer> allCustomers)
    {
        _dl.updateCustomer(allCustomers);
    }

    public List<Admin> GetAdmin()
    { 
        return _dl.GetAdmin();
    }

    public void AddAdmin(Admin newAdmin)
    {
        _dl.AddAdmin(newAdmin);
    }

    public List<StoreFront> GetStoreFronts()
    {

        return _dl.GetStoreFronts();
    }

    public void addStoreFront(StoreFront newStore)
    {
        _dl.addStoreFront(newStore);
    }

    public void removeStoreFront(List<StoreFront> allStores, StoreFront exStore)
    {
        _dl.removeStoreFront(allStores, exStore);
    }

    public void updateStoreFront(List<StoreFront> allStores)
    {
        _dl.updateStoreFront(allStores);
    }

public List<Product> GetProducts()
    {
        return _dl.GetProducts();
    }

    public void addProducts(Product newProduct)
    {
        _dl.addProducts(newProduct);
    }
    public void removeProducts(List<Product> allProducts, Product exProduct)
    {        
        _dl.removeProducts(allProducts, exProduct);
    }
    public List<Inventory> GetInventories()
    {
        return _dl.GetInventories();
    }

    public void addInventory(int StoreID, int ProductID, Inventory newInventory)
    {
        _dl.addInventory(StoreID, ProductID, newInventory);
    }

    public void removeInventory(List<Inventory> allInventory, Inventory exInventory)
    {
        _dl.removeInventory(allInventory, exInventory);
    }

    public void updateInventory(int quantity, int inventoryID, Inventory updateInventory)
    {       
        _dl.updateInventory(quantity, inventoryID, updateInventory);
    }
    
    public List<LineItem> getLineItem()
    {       
        return _dl.getLineItem();    
    }

    public void addLineItem(int OrderID, int ProductID, LineItem newLineItem)
    {
        _dl.addLineItem(OrderID, ProductID, newLineItem);
    }

    public void removeLineItem(List<LineItem> removeLineItem, LineItem exLineItem)
    {
        _dl.removeLineItem(removeLineItem, exLineItem);
    }

public void clearLineItem(List<LineItem> clearLineItem)
    {
        _dl.clearLineItem(clearLineItem);
    }
    public List<Order> getOrders()
    {
        return _dl.getOrders();
    }
    
    public void addOrder(int CustomerID, int StoreID, Order newOrder)
    {
        _dl.addOrder(CustomerID, StoreID, newOrder);
    }
    public void updateOrder(decimal totalPlus, int orderID, Order updateOrder)
    {
        _dl.updateOrder(totalPlus, orderID, updateOrder);
    }
}
