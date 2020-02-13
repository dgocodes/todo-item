using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo.Domain.Entities;
using Todo.Domain.Queries;

namespace Todo.Domain.Tests.QueryTests
{
    [TestClass]
    public class TodoQueryTests
    {
        private List<TodoItem> _items;

        public TodoQueryTests()
        {
            _items = new List<TodoItem>{
                new TodoItem("Titulo 1", "Usuario 1", DateTime.Now),
                new TodoItem("Titulo 2", "Usuario 2", DateTime.Now),
                new TodoItem("Titulo 3", "Usuario 3", DateTime.Now),
                new TodoItem("Titulo 4", "Usuario 4", DateTime.Now),
            };
        }

        [TestMethod]
        public void Retornar_apenas_tarefas_do_usuario1()
        {
            var result = _items.AsQueryable()
                               .Where(TodoQueries.GetAll("Usuario 1"));

            Assert.AreEqual(1, result.Count());
        }
    }
}