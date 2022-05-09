using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;

namespace WpfSales.Model
{
    public class subaccount : Model
    {
        public int Id { get; set; }

        public string AccountPlan { get; set; }
        public int AccountPlanID { get; set; }

        public string Name { get; set; }

        public double Number { get; set; }

        public static List<subaccount> GetList()
        {
            var list = new List<subaccount>();
            try
            {
                using (var connect = new SQLiteConnection(_ConnectionString))
                {
                    connect.Open();
                    var command = connect.CreateCommand();
                    command.CommandText = "SELECT id, (SELECT name FROM accountplans WHERE id = accountplanid), name, number FROM subaccounts";
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string accountplanid = reader.GetString(1);
                        string name = reader.GetString(2);
                        double number = reader.GetDouble(3);
                        var subaccounts = new subaccount
                        {
                            Id = id,
                            AccountPlan = accountplanid,
                            Name = name,
                            Number = number,
                        };
                        list.Add(subaccounts);
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return list;
        }
        public static void Add(subaccount subaccounts)
        {
            try
            {
                using (var connect = new SQLiteConnection(_ConnectionString))
                {
                    connect.Open();
                    var command = connect.CreateCommand();
                    command.CommandText = String.Format(@"INSERT INTO subaccounts (accountplanid, name, number)VALUES( @accountplanid, @name, @number)");
                    command.Parameters.AddWithValue("accountplanid", subaccounts.AccountPlanID);
                    command.Parameters.AddWithValue("name", subaccounts.Name);
                    command.Parameters.AddWithValue("number", subaccounts.Number);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static void Update(subaccount subaccounts)
        {
            try
            {
                using (var connect = new SQLiteConnection(_ConnectionString))
                {
                    connect.Open();
                    var command = connect.CreateCommand();
                    command.CommandText = String.Format(@"UPDATE subaccounts SET  accountplanid=@accountplanid, name=@name, number=@number WHERE id=@id");
                    command.Parameters.AddWithValue("accountplanid", subaccounts.AccountPlanID);
                    command.Parameters.AddWithValue("name", subaccounts.Name);
                    command.Parameters.AddWithValue("number", subaccounts.Number);
                    command.Parameters.AddWithValue("id", subaccounts.Id);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static void Delete(subaccount subaccounts)
        {
            try
            {
                using (var connect = new SQLiteConnection(_ConnectionString))
                {
                    connect.Open();
                    var command = connect.CreateCommand();
                    command.CommandText = String.Format(@"DELETE FROM subaccounts WHERE id = @id");
                    command.Parameters.AddWithValue("id", subaccounts.Id);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public subaccount()
        {
        }


    }
}
