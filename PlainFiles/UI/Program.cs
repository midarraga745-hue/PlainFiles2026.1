using Core;

var textFile = new SimpleTextFile("c:\\tmp\\animals.txt");
using var logger = new LogWriter("c:\\tmp\\app.log");

try
{
    logger.WriteLog("info", "Application started");
    var lines = textFile.ReadLines();
    var list = lines.ToList();
    var option = string.Empty;

    do
    {
        option = Menu();
        switch (option)
        {
            case "1":
                foreach (var item in list)
                {
                    Console.WriteLine(item);
                }
                logger.WriteLog("info", "File listed.");
                break;

            case "2":
                Console.Write("Enter a new line: ");
                var newLine = Console.ReadLine();
                list.Add(newLine!);
                logger.WriteLog("info", $"New line added: {newLine}");
                break;

            case "3":
                Console.Write("Enter a new line: ");
                var lineToRemove = Console.ReadLine();
                list.Remove(lineToRemove!);
                logger.WriteLog("info", $"Line removed: {lineToRemove}");
                break;

            // Ingresar un texto que desea eliminar por completo
            case "4":
                Console.Write("Ingrese el texto que desea eliminar por completo: ");
                string target = Console.ReadLine();
                // Elimina todos los elementos que coincidan exactamente
                list.RemoveAll(item => item == target);
                Console.WriteLine("Todas las ocurrencias han sido eliminadas.");
                logger.WriteLog("info", $"Todas las ocurrencias de '{target}' eliminadas.");
                break;

            case "5":
                list.Sort();
                Console.WriteLine("La lista ha sido ordenada alfabéticamente.");
                logger.WriteLog("info", "Lista ordenada alfabéticamente.");
                break;

            case "6":
                textFile.WriteLines(list.ToArray());
                Console.WriteLine("Changes saved.");
                logger.WriteLog("info", "File saved.");
                break;

            default:
                break;
        }
    } while (option != "0");
    textFile.WriteLines(list.ToArray());
    Console.WriteLine("Changes saved.");
}
catch (Exception ex)
{
    logger.WriteLog("error", $"An error happened: {ex.Message}.");
}
finally
{
    logger.WriteLog("info", "Application ended.");
}

string Menu()
{
    Console.WriteLine("Menu:");
    Console.WriteLine("1. Show lines");
    Console.WriteLine("2. Add line");
    Console.WriteLine("3. Remove one occurrence");
    Console.WriteLine("4. Remove all occurrences"); // Homework: Implement this option
    Console.WriteLine("5. Sort"); // Homework: Implement this option
    Console.WriteLine("6. Save changes");
    Console.WriteLine("0. Exit");
    Console.Write("Choose an option: ");
    return Console.ReadLine() ?? string.Empty;
}

var people = new List<string[]>
{
    new[] { "Id", "Name", "Age"},
    new[] { "1", "Juan", "51"},
    new[] { "2", "Maria", "46"},
    new[] { "3", "Valery", "16"},
};

var manualCsvHelper = new ManualCsvHelper();
manualCsvHelper.WriteCSV("c:\\tmp\\people.csv", people);

var loadedPeople = manualCsvHelper.ReadCSV("c:\\tmp\\people.csv");
foreach (var person in loadedPeople)
{
    Console.WriteLine(string.Join("|", person));
}

// Homework: Implement a CSV helper with a menu.