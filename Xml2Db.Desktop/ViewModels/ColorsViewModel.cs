using Microsoft.EntityFrameworkCore;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xml2Db.Models;

namespace Xml2Db.Desktop.ViewModels
{
    public class ColorsViewModel
    {
        private readonly Func<Context> _contextFactory;

        public ColorsViewModel(Func<Context> contextFactory)
        {
            _contextFactory = contextFactory;
            Load();
        }

        public ObservableCollection<Color> Colors { get; } = new ObservableCollection<Color>();

        public void Load()
        {
            using (var context = _contextFactory())
            {
                Colors.Clear();
                Colors.AddRange(context.Colors);
            }
        }
    }
}
