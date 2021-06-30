using AplicadaBlazorParcial2.DAL;
using AplicadaBlazorParcial2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AplicadaBlazorParcial2.BLL
{
    public class ClientesBLL
    {
        private Contexto contexto { get; set; }
        public ClientesBLL(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public async Task<Clientes> Buscar(int id)
        {
            Clientes clientes;

            try
            {

                clientes = await contexto.Clientes
                   .Where(p => p.ClienteId == id)
                   .AsNoTracking()
                   .FirstOrDefaultAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return clientes;
        }

        public async Task<List<Clientes>> GetClientes()
        {
            List<Clientes> lista = new List<Clientes>();

            try
            {
                lista = await contexto.Clientes.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return lista;
        }

        public async Task<List<Clientes>> GetList(Expression<Func<Clientes, bool>> criterio)
        {
            List<Clientes> lista = new List<Clientes>();

            try
            {
                lista = await contexto.Clientes.Where(criterio).Include(v => v.Ventas).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return lista;
        }
    }
}
