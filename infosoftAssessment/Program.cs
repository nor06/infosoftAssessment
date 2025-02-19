using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using MySql.Data.MySqlClient;

namespace infosoftAssessment
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false; 

            while (!exit)
            {
                Console.Clear(); 
                Console.WriteLine("WELCOME TO BOGSY VIDEO STORE!!\n");
                Console.WriteLine("Customer Library   |   Video Library   |   Reports   |   Rental\n");
                Console.Write("Select a Library (enter customer, video, reports, rental, or exit to quit): ");
                String home = Console.ReadLine().ToLower();

                if (home.ToLower() == "customer")
                {
                    customerLibrary();
                }
                else if (home.ToLower() == "video")
                {
                    videoLibrary();
                }
                else if (home.ToLower() == "reports")
                {
                    reports();
                }
                else if (home.ToLower() == "rental")
                {
                    rentalModule();
                }
                else if (home.ToLower() == "exit")
                {
                    exit = true; 
                    Console.WriteLine("Thank you for using Bogsy Video Store! Goodbye.");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter customer, video, reports, rental, or exit.");
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }















        static void customerLibrary()
        {
            
            string connectionString = "Server=localhost;User ID=root;Password=;Database=bogsy";

            using (MySql.Data.MySqlClient.MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
            {
                try
                {
                    con.Open(); 
                    Console.WriteLine("Connected to the database.\n");
                
                    string selectQuery = "SELECT id, name, email, phoneNumber FROM customer";
                    using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(selectQuery, con))
                    using (MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader())
                    
                    {
                        Console.WriteLine("Current Customers\n");
                        Console.WriteLine("ID | Name | Email | Phone Number");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["id"]} | {reader["name"]} | {reader["email"]} | {reader["phoneNumber"]}");
                        }
                    }
                    Console.WriteLine("\nAdd Customer   |   Edit Customer\n");
                    Console.Write("Select a method (enter add or edit only): ");
                    string customer = Console.ReadLine();

                    if (customer.ToLower() == "add")
                    {
                      
                        Console.Write("Enter name: ");
                        string customerName = Console.ReadLine();
                        Console.Write("Enter email: ");
                        string customerEmail = Console.ReadLine();
                        Console.Write("Enter phone number: ");
                        string customerPhoneNumber = Console.ReadLine();

                     
                        string query = "INSERT INTO customer (name, email, phoneNumber) VALUES (@name, @email, @phoneNumber)";

                        using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, con))
                        {
                            
                            cmd.Parameters.AddWithValue("@name", customerName);
                            cmd.Parameters.AddWithValue("@email", customerEmail);
                            cmd.Parameters.AddWithValue("@phoneNumber", customerPhoneNumber);

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("Customer added successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Failed to add customer.");
                            }
                        }
                    }
                    else if (customer.ToLower() == "edit")
                    {

                       
                        Console.Write("\nEnter customer ID to edit: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("Enter new name: ");
                        string newName = Console.ReadLine();
                        Console.Write("Enter new email: ");
                        string newEmail = Console.ReadLine();
                        Console.Write("Enter new phone number: ");
                        string newPhoneNumber = Console.ReadLine();

                      
                        string updateQuery = "UPDATE customer SET name = @name, email = @email, phoneNumber = @phoneNumber WHERE id = @id";

                        using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, con))
                        {
                           
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.Parameters.AddWithValue("@name", newName);
                            cmd.Parameters.AddWithValue("@email", newEmail);
                            cmd.Parameters.AddWithValue("@phoneNumber", newPhoneNumber);

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("Customer updated successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Customer not found or update failed.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
        static void videoLibrary()
        {
            
            string connectionString = "Server=localhost;User ID=root;Password=;Database=bogsy";

            using (MySql.Data.MySqlClient.MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    Console.WriteLine("Connected to the database.\n");

                   
                    string selectQuery = "SELECT videoId, title, category, available, rented FROM video";
                    using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(selectQuery, con))
                    using (MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\nCurrent Videos\n");
                        Console.WriteLine("ID | Title | Category | Available | Rented");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["videoId"]} | {reader["title"]} | {reader["category"]} | {reader["available"]} | {reader["rented"]}");
                        }
                    }

                    Console.WriteLine("\nAdd Video   |   Edit Video   |   Delete Video\n");
                    Console.Write("Select a method (enter add, edit, or delete only): ");
                    string action = Console.ReadLine().ToLower();

                    if (action == "add")
                    {
                        Console.Write("Enter title: ");
                        string title = Console.ReadLine();

                       
                        string category;
                        do
                        {
                            Console.Write("Enter category (VCD or DVD only): ");
                            category = Console.ReadLine().ToUpper();
                        } while (category != "VCD" && category != "DVD");

                        Console.Write("Enter rental days limit (1 to 3): ");
                        int rentalDaysLimit;
                        while (!int.TryParse(Console.ReadLine(), out rentalDaysLimit) || rentalDaysLimit < 1 || rentalDaysLimit > 3)
                        {
                            Console.Write("Invalid input. Enter rental days limit (1 to 3): ");
                        }

                        Console.Write("Quantity available: ");
                        int available = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Quantity rented: ");
                        int rented = Convert.ToInt32(Console.ReadLine());

                        string query = "INSERT INTO video (title, category, rentalDaysLimit, available, rented) VALUES (@title, @category, @rentalDaysLimit, @available, @rented)";
                        using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@title", title);
                            cmd.Parameters.AddWithValue("@category", category);
                            cmd.Parameters.AddWithValue("@rentalDaysLimit", rentalDaysLimit);
                            cmd.Parameters.AddWithValue("@available", available);
                            cmd.Parameters.AddWithValue("@rented", rented);
                            cmd.ExecuteNonQuery();
                            Console.WriteLine("Video added successfully.");
                        }
                    }
                    else if (action == "edit")
                    {
                        Console.Write("Enter video ID to edit: ");
                        int videoId = int.Parse(Console.ReadLine());

                        Console.Write("Enter new title: ");
                        string newTitle = Console.ReadLine();

                     
                        string newCategory;
                        do
                        {
                            Console.Write("Enter new category (VCD or DVD only): ");
                            newCategory = Console.ReadLine().ToUpper();
                        } while (newCategory != "VCD" && newCategory != "DVD");

                        Console.Write("Enter new rental days limit (1 to 3): ");
                        int newRentalDaysLimit;
                        while (!int.TryParse(Console.ReadLine(), out newRentalDaysLimit) || newRentalDaysLimit < 1 || newRentalDaysLimit > 3)
                        {
                            Console.Write("Invalid input. Enter rental days limit (1 to 3): ");
                        }

                        Console.Write("Enter new available quantity: ");
                        int newAvailable = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter new rented quantity: ");
                        int newRented = Convert.ToInt32(Console.ReadLine());

                        string updateQuery = "UPDATE video SET title = @title, category = @category, rentalDaysLimit = @rentalDaysLimit, available = @available, rented = @rented WHERE videoId = @videoId";
                        using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@videoId", videoId);
                            cmd.Parameters.AddWithValue("@title", newTitle);
                            cmd.Parameters.AddWithValue("@category", newCategory);
                            cmd.Parameters.AddWithValue("@rentalDaysLimit", newRentalDaysLimit);
                            cmd.Parameters.AddWithValue("@available", newAvailable);
                            cmd.Parameters.AddWithValue("@rented", newRented);
                            cmd.ExecuteNonQuery();
                            Console.WriteLine("Video updated successfully.");
                        }
                    }
                    else if (action == "delete")
                    {
                        Console.Write("Enter video ID to delete: ");
                        int videoId = int.Parse(Console.ReadLine());

                        string deleteQuery = "DELETE FROM video WHERE videoId = @videoId";
                        using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(deleteQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@videoId", videoId);
                            cmd.ExecuteNonQuery();
                            Console.WriteLine("Video deleted successfully.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }


        static void reports()
        {
            string connectionString = "Server=localhost;User ID=root;Password=;Database=bogsy";
            using (MySql.Data.MySqlClient.MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    Console.WriteLine("\nCustomer Reports   |   Video Reports   |   Rental Reports\n");
                    Console.Write("Select a report type (enter customer, video or rental only): ");
                    string report = Console.ReadLine();

                    if (report.ToLower() == "customer")
                    {
                      
                        string selectQuery = "SELECT id, name, email, phoneNumber FROM customer";
                        using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(selectQuery, con))
                        using (MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader())

                        {
                            Console.WriteLine("Current Customers\n");
                            Console.WriteLine("ID | Name | Email | Phone Number");
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader["id"]} | {reader["name"]} | {reader["email"]} | {reader["phoneNumber"]}");
                            }
                        }
                    }
                    else if (report.ToLower() == "video")
                    {
                        string selectQuery = "SELECT videoId, title, category, available, rented FROM video";
                        using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(selectQuery, con))
                        using (MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            Console.WriteLine("\nCurrent Videos\n");
                            Console.WriteLine("ID | Title | Category | Available | Rented");
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader["videoId"]} | {reader["title"]} | {reader["category"]} | {reader["available"]} | {reader["rented"]}");
                            }
                        }
                    }
                    else if (report.ToLower() == "rental")
                    {
                        string selectQuery = "SELECT rentalId, id, videoId, rentalDate, returnDate, actualReturnDate, rentalFee, lateFee FROM rental";
                        using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(selectQuery, con))
                        using (MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            Console.WriteLine("\nCurrent Rentals\n");
                            Console.WriteLine("Rental ID | Customer ID | Video ID | Rental Date | Return Date | Actual Return Date | Rental Fee | Late Fee");
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader["rentalId"]} | {reader["id"]} | {reader["videoId"]} | {reader["rentalDate"]} | {reader["returnDate"]} | {reader["actualReturnDate"]} | {reader["rentalFee"]} | {reader["lateFee"]}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
        static void rentalModule()
        {
            string connectionString = "Server=localhost;User ID=root;Password=;Database=bogsy";
            using (MySql.Data.MySqlClient.MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    Console.WriteLine("Connected to the database.\n");

                    string selectQuery = "SELECT id, name, email, phoneNumber FROM customer";
                    using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(selectQuery, con))
                    using (MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("Current Customers");
                        Console.WriteLine("ID | Name | Email | Phone Number");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["id"]} | {reader["name"]} | {reader["email"]} | {reader["phoneNumber"]}");
                        }
                    }

                    string selectQuery1 = "SELECT videoId, title, category, available, rented FROM video";
                    using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(selectQuery1, con))
                    using (MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\nCurrent Videos");
                        Console.WriteLine("ID | Title | Category | Available | Rented");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["videoId"]} | {reader["title"]} | {reader["category"]} | {reader["available"]} | {reader["rented"]}");
                        }
                    }


                    Console.Write("\nEnter Customer ID: ");
                    int id = int.Parse(Console.ReadLine());
                    Console.Write("Enter Video ID: ");
                    int videoId = int.Parse(Console.ReadLine());


                    string selectQuery2 = "SELECT title, category, available, rentalDaysLimit FROM video WHERE videoId = @videoId";
                    using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(selectQuery2, con))
                    {
                        cmd.Parameters.AddWithValue("@videoId", videoId);
                        using (MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                Console.WriteLine("Video not found.");
                                return;
                            }

                            string title = reader["title"].ToString();
                            string category = reader["category"].ToString();
                            int available = Convert.ToInt32(reader["available"]);
                            int rentalDaysLimit = Convert.ToInt32(reader["rentalDaysLimit"]);
                            reader.Close();

                            if (available <= 0)
                            {
                                Console.WriteLine("Sorry, this video is out of stock.");
                                return;
                            }


                            double rentalFee = (category == "DVD") ? 50.0 : 25.0;
                            DateTime rentalDate = DateTime.Now;
                            DateTime returnDate = rentalDate.AddDays(rentalDaysLimit);


                            string insertQuery = "INSERT INTO rental (id, videoId, rentalDate, returnDate, rentalFee) VALUES (@id, @videoId, @rentalDate, @returnDate, @rentalFee)";
                            using (MySql.Data.MySqlClient.MySqlCommand insertCmd = new MySql.Data.MySqlClient.MySqlCommand(insertQuery, con))
                            {
                                insertCmd.Parameters.AddWithValue("@id", id);
                                insertCmd.Parameters.AddWithValue("@videoId", videoId);
                                insertCmd.Parameters.AddWithValue("@rentalDate", rentalDate);
                                insertCmd.Parameters.AddWithValue("@returnDate", returnDate);
                                insertCmd.Parameters.AddWithValue("@rentalFee", rentalFee);
                                insertCmd.ExecuteNonQuery();
                            }

                            string updateQuery = "UPDATE video SET available = available - 1 WHERE videoId = @videoId";
                            using (MySql.Data.MySqlClient.MySqlCommand updateCmd = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, con))
                            {
                                updateCmd.Parameters.AddWithValue("@videoId", videoId);
                                updateCmd.ExecuteNonQuery();
                            }

                            Console.WriteLine($"\nRental recorded: {title} rented for {rentalDaysLimit} days. Due date: {returnDate.ToShortDateString()}.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }



    }
}
