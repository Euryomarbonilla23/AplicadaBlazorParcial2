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
    public class CobrosBLL
    {
        private Contexto contexto { get; set; }
        public CobrosBLL(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public async Task<bool> Guardar(Cobros cobros)
        {
            return await Insertar(cobros);
        }

        private async Task<bool> Insertar(Cobros cobros)
        {
            bool paso = false;

            try
            {
                foreach (var item in cobros.cobrosDetalles)
                {
                    item.Venta = await contexto.Ventas.FindAsync(item.VentaId);
                    item.Venta.Balance -= item.Cobrado;
                    contexto.Entry(item.Venta).State = EntityState.Modified;
                }
                await contexto.Cobros.AddAsync(cobros);
                paso = (contexto.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

        public async Task<Cobros> Buscar(int id)
        {
            Cobros cobros;

            try
            {
                cobros = await contexto.Cobros.
                    Where(c => c.CobroId == id)
                    .Include(c => c.cobrosDetalles)
                    .FirstOrDefaultAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return cobros;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool ok = false;
            try
            {
                var registro = await Buscar(id);

                if (registro != null)
                {
                    contexto.Cobros.Remove(registro);
                    ok = await contexto.SaveChangesAsync() > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        public async Task<List<Cobros>> GetCobros()
        {
            List<Cobros> lista = new List<Cobros>();

            try
            {
                lista = await contexto.Cobros.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return lista;
        }

        public async Task<List<Cobros>> GetList(Expression<Func<Cobros, bool>> criterio)
        {
            List<Cobros> lista = new List<Cobros>();

            try
            {
                lista = await contexto.Cobros.Where(criterio).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return lista;
        }
    }

}
