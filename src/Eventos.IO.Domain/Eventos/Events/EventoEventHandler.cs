using Eventos.IO.Domain.Core.Events;
using Eventos.IO.Domain.Eventos.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.Eventos.Events
{
    public class EventoEventHandler : IHandler<EventoRegistradoEvent>,
        IHandler<EventoAtualizadoEvent>,
        IHandler<EventoExcluidoEvent>
    {
        public void Handle(EventoExcluidoEvent message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Evento excluido com sucesso!");
        }

        public void Handle(EventoAtualizadoEvent message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Evento atualizado com sucesso!");
        }

        public void Handle(EventoRegistradoEvent message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Evento registrado com sucesso!");
        }
    }
}
