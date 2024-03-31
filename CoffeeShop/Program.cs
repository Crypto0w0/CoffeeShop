using System.Data.SqlClient;

class Program
{
    static string connectionString = "Data Source=localhost;Database=CoffeeShop;Integrated Security=false;User ID=root;Password=Alex228420;";

    static void Main()
    {
        using(SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("Connection succeded");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Connection error: {e.Message}");
            }
        }
        int ans;
        Console.WriteLine("1 - Add new Coffee");
        Console.WriteLine("2 - Redact Coffee");
        Console.WriteLine("3 - Delete Coffee");
        Console.WriteLine("4 - Check for Cherry");
        Console.WriteLine("5 - Find a coffee in the price diapasone");
        Console.WriteLine("6 - Find a coffee in the weight diapasone");
        Console.WriteLine("7 - Find all coffies in the defined country");
        Console.WriteLine("8 - Find amount of types in every country");
        ans = Convert.ToInt32(Console.ReadLine());
        if (ans == 1)
        {
            AddCoffee();
        }
        else if (ans == 2)
        {
            UpdateCoffee();
        }
        else if (ans == 3)
        {
            DeleteCoffee();
        }
        else if (ans == 4)
        {
            Check4Cherry();
        }
        else if (ans == 5)
        {
            FindInPriceDiapasone();
        }
        else if (ans == 6)
        {
            FindInWeightDiapasone();
        }
        else if (ans == 7)
        {
            FindCoffeeInTheCountry();
        }
        else if (ans == 8)
        {
            FindAllTypesInCountries();
        }
        else if (ans == 9)
        {
            FindAvgWeightInCountries();
        }
        else Console.WriteLine("Unknown command");
    }
    static void AddCoffee()
    {
        using(SqlConnection connection = new SqlConnection(connectionString))
        {
            Console.Write("Name: ");
            string n = Console.ReadLine();
            Console.Write("Price: ");
            int p = Convert.ToInt32(Console.ReadLine());
            Console.Write("Weight: ");
            int w = Convert.ToInt32(Console.ReadLine());
            Console.Write("Type: ");
            string t = Console.ReadLine();
            Console.Write("Info: ");
            string i = Console.ReadLine();
            Console.Write("Country: ");
            string c = Console.ReadLine();
            connection.Open();
            string query = "INSERT INTO CoffeeShop (name, price, weight, type, info, country) VALUES (@Name, @Price, @Weight, @Type, @Info, @Country)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", n);
                command.Parameters.AddWithValue("@Price", p);
                command.Parameters.AddWithValue("@Weight", w);
                command.Parameters.AddWithValue("@Type", t);
                command.Parameters.AddWithValue("@Info", i);
                command.Parameters.AddWithValue("@Country", c);
            }
            
        }
    }

    static void UpdateCoffee()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            Console.WriteLine("Enter the id of coffee you want to delete");
            int ID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("1 - Change name");
            Console.WriteLine("2 - Change price");
            Console.WriteLine("3 - Change weight");
            Console.WriteLine("4 - Change type");
            Console.WriteLine("5 - Change info");
            Console.WriteLine("6 - Change country");
            int ans = Convert.ToInt32(Console.ReadLine());
            string query;
            if (ans == 1)
            {
                Console.WriteLine("New name: ");
                string n = Console.ReadLine();
                query = "UPDATE CoffeeShop SET name = @Name WHERE id = @ID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", n);
                    command.Parameters.AddWithValue("@ID", ID);
                }
            }
            else if (ans == 2)
            {
                Console.WriteLine("New price: ");
                int p = Convert.ToInt32(Console.ReadLine());
                query = "UPDATE CoffeeShop SET price = @Price WHERE id = @ID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Price", p);
                    command.Parameters.AddWithValue("@ID", ID);
                    
                }
            }
            else if (ans == 3)
            {
                Console.WriteLine("New weight: ");
                int w = Convert.ToInt32(Console.ReadLine());
                query = "UPDATE CoffeeShop SET weight = @Weight WHERE id = @ID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Weight", w);
                    command.Parameters.AddWithValue("@ID", ID);
                }
            }
            else if (ans == 4)
            {
                Console.WriteLine("New type: ");
                string t = Console.ReadLine();
                query = "UPDATE CoffeeShop SET type = @Type WHERE id = @ID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Type", t);
                    command.Parameters.AddWithValue("@ID", ID);
                }
            }
            else if (ans == 5)
            {
                Console.WriteLine("New info: ");
                string i = Console.ReadLine();
                query = "UPDATE CoffeeShop SET info = @Info WHERE id = @ID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Info", i);
                    command.Parameters.AddWithValue("@ID", ID);
                }
            }
            else if (ans == 6)
            {
                Console.WriteLine("New country: ");
                string c = Console.ReadLine();
                query = "UPDATE CoffeeShop SET country = @Country WHERE id = @ID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Country", c);
                    command.Parameters.AddWithValue("@ID", ID);
                }
            }
            else Console.WriteLine("Unknown command");
        }
    }
    static void DeleteCoffee()
    {
        Console.WriteLine("Enter the id of coffee you want to delete");
        int ID = Convert.ToInt32(Console.ReadLine());
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "DELETE FROM CoffeeShop WHERE ID = @ID ";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ID", ID);
            }
        }
    }
    static void Check4Cherry()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM CoffeeShop WHERE info LIKE '%cherry%'";
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Name: {reader["name"]}, Price: {reader["price"]}, Weight: {reader["weight"]}, Type: {reader["type"]}, Info: {reader["info"]}, Country: {reader["country"]}");
                }
            }
        }
    }

    static void FindInPriceDiapasone()
    {
        Console.WriteLine("Min: ");
        int min = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Max: ");
        int max = Convert.ToInt32(Console.ReadLine());
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM CoffeeShop WHERE price BETWEEN @Min AND @Max";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Min", min);
                command.Parameters.AddWithValue("@Max", max);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(
                            $"Name: {reader["name"]}, Price: {reader["price"]}, Weight: {reader["weight"]}, Type: {reader["type"]}, Info: {reader["info"]}, Country: {reader["country"]}");
                    }
                }
            }
        }
    }

    static void FindInWeightDiapasone()
    {
        Console.WriteLine("Min: ");
        int min = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Max: ");
        int max = Convert.ToInt32(Console.ReadLine());
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM CoffeeShop WHERE weight BETWEEN @Min AND @Max";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Min", min);
                command.Parameters.AddWithValue("@Max", max);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(
                            $"Name: {reader["name"]}, Price: {reader["price"]}, Weight: {reader["weight"]}, Type: {reader["type"]}, Info: {reader["info"]}, Country: {reader["country"]}");
                    }
                }
            }
        }
    }
    static void FindCoffeeInTheCountry()
    {
        Console.WriteLine("Country: ");
        string c = Console.ReadLine();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM CoffeeShop WHERE country = @Country";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Country", c);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(
                            $"Name: {reader["name"]}, Price: {reader["price"]}, Weight: {reader["weight"]}, Type: {reader["type"]}, Info: {reader["info"]}, Country: {reader["country"]}");
                    }
                }
            }
        }
    }
    static void FindAllTypesInCountries()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT country, COUNT(DISTINCT type) AS amount FROM CoffeeShop";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(
                            $"Country: {reader["country"]}, Amount: {reader["amount"]}");
                    }
                }
            }
        }
    }
    static void FindAvgWeightInCountries()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT country, AVG(weight) AS avgweight FROM CoffeeShop";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(
                            $"Country: {reader["country"]}, Amount: {reader["avgweight"]}");
                    }
                }
            }
        }
    }
}