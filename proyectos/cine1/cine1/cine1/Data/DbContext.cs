using cine1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace cine1.Data
{
    public class DbContext
    {
        public DbContext(string valor) => Valor = valor;
        public string Valor { get; }
    }
}
