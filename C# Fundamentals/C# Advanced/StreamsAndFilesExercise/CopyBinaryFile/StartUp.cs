using System.IO;

class StartUp
{
    static void Main()
    {
        using (var sourseFile = new FileStream("copyMe.png", FileMode.Open))
        {
            using (var destinationFile = new FileStream("copy.png", FileMode.Create))
            {
                byte[] buffer = new byte[4096];

                int readBytesCount;
                while ((readBytesCount = sourseFile.Read(buffer, 0, buffer.Length)) != 0)
                {
                    destinationFile.Write(buffer, 0, readBytesCount);

                }
            }
        }
    }
}