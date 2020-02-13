using Flunt.Notifications;
using Todo.Domain.Commands;
using Todo.Domain.Commands.Contracts;
using Todo.Domain.Entities;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Repositories;

namespace Todo.Domain.Handlers
{
    public class TodoHandler : Notifiable,
                               IHandler<CreateTodoCommand>,
                               IHandler<UpdateTodoCommand>,
                               IHandler<MarkTodoAsDoneCommand>,
                               IHandler<MarkTodoAsUndoneCommand>
    {

        private readonly ITodoRepository _repository;

        public TodoHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(CreateTodoCommand command)
        {
            command.Validate();

            if (command.Invalid)
                return new GenericCommandResult(false, "Ops, problemas com sua tarefa!", command.Notifications);

            var model = new TodoItem(command.Title, command.User, command.Date);

            _repository.Create(model);

            return new GenericCommandResult(true, "Tarefa criada com sucesso!", model);
        }

        public ICommandResult Handle(UpdateTodoCommand command)
        {
            command.Validate();

            if (command.Invalid)
                return new GenericCommandResult(false, "Ops, problemas com sua tarefa!", command.Notifications);

            var todo = _repository.GetById(command.Id, command.User);

            todo.UpdateTitle(command.Title);

            _repository.Update(todo);

            return new GenericCommandResult(true, "Tarefa atualizada com sucesso!", todo);
        }

        public ICommandResult Handle(MarkTodoAsDoneCommand command)
        {
            command.Validate();

            if (command.Invalid)
                return new GenericCommandResult(false, "Ops, problemas com sua tarefa!", command.Notifications);

            var todo = _repository.GetById(command.Id, command.User);

            todo.MarkAsDone();

            _repository.Update(todo);

            return new GenericCommandResult(true, "Tarefa marcada como concluida!", todo);
        }

        public ICommandResult Handle(MarkTodoAsUndoneCommand command)
        {
            command.Validate();

            if (command.Invalid)
                return new GenericCommandResult(false, "Ops, problemas com sua tarefa!", command.Notifications);

            var todo = _repository.GetById(command.Id, command.User);

            todo.MarkAsUndone();

            _repository.Update(todo);

            return new GenericCommandResult(true, "Tarefa removida das tarefas concluídas!", todo);
        }
    }
}