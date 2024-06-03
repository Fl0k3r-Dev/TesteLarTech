using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TesteLarTech.Core.Hash;
using TesteLarTech.Domain.Entities;
using Xunit;

namespace TesteLarTech.Tests.Entities
{
    public class PessoaTests
    {
        [Fact]
        public void Constructor_WithValidParameters_ShouldInitializeProperties()
        {
            string email = "test@example.com";
            string password = "password";
            string nivel = "default";
            string nome = "Teste";
            string cpf = "12345678900";
            DateTime nascimento = DateTime.Now;
            bool ativo = true;

            var pessoa = new Pessoa(email, password, nivel, nome, cpf, nascimento, ativo);

            Assert.Equal(email, pessoa.Email);
            Assert.Equal(password, pessoa.Password);
            Assert.Equal(nivel, pessoa.Nivel);
            Assert.Equal(nome, pessoa.Nome);
            Assert.Equal(cpf, pessoa.CPF);
            Assert.Equal(nascimento, pessoa.Nascimento);
            Assert.Equal(ativo, pessoa.Ativo);
            Assert.NotNull(pessoa.Telefones);
            Assert.Empty(pessoa.Telefones);
        }

        [Theory]
        [InlineData("invalidemail.com")]
        [InlineData("invalidemail@")]
        [InlineData("invalidemail")]
        public void SetEmail_WithInvalidEmail_ShouldNotChangeEmail(string invalidEmail)
        {
            var pessoa = new Pessoa();
            string initialEmail = "initial@example.com";
            pessoa.Email = initialEmail;

            pessoa.SetEmail(invalidEmail);

            Assert.Equal(initialEmail, pessoa.Email);
        }

        [Theory]
        [InlineData("validemail@example.com")]
        [InlineData("another.valid.email@test.com")]
        public void SetEmail_WithValidEmail_ShouldChangeEmail(string validEmail)
        {
            var pessoa = new Pessoa();

            pessoa.SetEmail(validEmail);

            Assert.Equal(validEmail, pessoa.Email);
        }

        [Fact]
        public void SetPassword_WithValidPassword_ShouldEncryptPassword()
        {
            var pessoa = new Pessoa();
            string password = "password";

            pessoa.SetPassword(password);

            Assert.NotEmpty(pessoa.Password);
            Assert.NotEqual(password, pessoa.Password);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void SetPassword_WithNullOrEmptyPassword_ShouldNotChangePassword(string password)
        {
            var pessoa = new Pessoa();
            string initialPassword = "initial";

            pessoa.Password = initialPassword;
            pessoa.SetPassword(password);

            Assert.Equal(initialPassword, pessoa.Password);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void SetNome_WithNullOrEmptyName_ShouldNotChangeName(string nome)
        {
            var pessoa = new Pessoa();
            string initialName = "Initial Name";

            pessoa.Nome = initialName;
            pessoa.SetNome(nome);

            Assert.Equal(initialName, pessoa.Nome);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetStatus_ShouldInvertStatus(bool initialStatus)
        {
            var pessoa = new Pessoa();
            pessoa.Ativo = initialStatus;

            pessoa.SetStatus(initialStatus);

            Assert.Equal(!initialStatus, pessoa.Ativo);
        }
    }
}
