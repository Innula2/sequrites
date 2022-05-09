using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;

namespace WpfSales.Model
{
    public class Deal : Model
    {
        public int Id { get; set; }
        public int Agreement { get; set; }  

        public string Tiker { get; set; }
        public int Order { get; set; }  

        public int Number { get; set; }
        public string Date { get; set; } 
        public int Quantity { get; set; }

        public double Price { get; set; } 
        public double TotalCost { get; set; } 

        public string Trader { get; set; }
        public double Commission { get; set; } 

        public static List<Deal> GetList()
        {
            var list = new List<Deal>();
            try
            {
                using (var connect = new SQLiteConnection(_ConnectionString))
                {
                    connect.Open();
                    var command = connect.CreateCommand();
                    command.CommandText = "SELECT * FROM deals";
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        int Agreement = reader.GetInt32(1);
                        string Tiker = reader.GetString(2);
                        int Order = reader.GetInt32(3);
                        int Number = reader.GetInt32(4);
                        string Date = reader.GetString(5);
                        int Quantity = reader.GetInt32(6);
                        double Price = reader.GetDouble(7);
                        double TotalCost = reader.GetDouble(8);
                        string Trader = reader.GetString(9);
                        double Commission = reader.GetDouble(10);
                        var deals = new Deal
                        {
                            Id = id,
                            Agreement = Agreement,
							Tiker = Tiker,
                            Order = Order,
                            Number = Number,
                            Date = Date,
                            Quantity = Quantity,
                            Price = Price,
                            TotalCost = TotalCost,
                            Trader = Trader,
                            Commission = Commission
                        };
                        list.Add(deals);
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return list;
        }
        public static void Add(Deal deals)
        {
            try
            {
                using (var connect = new SQLiteConnection(_ConnectionString))
                {
                    connect.Open();
                    var command = connect.CreateCommand();
                    command.CommandText = String.Format(@"INSERT INTO deals  (agreement, tiker, order_deal, number_deal, date, quantity, price, totalcost, trader, commission ) VALUES( @agreement, @tiker, @order_deal, @number_deal, @date, @quantity, @price, @totalcost, @trader, @commission)");
                    command.Parameters.AddWithValue("agreement", deals.Agreement);
                    command.Parameters.AddWithValue("tiker", deals.Tiker);
                    command.Parameters.AddWithValue("order_deal", deals.Order);
                    command.Parameters.AddWithValue("number_deal", deals.Number);
                    command.Parameters.AddWithValue("date", deals.Date);
                    command.Parameters.AddWithValue("quantity", deals.Quantity);
                    command.Parameters.AddWithValue("price", deals.Price);
                    command.Parameters.AddWithValue("totalcost", deals.TotalCost);
                    command.Parameters.AddWithValue("trader", deals.Trader);
                    command.Parameters.AddWithValue("comission", deals.Commission);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static void Update(Deal deals)
        {
            try
            {
                using (var connect = new SQLiteConnection(_ConnectionString))
                {
                    connect.Open();
                    var command = connect.CreateCommand();
                    command.CommandText = String.Format(@"UPDATE deals SET  agreement=@agreement, tiker=@tiker, order_deal=@order_deal,  number_deal=@number_deal, date=@date, quantity=@quantity, price=@price, totalcost=@totalcost, trader=@trader , commission=@commission WHERE id=@id");
                    command.Parameters.AddWithValue("agreement", deals.Agreement);
                    command.Parameters.AddWithValue("tiker", deals.Tiker);
                    command.Parameters.AddWithValue("order_deal", deals.Order);
                    command.Parameters.AddWithValue("number_deal", deals.Number);
                    command.Parameters.AddWithValue("date", deals.Date);
                    command.Parameters.AddWithValue("quantity", deals.Quantity);
                    command.Parameters.AddWithValue("price", deals.Price);
                    command.Parameters.AddWithValue("totalcost", deals.TotalCost);
                    command.Parameters.AddWithValue("trader", deals.Trader);
                    command.Parameters.AddWithValue("comission", deals.Commission);

                    command.Parameters.AddWithValue("id", deals.Id);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static void Delete(Deal deals)
        {
            try
            {
                using (var connect = new SQLiteConnection(_ConnectionString))
                {
                    connect.Open();
                    var command = connect.CreateCommand();
                    command.CommandText = String.Format(@"DELETE FROM deals WHERE id = @id");
                    command.Parameters.AddWithValue("id", deals.Id);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public Deal()
        {
        }


    }
}
