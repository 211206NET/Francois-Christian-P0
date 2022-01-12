namespace BL;

public interface IBL
{
    List<Customer> GetCustomers();
    void AddCustomer(Customer newCustomer);
    void updateCustomer(List<Customer> allCustomers);
    List<Admin> GetAdmin();
    void AddAdmin(Admin newAdmin);
    List<StoreFront> GetStoreFronts();
    void addStoreFront(StoreFront newStore);
    void removeStoreFront(List<StoreFront> allStores, StoreFront exStore);
    void updateStoreFront(List<StoreFront> allStores);
    List<Product> GetProducts();
    void addProducts(Product newProduct);
    void removeProducts(List<Product> allProducts, Product exProduct);
    List<Inventory> GetInventories();
    void addInventory(int StoreID, int ProductID, Inventory newInventory);
    void removeInventory(List<Inventory> allInventory, Inventory exInventory);
    void updateInventory(int quantity, int inventoryID, Inventory updateInventory);
    List<LineItem> getLineItem();
    void addLineItem(int OrderID, int ProductID, LineItem newLineItem);
    void removeLineItem(List<LineItem> removeLineItem, LineItem exLineItem);
    void clearLineItem(List<LineItem> clearLineItem);
    List<Order> getOrders();
    void addOrder(int CustomerID, int StoreID, Order newOrder);
    void updateOrder(decimal totalPlus, int orderID, Order updateOrder);
}