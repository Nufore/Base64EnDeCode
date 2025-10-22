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

    public static void ConvertToFile(string dirFrom, string fileName, CustomLogger logger)
    {
        if (!Directory.Exists(dirFrom))
        {
            logger.Log($"Директория {dirFrom} не инициализирована.");
            return;
        }

        var fullFilePath = Path.Combine(dirFrom, fileName);

        if (!File.Exists(fullFilePath))
        {
            logger.Log($"В директории {dirFrom} нет файла \"{fileName}\".");
            return;
        }

        string base64String = File.ReadAllText(fullFilePath);
        byte[] fileBytes = Convert.FromBase64String(base64String);

        string extension = FileTypeDetector.DetectFileExtension(fileBytes);
        string outputFileName = $"result{DateTime.UtcNow:yyyyMMddHHmm}.txt";
        string outputFilePath = Path.ChangeExtension(Path.Combine(dirFrom, outputFileName), extension);

        File.WriteAllBytes(outputFilePath, fileBytes);
        logger.Log("Файл успешно создан.");
    }
}

public class FileTypeDetector
{
    private static readonly Dictionary<string, byte[]> FileSignatures = new Dictionary<string, byte[]>
    {
        { ".png", new byte[] { 0x89, 0x50, 0x4E, 0x47 } },
        { ".jpg", new byte[] { 0xFF, 0xD8, 0xFF } },
        { ".jpeg", new byte[] { 0xFF, 0xD8, 0xFF } },
        { ".gif", new byte[] { 0x47, 0x49, 0x46 } },
        { ".bmp", new byte[] { 0x42, 0x4D } },
        { ".pdf", new byte[] { 0x25, 0x50, 0x44, 0x46 } },
        { ".zip", new byte[] { 0x50, 0x4B, 0x03, 0x04 } },
        { ".doc", new byte[] { 0xD0, 0xCF, 0x11, 0xE0 } },
        { ".docx", new byte[] { 0x50, 0x4B, 0x03, 0x04 } },
        { ".xlsx", new byte[] { 0x50, 0x4B, 0x03, 0x04 } },
        { ".mp3", new byte[] { 0x49, 0x44, 0x33 } },
        { ".mp4", new byte[] { 0x00, 0x00, 0x00, 0x18, 0x66, 0x74, 0x79, 0x70 } }
    };

    public static string DetectFileExtension(byte[] data)
    {
        foreach (var signature in FileSignatures)
        {
            if (data.Length >= signature.Value.Length)
            {
                bool match = true;
                for (int i = 0; i < signature.Value.Length; i++)
                {
                    if (data[i] != signature.Value[i])
                    {
                        match = false;
                        break;
                    }
                }

                if (match)
                    return signature.Key;
            }
        }

        return ".txt";
    }
}