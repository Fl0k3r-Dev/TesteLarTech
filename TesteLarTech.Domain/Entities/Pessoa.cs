using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using TesteLarTech.Core.Hash;
using TesteLarTech.Domain.Entities.Base;

namespace TesteLarTech.Domain.Entities
{
    public class Pessoa : EntityBase
    {

        public Pessoa()
        {
        }

        public Pessoa(string email, string password, string nivel, string nome, string cpf, DateTime nascimento, bool ativo)
        {
            Email = email;
            Password = password;
            Nivel = nivel;
            Nome = nome;
            CPF = cpf;
            Nascimento = nascimento;
            Ativo = ativo;
            Telefones = new List<Telefone>();
        }

        
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nivel { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime Nascimento { get; set; }
        public bool Ativo { get; set; }

        public ICollection<Telefone> Telefones { get; set; } = new List<Telefone>();


        public void SetEmail(string email)
        {
            if (new EmailAddressAttribute().IsValid(email))
            {
                Email = email;
            }
        }

        public void SetPassword(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                Password = new Encrypt(SHA512.Create()).EncryptString(str);
            }
        }

        public void SetNome(string nome)
        {
            if (!string.IsNullOrEmpty(nome))
            {
                Nome = nome;
            }
        }

        public void SetStatus(bool status)
        {
            Ativo = !status;
        }

    }
}
