#!/bin/bash

# Gerar chave RSA de 2048 bits
openssl genrsa -out rsa-private-key.pem 2048

# Exibir informacoes da chave
openssl rsa -in rsa-private-key.pem -text -noout

# Gerar chave publica
openssl rsa -in rsa-private-key.pem -pubout -out rsa-public-key.pem 

# Exibir informacoes da chave publica
openssl rsa -pubin -in rsa-public-key.pem -text -noout