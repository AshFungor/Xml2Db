using Microsoft.EntityFrameworkCore;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xml2Db.Models;

namespace Xml2Db.Desktop.ViewModels
{
    public class WaresViewModel
    {
        private readonly Func<Context> _contextFactory;

        public WaresViewModel(Func<Context> contextFactory)
        {
            _contextFactory = contextFactory;
            Load();
        }

        public ObservableCollection<Ware> Wares { get; } = new ObservableCollection<Ware>();

        public void Load()
        {
            using (var context = _contextFactory())
            {
                Wares.Clear();
                Wares.AddRange(
                    context.Wares
                    .Include(w => w.Origin)
                    .Include(w => w.Color));
            }
        }
    }
}
