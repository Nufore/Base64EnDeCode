namespace Base64EnDeCode;

public static class Converter
{
    public static void ConvertToBase64(string dirFrom, CustomLogger logger)
    {
        if (!Directory.Exists(dirFrom))
        {
            logger.Log($"Директория {dirFrom} не инициализирована.");
            return;
        }

        var files = Directory.EnumerateFiles(dirFrom);
        if (files.Count() == 0)
        {
            logger.Log($"Директория {dirFrom} не содержит файлов.");
            return;
        }

        foreach (var file in files)
        {
            logger.Log($"Чтение файла {file}.");
            byte[] fileBytes = File.ReadAllBytes(file);
            string base64String = Convert.ToBase64String(fileBytes);
            string encodedFileName = file + $"_encoded{DateTime.UtcNow:yyyyMMddHHmm}.txt";
            File.WriteAllText(encodedFileName, base64String);
            logger.Log($"Данные по файлу {file} переведены в base64.");
        }
    }
}