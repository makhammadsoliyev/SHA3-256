using Org.BouncyCastle.Crypto.Digests;
using System.Text;


var filesPath = @"..\..\..\..\SHA3-256\Files\";
var hashList = CalculateAllSHA3FromFiles(filesPath);

hashList.Sort();

var concatenatedHashes = string.Join("", hashList);
var email = "makhammadsoliyev@gmail.com";

concatenatedHashes += email;

var result = CalculateSHA3(Encoding.UTF8.GetBytes(concatenatedHashes));
Console.WriteLine(result);

static List<string> CalculateAllSHA3FromFiles(string filesPath)
{
    var hashList = new List<string>();
    var files = Directory.GetFiles(filesPath);

    foreach (var file in files)
    {
        var fileContent = File.ReadAllBytes(file);
        var hash = CalculateSHA3(fileContent);

        hashList.Add(hash);
    }

    return hashList;
}

static string CalculateSHA3(byte[] data)
{
    var sha3 = new Sha3Digest(256);

    var hasBytes = new byte[sha3.GetDigestSize()];

    sha3.BlockUpdate(data, 0, data.Length);

    sha3.DoFinal(hasBytes, 0);

    var hashBuilder = new StringBuilder();

    foreach ( var b in hasBytes )
        hashBuilder.Append(b.ToString("x2"));

    return hashBuilder.ToString();
}
