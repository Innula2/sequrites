using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;

namespace WpfSales.Model
{
    public class Operation : Model
    {
        public int Id { get; set; }
        public string Deal { get; set; }
        public int DealId { get; set; }
        public string Subaccount { get; set; }
        public int SubaccountId { get; set; }
        public int Number { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
        public double Sum { get; set; }
        public double SaldoInput { get; set; }
        public double SaldoOutput { get; set; }

        public static List<Operation> GetList()
        {
            var list = new List<Operation>();
            try
            {
                using (var connect = new SQLiteConnection(_ConnectionString))
                {
                    connect.Open();
                    var command = connect.CreateCommand();
                    command.CommandText = "SELECT id, (SELECT tiker FROM deals WHERE id = dealid),(SELECT name FROM subaccounts WHERE id = subaccountid), number, date, type, sum, saldoinput, saldooutput  FROM operations";
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string dealid = reader.GetString(1);
                        string subaccountid = reader.GetString(2);
                        int number = reader.GetInt32(3);
                        string date = reader.GetString(4);
                        string type = reader.GetString(5);
                        double sum = reader.GetDouble(6);
                        double saldoinput = reader.GetDouble(7);
                        double saldooutput = reader.GetDouble(8);
                        var operations = new Operation
                        {
                            Id = id,
                            Deal = dealid,
                            Subaccount = subaccountid,
                            Number = number,
                            Date = date,
                            Type = type,
                            Sum = sum,
                            SaldoInput = saldoinput,
                            SaldoOutput = saldooutput
                        };
                        list.Add(operations);
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return list;
        }
        public static void Add(Operation operations)
        {
            try
            {
                using (var connect = new SQLiteConnection(_ConnectionString))
                {
                    connect.Open();
                    var command = connect.CreateCommand();
                    command.CommandText = String.Format(@"INSERT INTO operations (dealid, subaccountid, number, date,type , sum, saldoinput, saldooutput ) VALUES (@dealid, @subaccountid, @number, @date, @type , @sum, @saldoinput, @saldooutput)");
                    command.Parameters.AddWithValue("dealid", operations.DealId);
                    command.Parameters.AddWithValue("subaccountid", operations.SubaccountId);
                    command.Parameters.AddWithValue("number", operations.Number);
                    command.Parameters.AddWithValue("date", operations.Date);
                    command.Parameters.AddWithValue("type", operations.Type);
                    command.Parameters.AddWithValue("sum", operations.Sum);
                    command.Parameters.AddWithValue("saldoinput", operations.SaldoInput);
                    command.Parameters.AddWithValue("saldooutput", operations.SaldoOutput);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static void Update(Operation operations)
        {
            try
            {
                using (var connect = new SQLiteConnection(_ConnectionString))
                {
                    connect.Open();
                    var command = connect.CreateCommand();
                    command.CommandText = String.Format(@"UPDATE operations SET dealid=@dealid, subaccountid=@subaccountid, number=@number, date=@date, type=@type, sum=@sum, saldoinput=@saldoinput, saldooutput=@saldooutput WHERE id=@id");
                    command.Parameters.AddWithValue("dealid", operations.DealId);
                    command.Parameters.AddWithValue("subaccountid", operations.SubaccountId);
                    command.Parameters.AddWithValue("number", operations.Number);
                    command.Parameters.AddWithValue("date", operations.Date);
                    command.Parameters.AddWithValue("type", operations.Type);
                    command.Parameters.AddWithValue("sum", operations.Sum);
                    command.Parameters.AddWithValue("saldoinput", operations.SaldoInput);
                    command.Parameters.AddWithValue("saldooutput", operations.SaldoOutput);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static void Delete(Operation operations)
        {
            try
            {
                using (var connect = new SQLiteConnection(_ConnectionString))
                {
                    connect.Open();
                    var command = connect.CreateCommand();
                    command.CommandText = String.Format(@"DELETE FROM operations WHERE id = @id");
                    command.Parameters.AddWithValue("id", operations.Id);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public Operation()
        {
        }
    }
}
