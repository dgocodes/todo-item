using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo.Domain.Entities;

namespace Todo.Domain.Tests.EntitiesTests
{
    [TestClass]
    public class TodoItemTests
    {
        private readonly TodoItem _todo = new TodoItem("Título", "Usuário", DateTime.Now);

        [TestMethod]
        public void Dado_um_novo_todo_concluido_deve_ser_false()
        {
            Assert.AreEqual(_todo.Done, false);
        }
    }
}