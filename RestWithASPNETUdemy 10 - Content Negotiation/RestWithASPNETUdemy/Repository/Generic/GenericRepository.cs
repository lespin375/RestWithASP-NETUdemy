﻿using Microsoft.EntityFrameworkCore;
using RestWithASPNETUdemy.Model.Base;
using RestWithASPNETUdemy.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNETUdemy.Repository.Generic
{
    //A Implementação do repositório genérico recebe qualquer tipo T que implemente IRepository de mesmo tipo
    //desde que T extenda BaseEntity
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private MySQLContext _context;
        private DbSet<T> dataset;

        public GenericRepository(MySQLContext context)
        {
            _context = context;
            dataset = _context.Set<T>();
        }

        public T Create(T item)
        {
            try {
                dataset.Add(item);
                _context.SaveChanges();
            } catch (Exception ex) {
                throw ex;
            }
            return item;
        }

        public void Delete(long id)
        {
            var result = dataset.SingleOrDefault(p => p.Id.Equals(id));
            try {
                if (result != null) {
                    dataset.Remove(result);
                    _context.SaveChanges();
                }
            } catch (Exception ex) {
                throw ex;
            }
        }

        public bool Exists(long? id)
        {
            return dataset.Any(p => p.Id.Equals(id));
        }

        public List<T> FindAll()
        {
            return dataset.ToList();
        }

        public T FindById(long id)
        {
            return dataset.SingleOrDefault(p => p.Id.Equals(id));
        }

        public T Update(T item)
        {
            // Verificamos se a pessoa existe na base
            // Se não existir retornamos uma instancia vazia de pessoa
            if (!Exists(item.Id)) return null;

            // Pega o estado atual do registro no banco
            // seta as alterações e salva
            var result = dataset.SingleOrDefault(b => b.Id == item.Id);
            if (result != null) {
                try {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                } catch (Exception ex) {
                    throw ex;
                }
            }
            return result;
        }
    }
}
