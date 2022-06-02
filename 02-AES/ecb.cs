using System.Security.Cryptography;
using System.Text;

var mensagem = "desenvolvedor.io 654654";
byte[] chave = new byte[16]; // <- Tamanho da chave
RandomNumberGenerator.Fill(chave); // <- Preenche a chave com números verdadeiramente aleatórios

Console.WriteLine("============== CRIPTOGRAFANDO ==============");

Aes aes = Aes.Create();
aes.Key = chave; // Oracle Padding 
var ciphertext = aes.EncryptEcb(Encoding.UTF8.GetBytes(mensagem), PaddingMode.PKCS7); // <- Criptografa utilizando o ECB (Não recomendado) / PaddingMode.PKCS7

Console.WriteLine("Mensagem: {0}", mensagem);
Console.WriteLine("Senha: {0}", Convert.ToHexString(chave));
Console.WriteLine("Cipher: {0}", Convert.ToHexString(ciphertext));
Console.WriteLine();

Console.WriteLine("============== DESCRIPTOGRAFANDO ==============");

Console.WriteLine(Encoding.UTF8.GetString(aes.DecryptEcb(ciphertext, PaddingMode.PKCS7)));