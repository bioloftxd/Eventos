using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventos.IO.Domain.Core.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace Eventos.IO.Site.Views.ViewComponents
{
    public class SummaryViewComponent : ViewComponent
    {
        private readonly IDomainNotificationHandler<DomainNotification> _notifications;

        public SummaryViewComponent(IDomainNotificationHandler<DomainNotification> notifications)
        {
            _notifications = notifications;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notificacoes = await Task.FromResult(_notifications.GetNotification());
            notificacoes.ForEach(c=> ViewData.ModelState.AddModelError(string.Empty, c.Value));
            return View();
        }
    }
}
