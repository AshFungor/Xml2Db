using Microsoft.EntityFrameworkCore;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xml2Db.Models;

namespace Xml2Db.Desktop.ViewModels
{
    public class OriginsViewModel
    {
        private readonly Func<Context> _contextFactory;

        public OriginsViewModel(Func<Context> contextFactory)
        {
            _contextFactory = contextFactory;
            Load();
        }

        public ObservableCollection<Origin> Origins { get; } = new ObservableCollection<Origin>();

        public void Load()
        {
            using (var context = _contextFactory())
            {
                Origins.Clear();
                Origins.AddRange(context.Origins);
            }
        }
    }
}
