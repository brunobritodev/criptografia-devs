using System.Security.Cryptography;
using System.Text;
static void WriteByteArray(string name, byte[] byteArray)
{
    Console.Write("{0}: {1} bytes, {2}-bit:", name, byteArray.Length, byteArray.Length << 3);
    Console.WriteLine(" -> {0} - base64: {1}", BitConverter.ToString(byteArray), Convert.ToBase64String(byteArray));
}

// variaveis
string mensagem = "desenvolvedor.io";
byte[] chave = new byte[16];
byte[] initializationVector = new byte[12];
byte[] authTag = new byte[16];

// Gera a chave e o iv
RandomNumberGenerator.Fill(chave);
RandomNumberGenerator.Fill(initializationVector);

// Exibe as informacoes na tela
Console.WriteLine("============== CRIPTOGRAFANDO ==============");
Console.WriteLine("mensagem: {0}", mensagem);
WriteByteArray("chave", chave);
Console.WriteLine();

// converte a mensagem para bytes
byte[] plainBytes = Encoding.UTF8.GetBytes(mensagem);
byte[] cipher = new byte[plainBytes.Length];

// Criptografia
using (var aesgcm = new AesGcm(chave))
    aesgcm.Encrypt(initializationVector, plainBytes, cipher, authTag);

WriteByteArray("cipher", cipher);
WriteByteArray("iv", initializationVector);
WriteByteArray("authTag", authTag);

Console.WriteLine();

Console.WriteLine("============== DESCRIPTOGRAFANDO ==============");
// Transforma em base64 para poder transmitir
Console.WriteLine("cipher: {0}", Convert.ToBase64String(cipher));
Console.WriteLine("iv: {0}", Convert.ToBase64String(initializationVector));
Console.WriteLine("authTag: {0}", Convert.ToBase64String(authTag));


Console.WriteLine();
// allocate the decrypted text byte array as the same size as the plain text byte array
byte[] decryptedBytes = new byte[cipher.Length];
// perform decryption
using (AesGcm aesgcm = new AesGcm(chave)) 
    aesgcm.Decrypt(initializationVector, cipher, authTag, decryptedBytes);

// convert the byte array to the plain text string
string decryptedText = Encoding.UTF8.GetString(decryptedBytes);
Console.WriteLine("mensagem: {0}", decryptedText);
Console.WriteLine();

