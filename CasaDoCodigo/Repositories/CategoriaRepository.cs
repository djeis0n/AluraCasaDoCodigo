using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface ICategoriaRepository
    {
        Task<Categoria> GetCategoria(string nome);
        IList<Categoria> GetCategorias();
    }

    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public async Task<Categoria> GetCategoria(string nome)
        {
            var categoriaDB = dbSet
                .Where(c => c.Nome == nome)
                .SingleOrDefault();

            if (categoriaDB == null)
            {
                var novaCategoria = new Categoria()
                {
                    Nome = nome
                };

                categoriaDB = dbSet.Add(novaCategoria).Entity;
            }

            await contexto.SaveChangesAsync();
            return categoriaDB;
        }

        public IList<Categoria> GetCategorias()
        {
            return dbSet.ToList();
        }
    }

}
