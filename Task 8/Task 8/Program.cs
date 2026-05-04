using System;
using System.Collections.Generic;

class Product
{
    public int Id;
    public string Name;
    public double Price;
    public int Quantity;
}

class Program
{
    static List<Product> products = new List<Product>();
    static List<Product> cart = new List<Product>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n===== MENU =====");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. View Products");
            Console.WriteLine("3. Search Product");
            Console.WriteLine("4. Add to Cart");
            Console.WriteLine("5. View Cart");
            Console.WriteLine("6. Checkout");
            Console.WriteLine("7. Exit");

            Console.Write("Choose: ");

            try
            {
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1: AddProduct(); break;
                    case 2: ViewProducts(); break;
                    case 3: SearchMenu(); break;
                    case 4: AddToCartMenu(); break;
                    case 5: ViewCartRecursive(0); break;
                    case 6: Checkout(); break;
                    case 7: return;
                    default: Console.WriteLine("Invalid choice"); break;
                }
            }
            catch
            {
                Console.WriteLine("Invalid input!");
            }
        }
    }

    // =========================
    // Add Product
    // =========================
    static void AddProduct()
    {
        try
        {
            Product p = new Product();

            Console.Write("Enter ID: ");
            p.Id = int.Parse(Console.ReadLine());

            Console.Write("Enter Name: ");
            p.Name = Console.ReadLine();

            Console.Write("Enter Price: ");
            p.Price = double.Parse(Console.ReadLine());

            Console.Write("Enter Quantity: ");
            p.Quantity = int.Parse(Console.ReadLine());

            products.Add(p);

            Console.WriteLine("Product added successfully!");
        }
        catch
        {
            Console.WriteLine("Error adding product!");
        }
    }

    // =========================
    // View Products
    // =========================
    static void ViewProducts()
    {
        if (products.Count == 0)
        {
            Console.WriteLine("No products available.");
            return;
        }

        foreach (var p in products)
        {
            Console.WriteLine($"ID: {p.Id}, Name: {p.Name}, Price: {p.Price}, Qty: {p.Quantity}");
        }
    }

    // =========================
    // Search Menu
    // =========================
    static void SearchMenu()
    {
        Console.WriteLine("1. Search by ID");
        Console.WriteLine("2. Search by Name");

        int choice = int.Parse(Console.ReadLine());

        if (choice == 1)
        {
            Console.Write("Enter ID: ");
            int id = int.Parse(Console.ReadLine());

            if (SearchProduct(id, out Product found))
                PrintProduct(found);
            else
                Console.WriteLine("Product not found!");
        }
        else if (choice == 2)
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            if (SearchProduct(name, out Product found))
                PrintProduct(found);
            else
                Console.WriteLine("Product not found!");
        }
    }

    // =========================
    // Method Overloading + out
    // =========================
    static bool SearchProduct(int id, out Product found)
    {
        foreach (var p in products)
        {
            if (p.Id == id)
            {
                found = p;
                return true;
            }
        }
        found = null;
        return false;
    }

    static bool SearchProduct(string name, out Product found)
    {
        foreach (var p in products)
        {
            if (p.Name.ToLower() == name.ToLower())
            {
                found = p;
                return true;
            }
        }
        found = null;
        return false;
    }

    static void PrintProduct(Product p)
    {
        Console.WriteLine($"ID: {p.Id}, Name: {p.Name}, Price: {p.Price}, Qty: {p.Quantity}");
    }

    // =========================
    // Add to Cart
    // =========================
    static void AddToCartMenu()
    {
        try
        {
            Console.Write("Enter Product ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter Quantity: ");
            int qty = int.Parse(Console.ReadLine());

            AddToCart(id, ref qty);
        }
        catch
        {
            Console.WriteLine("Invalid input!");
        }
    }

    // =========================
    // ref usage
    // =========================
    static void AddToCart(int productId, ref int quantity)
    {
        if (SearchProduct(productId, out Product p))
        {
            if (quantity <= 0)
            {
                Console.WriteLine("Invalid quantity!");
                return;
            }

            if (p.Quantity >= quantity)
            {
                p.Quantity -= quantity;

                cart.Add(new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = quantity
                });

                Console.WriteLine("Added to cart!");
            }
            else
            {
                Console.WriteLine("Insufficient quantity!");
            }
        }
        else
        {
            Console.WriteLine("Product not found!");
        }
    }

    // =========================
    // Recursion (View Cart)
    // =========================
    static void ViewCartRecursive(int index)
    {
        if (cart.Count == 0)
        {
            Console.WriteLine("Cart is empty.");
            return;
        }

        if (index >= cart.Count)
            return;

        var item = cart[index];
        Console.WriteLine($"Name: {item.Name}, Qty: {item.Quantity}, Price: {item.Price}");

        ViewCartRecursive(index + 1);
    }

    // =========================
    // Checkout
    // =========================
    static void Checkout()
    {
        if (cart.Count == 0)
        {
            Console.WriteLine("Cart is empty!");
            return;
        }

        double total = 0;

        foreach (var item in cart)
        {
            total += item.Price * item.Quantity;
        }

        Console.WriteLine($"Total Price: {total}");

        cart.Clear();
        Console.WriteLine("Checkout complete!");
    }
}