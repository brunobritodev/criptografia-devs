using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
static void WriteByteArray(string name, byte[] byteArray)
{
    Console.Write("{0}: {1} bytes, {2}-bit:", name, byteArray.Length, byteArray.Length << 3);
    Console.WriteLine(" -> {0}", BitConverter.ToString(byteArray));
}


var rsa = RSA.Create();
var pem = File.ReadAllText("rsa-private-key.pem");
rsa.ImportFromPem(pem);

var mensagem = "desenvolvedorio";


Console.WriteLine("========================== CRIPTOGRAFAR =========================");

var cypher = rsa.Encrypt(Encoding.UTF8.GetBytes(mensagem), RSAEncryptionPadding.Pkcs1);
WriteByteArray("Cypher", cypher);

Console.WriteLine("========================== DESCRIPTOGRAFAR =========================");

var clearText = rsa.Decrypt(cypher, RSAEncryptionPadding.Pkcs1);
Console.WriteLine($"Clear text: {Encoding.UTF8.GetString(clearText)}");


Console.WriteLine("========================== ASSINAR DIGITALMENTE =========================");

var assinatura = rsa.SignData(Encoding.UTF8.GetBytes(mensagem), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
var documento = new { Mensagem = mensagem, Assinatura = assinatura };
Console.WriteLine($"Documento:");
Console.WriteLine(JsonSerializer.Serialize(documento, new JsonSerializerOptions() { WriteIndented = true }));

Console.WriteLine("========================== VALIDAR ASSINATURA =========================");

var assinaturaEstaCorreta = rsa.VerifyData(Encoding.UTF8.GetBytes(mensagem), assinatura, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
Console.WriteLine($"Assinatura esta correta: {assinaturaEstaCorreta}");


Console.WriteLine("========================== CARREGAR CHAVE PUBLICA =========================");

rsa = RSA.Create();
pem = File.ReadAllText("rsa-public-key.pem");
rsa.ImportFromPem(pem);
Console.WriteLine("Utilizando objeto RSA apenas com chave publica");


Console.WriteLine("========================== CRIPTOGRAFAR COM CHAVE PUBLICA =========================");
cypher = rsa.Encrypt(Encoding.UTF8.GetBytes(mensagem), RSAEncryptionPadding.Pkcs1);
WriteByteArray("Cypher", cypher);

Console.WriteLine("========================== VALIDAR ASSINATURA COM CHAVE PUBLICA =========================");
assinaturaEstaCorreta = rsa.VerifyData(Encoding.UTF8.GetBytes(mensagem), assinatura, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
Console.WriteLine($"Assinatura esta correta: {assinaturaEstaCorreta}");

Console.WriteLine("========================== TENTAR DESCRIPTOGRAFAR COM CHAVE PUBLICA =========================");
try
{
    rsa.Decrypt(cypher, RSAEncryptionPadding.Pkcs1);
}
catch (Exception e)
{
    Console.WriteLine(e);
}

Console.WriteLine("========================== TENTAR ASSINAR COM CHAVE PUBLICA =========================");
try
{
    rsa.SignData(Encoding.UTF8.GetBytes(mensagem), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
}
catch (Exception e)
{
    Console.WriteLine(e);
}