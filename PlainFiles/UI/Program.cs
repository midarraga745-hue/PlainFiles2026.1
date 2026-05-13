//using Core;

//var textFile = new SimpleTextFile("c:\\tmp\\animals.txt");
//using var logger = new LogWriter("c:\\tmp\\app.log");

//try
//{
//    logger.WriteLog("info", "Application started");
//    var lines = textFile.ReadLines();
//    var list = lines.ToList();
//    var option = string.Empty;

//    do
//    {
//        option = Menu();
//        switch (option)
//        {
//            case "1":
//                foreach (var item in list)
//                {
//                    Console.WriteLine(item);
//                }
//                logger.WriteLog("info", "File listed.");
//                break;

//            case "2":
//                Console.Write("Enter a new line: ");
//                var newLine = Console.ReadLine();
//                list.Add(newLine!);
//                logger.WriteLog("info", $"New line added: {newLine}");
//                break;

//            case "3":
//                Console.Write("Enter a new line: ");
//                var lineToRemove = Console.ReadLine();
//                list.Remove(lineToRemove!);
//                logger.WriteLog("info", $"Line removed: {lineToRemove}");
//                break;

//            // Ingresar un texto que desea eliminar por completo
//            case "4":
//                Console.Write("Ingrese el texto que desea eliminar por completo: ");
//                string target = Console.ReadLine();
//                // Elimina todos los elementos que coincidan exactamente
//                list.RemoveAll(item => item == target);
//                Console.WriteLine("Todas las ocurrencias han sido eliminadas.");
//                logger.WriteLog("info", $"Todas las ocurrencias de '{target}' eliminadas.");
//                break;

//            case "5":
//                list.Sort();
//                Console.WriteLine("La lista ha sido ordenada alfabéticamente.");
//                logger.WriteLog("info", "Lista ordenada alfabéticamente.");
//                break;

//            case "6":
//                textFile.WriteLines(list.ToArray());
//                Console.WriteLine("Changes saved.");
//                logger.WriteLog("info", "File saved.");
//                break;

//            default:
//                break;
//        }
//    } while (option != "0");
//    textFile.WriteLines(list.ToArray());
//    Console.WriteLine("Changes saved.");
//}
//catch (Exception ex)
//{
//    logger.WriteLog("error", $"An error happened: {ex.Message}.");
//}
//finally
//{
//    logger.WriteLog("info", "Application ended.");
//}

//string Menu()
//{
//    Console.WriteLine("Menu:");
//    Console.WriteLine("1. Show lines");
//    Console.WriteLine("2. Add line");
//    Console.WriteLine("3. Remove one occurrence");
//    Console.WriteLine("4. Remove all occurrences"); // Homework: Implement this option
//    Console.WriteLine("5. Sort"); // Homework: Implement this option
//    Console.WriteLine("6. Save changes");
//    Console.WriteLine("0. Exit");
//    Console.Write("Choose an option: ");
//    return Console.ReadLine() ?? string.Empty;
//}

using Core;

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

// Homework: Implement a CSV helper with a menu

// ─────────────────────────────────────────
//  TAREA: MENÚ CSV ← IMPLEMENTACIÓN
// ─────────────────────────────────────────
var csvData = manualCsvHelper.ReadCSV("c:\\tmp\\people.csv").ToList();

if (csvData.Count == 0)
    csvData.Add(new[] { "Id", "Name", "Age" });

string csvOption;
do
{
    csvOption = CsvMenu();
    switch (csvOption)
    {
        case "1": // Mostrar todas las filas
            foreach (var row in csvData)
                Console.WriteLine(string.Join(" | ", row));
            break;

        case "2": // Agregar persona
            Console.Write("Id: ");
            var id = Console.ReadLine()!;
            Console.Write("Nombre: ");
            var name = Console.ReadLine()!;
            Console.Write("Edad: ");
            var age = Console.ReadLine()!;
            csvData.Add(new[] { id, name, age });
            Console.WriteLine("Persona agregada.");
            break;

        case "3": // Eliminar por Id
            Console.Write("Ingresa el Id a eliminar: ");
            var removeId = Console.ReadLine()!;
            int removed = csvData.RemoveAll(r => r.Length > 0 && r[0] == removeId);
            Console.WriteLine(removed > 0 ? "Fila eliminada." : "Id no encontrado.");
            break;

        case "4": // Ordenar por Nombre
            var header = csvData[0];
            var sorted = csvData.Skip(1)
                                .OrderBy(r => r.Length > 1 ? r[1] : "")
                                .ToList();
            csvData = new List<string[]> { header };
            csvData.AddRange(sorted);
            Console.WriteLine("Lista ordenada por Nombre.");
            break;

        case "5": // Guardar
            manualCsvHelper.WriteCSV("c:\\tmp\\people.csv", csvData);
            Console.WriteLine("CSV guardado.");
            break;
    }
} while (csvOption != "0");

// Guardado automático al salir
manualCsvHelper.WriteCSV("c:\\tmp\\people.csv", csvData);
Console.WriteLine("Cambios del CSV guardados automáticamente.");

//  FUNCIONES DE MENÚ

string Menu()
{
    Console.WriteLine("\n===== MENÚ ARCHIVO DE TEXTO =====");
    Console.WriteLine("1. Show lines");
    Console.WriteLine("2. Add line");
    Console.WriteLine("3. Remove one occurrence");
    Console.WriteLine("4. Remove all occurrences");
    Console.WriteLine("5. Sort");
    Console.WriteLine("6. Save changes");
    Console.WriteLine("0. Exit");
    Console.Write("Choose an option: ");
    return Console.ReadLine() ?? string.Empty;
}

string CsvMenu()
{
    Console.WriteLine("\n===== MENÚ CSV =====");
    Console.WriteLine("1. Mostrar personas");
    Console.WriteLine("2. Agregar persona");
    Console.WriteLine("3. Eliminar por Id");
    Console.WriteLine("4. Ordenar por Nombre");
    Console.WriteLine("5. Guardar cambios");
    Console.WriteLine("0. Salir");
    Console.Write("Elige una opción: ");
    return Console.ReadLine() ?? string.Empty;
}