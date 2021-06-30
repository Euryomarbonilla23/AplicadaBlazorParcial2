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
    public class VentasBLL
    {
        private Contexto contexto { get; set; }
        public VentasBLL(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public async Task<Ventas> Buscar(int id)
        {
            Ventas ventas;

            try
            {
                ventas = await contexto.Ventas
                  .Where(b => b.VentaId == id)
                    .Include(c => c.cobrosDetalles)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return ventas;
        }

        public async Task<List<Ventas>> GetVentas()
        {
            List<Ventas> lista = new List<Ventas>();

            try
            {
                lista = await contexto.Ventas.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return lista;
        }

        public async Task<List<Ventas>> GetList(Expression<Func<Ventas, bool>> criterio)
        {
            List<Ventas> lista = new List<Ventas>();

            try
            {
                lista = await contexto.Ventas.Where(criterio).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return lista;
        }

        public async Task<List<CobrosDetalles>> GetVentasPorPagar(int clienteId)
        {
            var porPagar = new List<CobrosDetalles>();
            var ventas = await contexto.Ventas.Where(v => v.ClienteId == clienteId && v.Balance > 0)
                .AsNoTracking()
                .ToListAsync();

            foreach (var item in ventas)
            {
                porPagar.Add(new CobrosDetalles
                {
                    VentaId = item.VentaId,
                    Venta = item,
                    Cobrado = 0
                });
            }

            return porPagar;
        }
        public async Task<List<CobrosDetalles>> GetVentasPagada(int clienteId)
        {
            var porPagar = new List<CobrosDetalles>();
            var ventas = await contexto.Ventas
                .Where(v => v.ClienteId == clienteId && v.Balance > 0)
                .AsNoTracking()
                .ToListAsync();

            foreach (var item in ventas)
            {
                porPagar.Add(new CobrosDetalles
                {
                    VentaId = item.VentaId,
                    Venta = item,
                    Cobrado = 0
                });
            }

            return porPagar;
        }

    }
}
