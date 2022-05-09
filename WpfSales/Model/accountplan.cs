using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;

namespace WpfSales.Model
{
    public class accountplan : Model
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Type { get; set; }

        public int Number { get; set; }

        public static List<accountplan> GetList()
        {
            var list = new List<accountplan>();
            try
            {
                using (var connect = new SQLiteConnection(_ConnectionString))
                {
                    connect.Open();
                    var command = connect.CreateCommand();
                    command.CommandText = "SELECT id, name, type, number FROM accountplans";
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string type = reader.GetString(2);
                        int number = reader.GetInt32(3);
                        var accountplans = new accountplan
                        {
                            Id = id,
                            Name = name,
                            Type = type,
                            Number = number,
                        };
                        list.Add(accountplans);
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return list;
        }
        public static void Add(accountplan accountplans)
        {
            try
            {
                using (var connect = new SQLiteConnection(_ConnectionString))
                {
                    connect.Open();
                    var command = connect.CreateCommand();
                    command.CommandText = String.Format(@"INSERT INTO accountplans (name, type, number) VALUES (@name, @type, @number)");
                    command.Parameters.AddWithValue("name", accountplans.Name);
                    command.Parameters.AddWithValue("type", accountplans.Type);
                    command.Parameters.AddWithValue("number", accountplans.Number);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static void Update(accountplan accountplans)
        {
            try
            {
                using (var connect = new SQLiteConnection(_ConnectionString))
                {
                    connect.Open();
                    var command = connect.CreateCommand();
                    command.CommandText = String.Format(@"UPDATE accountplans SET name=@name, type=@type, number=@number WHERE id=@id");
                    command.Parameters.AddWithValue("name", accountplans.Name);
                    command.Parameters.AddWithValue("type", accountplans.Type);
                    command.Parameters.AddWithValue("number", accountplans.Number);
                    command.Parameters.AddWithValue("id", accountplans.Id);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static void Delete(accountplan accountplans)
        {
            try
            {
                using (var connect = new SQLiteConnection(_ConnectionString))
                {
                    connect.Open();
                    var command = connect.CreateCommand();
                    command.CommandText = String.Format(@"DELETE FROM accountplans WHERE id = @id");
                    command.Parameters.AddWithValue("id", accountplans.Id);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public accountplan()
        {
        }
    }
}
