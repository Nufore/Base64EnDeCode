namespace Base64EnDeCode;

public class Config
{
    public Decoder decoder { get; set; }
    public Encoder encoder { get; set; }
}

public class Decoder
{
    public string dirFrom { get; set; }
    public string fileName { get; set; }
}

public class Encoder
{
    public string dirFrom { get; set; }
}