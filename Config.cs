namespace Base64EnDeCode;

public class Config
{
    Decoder decoder;
    Encoder encoder;
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