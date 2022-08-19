using Newtonsoft.Json;
using System.Globalization;

int id;
string res;
decimal sum;
DateTime date;
var transactions = new List<Transaction>();

while (true)
{
    Console.WriteLine("Введите команду: (add/get/exit)");
    var command = Console.ReadLine();
    switch (command)
    {
        default:
            Console.WriteLine("Команда не распознана");
            break;
        case "exit":
            return;
        case "add":
            Console.Write("Введите Id: ");
            res = Console.ReadLine();
            if (int.TryParse(res, out id))
            {
                if (transactions.Any(x => x.Id == id))
                {
                    Console.WriteLine("Транзакция с таким идентификатором уже существует");
                }
                Console.Write("Введите дату: ");
                res = Console.ReadLine();
                if (DateTime.TryParseExact(res, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    Console.Write("Введите сумму: ");
                    res = Console.ReadLine();
                    if (decimal.TryParse(res.Replace('.', ','), out sum))
                    {
                        transactions.Add(new Transaction()
                        {
                            Amount = sum,
                            Id = id,
                            TransactionDate = date
                        });
                        Console.WriteLine("[OK]");
                    }
                    else
                    {
                        Console.WriteLine("Сумма не корректена");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Дата не корректена");
                    break;
                }
            }
            else
            {
                Console.WriteLine("Идентификатор не корректен");
                break;
            }
            break;
        case "get":
            Console.Write("Введите Id: ");
            res = Console.ReadLine();
            if (int.TryParse(res, out id))
            {
                var t = transactions.FirstOrDefault(x => x.Id == id);
                if (t is not null)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(t, Formatting.Indented));
                }
                else
                {
                    Console.WriteLine("Транзакция не найдена");
                    break;
                }
            }
            else
            {
                Console.WriteLine("Неверный номер транзакции");
            }
            break;
    }
}


/// <summary>
/// Транзакция.
/// </summary>
public class Transaction
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    [JsonProperty("id")]
    public int Id { get; set; }

    /// <summary>
    /// Дата транзакции.
    /// </summary>
    /// Я не знаю, почему в имени пробел, может быть опечатка,
    /// а может быть проверка на внимательность, поэтому вот.
    /// Брал из доки "Т1.pdf".
    [JsonProperty("transactionDate ")]
    public DateTime TransactionDate { get; set; }

    /// <summary>
    /// Сумма транзакции.
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }
}