using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BlogCore.AccesoDatos.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        protected readonly DbContext Context;
        internal DbSet<T> dbSet;

        public Repository(DbContext context)
        {
            Context = context;
            this.dbSet = context.Set<T>();
            
        }




        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null)
        {
            // Se crea una consulta IQueryable a partir del DbSet del contexto
            IQueryable<T> query = dbSet;  
            
            // Se aplica el filtro si se proporciona
            if(filter != null)
            {
                query = query.Where(filter);
            }

            // Se incluyen propiedades de navegacion si se proporcionan
            if(includeProperties != null)
            {
                // SE DIVIDE LA CADENA DE PROPIEDADES POR COMA Y SE ITERA SOBRE ELLAS
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) 
                {
                    query = query.Include(includeProperty);
                }
            }

            //Se aplica el ordenamiento si se proporciona
            if (orderBy != null)
            {
                // sE EJECUTA LA FUNCION DE ORDENAMIENTO Y SE CONVIERTE LA CONSULTA EN UNA LISTA
                return orderBy(query).ToList();

            }
            //SI NO SE PROPORCIONA UN ORDENAMIENTO, SIMPLEMENTE SE CONVIERTE LA CONSULTA EN UNA LISTA
            return query.ToList();
                
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach(var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return query.FirstOrDefault();
        }

        public void Remove(int id)
        {
            T entityToRemove = dbSet.Find(id);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
